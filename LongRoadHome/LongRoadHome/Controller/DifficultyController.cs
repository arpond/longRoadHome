using System;
using System.Collections.Generic;
using uk.ac.dundee.arpond.longRoadHome.Model;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using System.Linq;
namespace uk.ac.dundee.arpond.longRoadHome.Controller
{
    public class DifficultyController
    {
        public const String TAG = "DifficultyController";
        private double endLocationChance;
        private int endLocationMinimum;
        private double eventModifier;
        private double eventChance;
        private List<double> playerStatusTracker;
        private double playerStatus;
        private ModelFacade mf;
        private const double smoothing = 0.75d;
        private Random rnd = new Random();

        public DifficultyController()
        {
            mf = new ModelFacade();
            playerStatusTracker = new List<double>();
            playerStatus = 1;
            endLocationMinimum = 50;
            CalcEndLocationChance();
            CalcEventModifier();
            CalcEventChance();
        }

        /// <summary>
        /// Constructor from file
        /// </summary>
        /// <param name="toParse"></param>
        public DifficultyController(String toParse)
        {
            mf = new ModelFacade();
            playerStatusTracker = new List<double>();
            String[] dcElements = toParse.Split(':');
            double temp;
            double.TryParse(dcElements[1], out playerStatus);
            String[] trackerElements = dcElements[2].Split('|');
            for (int i = 1; i < trackerElements.Length; i++)
            {
                if (double.TryParse(trackerElements[i], out temp))
                {
                    playerStatusTracker.Add(temp);
                }
            }
            endLocationMinimum = 50;
            CalcEndLocationChance();
            CalcEventModifier();
            CalcEventChance();
        }

        public void CalcEventChance()
        {
            eventChance = (double) rnd.Next(40,80) / 100;
        }

        /// <summary>
        /// Calculates the end location chance
        /// </summary>
        private void CalcEndLocationChance()
        {
            if (playerStatusTracker.Count < endLocationMinimum)
            {
                endLocationChance = 0;
            }
            else
            {
                endLocationChance = (double)playerStatusTracker.Count / endLocationMinimum - 1;
            }
        }

        /// <summary>
        /// Calculates the current event modifier
        /// </summary>
        private void CalcEventModifier()
        {
            
            if (playerStatus > 1)
            {
                eventModifier = Math.Ceiling(playerStatus) - playerStatus;
            }
            else
            {
                eventModifier = 2 - playerStatus;
            }
        }


        /// <summary>
        /// Updates the player status
        /// </summary>
        /// <param name="gs">The game state to use to update with</param>
        public void UpdatePlayerStatus(GameState gs)
        {
            var stats = mf.GetPlayerCharacterResources(gs).Values;
            int statsSum = stats.Sum(value => value/100);

            double invValue = mf.GetValueOfInventory(gs);

            double newPlayerStatus = (double) statsSum / 4 + invValue;
            playerStatus = smoothing * playerStatus + (1 - smoothing) * newPlayerStatus;
            CalcEventModifier();
        }

        /// <summary>
        /// For testing only
        /// </summary>
        /// <param name="statsSum"></param>
        /// <param name="invValue"></param>
        public void UpdatePlayerStatus(int statsSum, double invValue)
        {
            double newPlayerStatus = (double) statsSum / 400 + invValue;
            playerStatus = smoothing * playerStatus + (1 - smoothing) * newPlayerStatus;
        }

        /// <summary>
        /// Updates the player status tracker with the current player status
        /// </summary>
        public void UpdateStatusTracker()
        {
            playerStatusTracker.Add(playerStatus);
            CalcEndLocationChance();
        }

        /// <summary>
        /// Accessor method for the end location chance
        /// </summary>
        /// <returns>Chance of the end location</returns>
        public double GetEndLocationChance()
        {
            return this.endLocationChance;
        }

        /// <summary>
        /// Accessor method for the event modifier
        /// </summary>
        /// <returns>The event modifier</returns>
        public double GetEventModifier()
        {
            return this.eventModifier;
        }

        /// <summary>
        /// Accessor method for the player status tracker
        /// </summary>
        /// <returns>The player status tracker</returns>
        public List<double> GetPlayerStatusTracker()
        {
            return this.playerStatusTracker;
        }


        /// <summary>
        /// 
        /// http://stackoverflow.com/questions/12946341/algorithm-for-scatter-plot-best-fit-line
        /// </summary>
        /// <returns></returns>
        public List<Tuple<int, double>> GenerateBestFitLine()
        {
            int numPoints = playerStatusTracker.Count;
            List<Tuple<int, double>> xyValues = new List<Tuple<int,double>>();
            for (int i = 0; i< numPoints; i++)
            {
                xyValues.Add(new Tuple<int, double>(i, playerStatusTracker[i]));
            }

            double meanX = xyValues.Average(value => value.Item1);
            double meanY = xyValues.Average(value => value.Item2);

            double sumXSquared = xyValues.Sum(value => value.Item1 * value.Item1);
            double sumXY = xyValues.Sum(value => value.Item1 * value.Item2);

            double a = (sumXY / numPoints - meanX * meanY) / (sumXSquared / numPoints - meanX * meanX);
            double b = (a * meanX - meanY);

            double a1 = a;
            double b1 = b;

            return xyValues.Select(value => new Tuple<int, double>(value.Item1, a1 * value.Item1 - b1)).ToList();
        }

        /// <summary>
        /// Tests if the current location is the end location
        /// </summary>
        /// <returns>If the location will be the end location</returns>
        public bool IsEndLocation()
        {
            int value = rnd.Next(1, 101);
            var bestFit = GenerateBestFitLine(); 
            if (value < endLocationChance*100)
            {
                return true;
            }
            else if (bestFit[bestFit.Count-1].Item2*100 < endLocationChance*100)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Accessor method for player status
        /// </summary>
        /// <returns>The current player status</returns>
        public double GetPlayerStatus()
        {
            return this.playerStatus;
        }

        /// <summary>
        /// Accessor method for event chance
        /// </summary>
        /// <returns>The current event chance</returns>
        public double GetEventChance()
        {
            return this.eventChance;
        }

        /// <summary>
        /// Parsed the Difficulty Controller to a string
        /// </summary>
        /// <returns></returns>
        public String ParseToString()
        {
            String parsed = TAG + ":" + playerStatus + ":Tracker";
            foreach(double status in playerStatusTracker)
            {
                parsed += "|" + status;
            }
            return parsed;

        }

        /// <summary>
        /// Checks if the string is a valid difficulty controller
        /// </summary>
        /// <param name="toTest">The string to text</param>
        /// <returns>If the string is a valid difficulty controller</returns>
        public static bool IsValidDifficultyController(String toTest)
        {
            String[] dcElements = toTest.Split(':');
            double temp;
            if (dcElements.Length != 3)
            {
                return false;
            }
            if (dcElements[0] != TAG)
            {
                return false;
            }
            if (!double.TryParse(dcElements[1], out temp) || temp < 0)
            {
                return false;
            }
            String[] trackerElements = dcElements[2].Split('|');
            if (trackerElements[0] != "Tracker")
            {
                return false;
            }
            if (trackerElements.Length > 1)
            {
                for (int i = 1; i < trackerElements.Length; i++)
                {
                    if (!double.TryParse(trackerElements[i], out temp) || temp < 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

}
