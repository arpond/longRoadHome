using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Discovery
{
    public class Discovery
    {
        public const String TAG = "Discovery";
        private int discoveryID;
        private String discoveryText;
        private int minLocationNumber;

        public Discovery()
        {
            discoveryID = 1;
            discoveryText = "Test Text";
            minLocationNumber = 1;
        }

        public Discovery(String toParse)
        {
            String[] discElements = toParse.Split(':');
            int.TryParse(discElements[1], out discoveryID);
            discoveryText = discElements[2];
            int.TryParse(discElements[3], out minLocationNumber);
        }

        /// <summary>
        /// Accessor Method for discovery text
        /// </summary>
        /// <returns>Discovery Text</returns>
        public String GetDiscoveryText()
        {
            return this.discoveryText;
        }
        
        /// <summary>
        /// Accessor Method for discovery id
        /// </summary>
        /// <returns>The ID of this discovery</returns>
        public int GetDiscoveryID()
        {
            return this.discoveryID;
        }

        /// <summary>
        /// Accessor method for min location number
        /// </summary>
        /// <returns>The min location number</returns>
        public int GetMinLocationNumber()
        {
            return minLocationNumber;
        }

        /// <summary>
        /// Method to check if Discovery can be discovered
        /// </summary>
        /// <param name="locationsVisited">The number of locations visited so far</param>
        /// <returns>If it is discoverable or not</returns>
        public bool IsDiscoverable(int locationsVisited)
        {
            return locationsVisited >= minLocationNumber;
        }

        /// <summary>
        /// Method to parse discovery to a string
        /// </summary>
        /// <returns>String representing this discovery</returns>
        public String ParseToString()
        {
            return String.Format("{0}:{1}:{2}:{3}", TAG, discoveryID, discoveryText, minLocationNumber);
        }

        /// <summary>
        /// Method to check if string is a valid discovery
        /// </summary>
        /// <param name="toTest">The string to check</param>
        /// <returns>If the stirng is valid or not</returns>
        public static bool IsValidDiscovery(String toTest)
        {

            int discID;
            int minNum;
            String[] discElements = toTest.Split(':');
            if (discElements[0] != TAG || discElements.Length != 4)
            {
                return false;
            }
            if (!int.TryParse(discElements[1], out discID) || discID < 1)
            {
                return false;
            }
            if (!int.TryParse(discElements[3], out minNum) || minNum < 1)
            {
                return false;
            }
            return true;
        }

    }
}
