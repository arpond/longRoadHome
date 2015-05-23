using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class LocationModel
    {
        public const String VISITED_TAG = "VisitedLocations";
        public const String UNVISITED_TAG = "UnvisitedLocations";

        private const int STD_MIN_SIZE = 1, STD_MAX_SIZE = 5, STD_MAX_ITEMS = 5, STD_MAX_AMOUNT = 10;
        private const int MAX_CONNECTIONS = 4, MIN_CONNECTIONS = 2;

        private SortedList<int,Location> visitedLocation;
        private SortedList<int,DummyLocation> unvisitedLocation;

        private Location currentLocation;
        private Sublocation currentSublocation;

        private Random rnd = new Random();

        private Bitmap worldMap;
        private SortedList<int, System.Windows.Point> buttonAreas;

        static LocationModel()
        {
            Civic civ = new Civic();
            Residential res = new Residential();
            Commercial com = new Commercial();
        }

        public LocationModel()
        {
            visitedLocation = new SortedList<int, Location>();
            unvisitedLocation = new SortedList<int, DummyLocation>();

        }

        /// <summary>
        /// Constructor for starting a new game
        /// </summary>
        /// <param name="numOfLocations">Number of locations to generate</param>
        public LocationModel(int numOfLocations)
        {
            visitedLocation = new SortedList<int, Location>();
            unvisitedLocation = new SortedList<int, DummyLocation>();

            InitializeLocationModel(numOfLocations);

            DummyLocation loc0 = new DummyLocation(0);
            unvisitedLocation.Add(0, loc0);
            var wm = new WorldMap(unvisitedLocation.Values);
            worldMap = wm.tmpBitmap;
            buttonAreas = wm.buttonAreas;
            unvisitedLocation.Remove(0);
        }

        /// <summary>
        /// Constructor for loading a game from save data
        /// </summary>
        /// <param name="visitedLocs">Visisted locations string</param>
        /// <param name="unvisitedLocs">Unvisited locations string</param>
        /// <param name="currLoc">Current location string</param>
        /// <param name="currSLoc">Current sublocation string</param>
        public LocationModel(String visitedLocs, String unvisitedLocs, String currLoc, String currSLoc)
        {
            visitedLocation = new SortedList<int, Location>();
            unvisitedLocation = new SortedList<int, DummyLocation>();

            String[] visitedElem = visitedLocs.Split('#');
            for (int i = 1; i < visitedElem.Length; i++ )
            {
                Location temp = new Location(visitedElem[i]);
                visitedLocation.Add(temp.GetLocationID(), temp);
            }

            String[] unvisitedElem = unvisitedLocs.Split('#');
            for(int j = 1; j < unvisitedElem.Length; j++ )
            {
                DummyLocation temp = new DummyLocation(unvisitedElem[j]);
                unvisitedLocation.Add(temp.GetLocationID(), temp);
            }

            int currID;
            if(int.TryParse(currLoc, out currID))
            {
                visitedLocation.TryGetValue(currID, out currentLocation);
            }

            int currSub;
            if (int.TryParse(currSLoc, out currSub))
            {
                if (currSub == 0)
                {
                    currentSublocation = null;
                }
                else
                {
                    currentSublocation = currentLocation.GetSublocationByID(currSub);
                }
            }
        }

        /// <summary>
        /// Checks if string is valid visited locations list
        /// </summary>
        /// <param name="toTest">String to test</param>
        /// <returns>If they are valid</returns>
        public static bool IsValidVisitedLocations(String toTest)
        {
            String[] visitedElem = toTest.Split('#');
            if (visitedElem[0] != VISITED_TAG)
            {
                return false;
            }
            for (int i = 1; i < visitedElem.Length; i++)
            {
                if(!Location.IsValidLocation(visitedElem[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if string is a valid unvisited locations list
        /// </summary>
        /// <param name="toTest">String to check</param>
        /// <returns>If they are valid</returns>
        public static bool IsValidUnvisitedLocations(String toTest)
        {
            String[] visitedElem = toTest.Split('#');
            if (visitedElem[0] != UNVISITED_TAG)
            {
                return false;
            }
            for (int i = 1; i < visitedElem.Length; i++)
            {
                if (!DummyLocation.IsValidDummyLocation(visitedElem[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Parse visted locations to a string
        /// </summary>
        /// <returns>The string of visited locations</returns>
        public String ParseVisitedToString()
        {
            String parsed = "VisitedLocations";
            foreach(Location visited in visitedLocation.Values)
            {
                parsed += "#" + visited.ParseToString();
            }
            return parsed;
        }

        /// <summary>
        /// Parse unvisted locations to a string
        /// </summary>
        /// <returns>The string of unvisited locations</returns>
        public String ParseUnvisitedToString()
        {
            String parsed = "UnvisitedLocations";
            foreach (DummyLocation unvisited in unvisitedLocation.Values)
            {
                parsed += "#" + unvisited.ParseToString();
            }
            return parsed;
        }

        /// <summary>
        /// Parse the current location to string
        /// </summary>The current location as a string</returns>
        public String ParseCurrLocationToString()
        {
            return "" + currentLocation.GetLocationID();
        }

        /// <summary>
        /// Parse the current sub location to a string
        /// </summary>
        /// <returns>The current sub location as a string</returns>
        public String ParseCurrSubLocToString()
        {
            if (currentSublocation == null)
            {
                return "" + 0;
            }
            return "" + currentSublocation.GetSublocationID();
        }

        /// <summary>
        /// Replace a dummy location with an actual location
        /// </summary>
        /// <param name="dummy">Dummy location to replace</param>
        /// <param name="minSize">Minimum size of the location</param>
        /// <param name="maxSize">Max size of the location</param>
        /// <param name="maxItems">Max items for the location</param>
        /// <param name="maxAmount">Max amount of each item</param>
        /// <returns>A location with the values specified</returns>
        public Location ReplaceDummyLocation(DummyLocation dummy, int minSize, int maxSize, int maxItems, int maxAmount)
        {
            Location location = Location.ConvertToLocation(dummy);
            location.GenerateSubLocations(minSize, maxSize, maxItems, maxAmount);
            return location;
        }

        /// <summary>
        /// Gets the current sublocation
        /// </summary>
        /// <returns>Current sublocation</returns>
        public Sublocation GetSubLocation()
        {
            return this.currentSublocation;
        }

        /// <summary>
        /// Gets the type of the sublocation
        /// </summary>
        /// <returns>The type of the sublocation</returns>
        public String GetSublocType()
        {
            return currentSublocation.GetType().ToString();
        }

        /// <summary>
        /// Change the current sublocation
        /// </summary>
        /// <param name="subLocationID">The id of the sublocation to change to</param>
        public bool ChangeSubLocation(int subLocationID)
        {
            if (currentLocation.SetCurrentSubLocation(subLocationID))
            {
                currentSublocation = currentLocation.GetCurrentSubLocation();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a location as visited
        /// </summary>
        /// <param name="locationID">The id of the location to check</param>
        /// <returns>If the location is visited or unvisited</returns>
        public bool LocationVisited(int locationID)
        {
            return visitedLocation.ContainsKey(locationID);
        }


        /// <summary>
        /// Moves from current location to the location ID
        /// Will not move if move is invalid
        /// </summary>
        /// <param name="locationID">The location ID to move to</param>
        /// <returns>If the move was successful</returns>
        public bool MoveToLocation(int locationID)
        {
            if(locationID < 0 || locationID == currentLocation.GetLocationID() || (!visitedLocation.ContainsKey(locationID) && !unvisitedLocation.ContainsKey(locationID)))
            {
                return false;
            }

            if (LocationVisited(locationID))
            {
                return MoveToVisitedLocation(locationID);
            }
            else
            {
                return MoveToUnvisitedLocation(locationID);
            }
        }

        /// <summary>
        /// Moves from the current location to an unvisited location
        /// </summary>
        /// <param name="locationID">ID of the unvisited location</param>
        /// <returns>If the move was successful</returns>
        private bool MoveToUnvisitedLocation(int locationID)
        {
            DummyLocation toChangeTo;
            if (unvisitedLocation.TryGetValue(locationID, out toChangeTo))
            {
                Location temp = Location.ConvertToLocation(toChangeTo);

                int minSize = STD_MIN_SIZE;
                int maxSize = STD_MAX_SIZE;
                int maxItems = rnd.Next(1, STD_MAX_ITEMS+1);
                int maxAmount = rnd.Next(1, STD_MAX_AMOUNT+1);

                temp.GenerateSubLocations(minSize, maxSize, maxItems, maxAmount);
                temp.SetVisited();
                unvisitedLocation.Remove(locationID);
                visitedLocation.Add(locationID, temp);
                currentLocation = temp;
                currentSublocation = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Moves from the current location to a visited location
        /// </summary>
        /// <param name="locationID">ID of the visited location</param>
        /// <returns>If the move was successful</returns>
        private bool MoveToVisitedLocation(int locationID)
        {
            Location toChangeTo;
            if (visitedLocation.TryGetValue(locationID, out toChangeTo))
            {
                currentLocation = toChangeTo;
                currentSublocation = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Scavenges the current sublocation
        /// </summary>
        /// <param name="possibleItems">Items that can be found here</param>
        /// <returns>List of items found</returns>
        public List<Item> Scavenge(List<Item> possibleItems)
        {
            List<Item> itemsScavenged = new List<Item>();
            if (currentSublocation == null || currentSublocation.GetScavenged())
            {
                return itemsScavenged;
            }

            itemsScavenged = currentSublocation.Scavenge(possibleItems);

            return itemsScavenged;
        }

        /// <summary>
        /// Initializes Location Model 
        /// </summary>
        /// <param name="totalLocations">Total number of locations</param>
        public void InitializeLocationModel(int total)
        {
            List<DummyLocation> newLocations = CreateDummyLocations(total);
            foreach(var loc in newLocations)
            {
                unvisitedLocation.Add(loc.GetLocationID(), loc);
            }
        }



        /// <summary>
        /// Checks if the current sublocation is scavenged
        /// </summary>
        /// <returns>If the current sublocation is scavenged or not</returns>
        public bool IsScavenged()
        {
            return currentSublocation.GetScavenged();
        }

        /// <summary>
        /// Checks if sublocation passed is scavenged
        /// </summary>
        /// <param name="id">ID of the sublocation to check</param>
        /// <returns>If the sublocation is scavenged</returns>
        public bool IsScavenged(int id)
        {
            return currentLocation.GetSublocationByID(id).GetScavenged();
        }

        /// <summary>
        /// Returns the lsit of unvisited locations
        /// </summary>
        /// <returns>Sorted list of unvisted locations</returns>
        public  SortedList<int, DummyLocation> GetUnvisited()
        {
            return this.unvisitedLocation;
        }

        public SortedList<int, Location> GetVisited()
        {
            return this.visitedLocation;
        }

        /// <summary>
        /// Creates a number of dummy locations
        /// Always rounds these to the next multiple of 4.
        /// </summary>
        /// <param name="numberOfLocations">Number of locations to generate (rounded to the next multiple of 4)</param>
        public List<DummyLocation> CreateDummyLocations(int numberOfLocations)
        {
            List<DummyLocation> newLocations = new List<DummyLocation>();
            if (numberOfLocations % 4 != 0)
            {
                numberOfLocations = numberOfLocations + (4 - numberOfLocations % 4); 
            }
            for(int i = 1; i<numberOfLocations+1; i++)
            {
                DummyLocation temp = new DummyLocation(i);
                newLocations.Add(temp);
                //unvisitedLocation.Add(i,temp);
            }
            return newLocations;
        }


        public Location GetCurentLocation()
        {
            return currentLocation;
        }

        public Bitmap GetWorldMap()
        {
            return worldMap;
        }

        public SortedList<int, System.Windows.Point> GetButtonAreas()
        {
            return buttonAreas;
        }
    }
}