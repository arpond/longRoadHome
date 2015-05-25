using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class DummyLocation
    {
        private const String TAG = "DummyLocation";

        protected int locationID;
        protected bool visited;

        public DummyLocation()
        {
            locationID = 0;
        }

        public DummyLocation(int locationID)
        {
            this.locationID = locationID;
            visited = false;
        }


        /// <summary>
        /// Parses DummyLocation from a string
        /// </summary>
        /// <param name="toParse">The string to parse from</param>
        public DummyLocation(String toParse)
        {
            String[] dlElems = toParse.Split(',');
            foreach(String elem in dlElems)
            {
                String[] locElem = elem.Split(':'); 
                switch (locElem[0])
                {
                    case "ID":
                        int tempID;
                        int.TryParse(locElem[1], out tempID);
                        locationID = tempID;
                        break;
                }
            }
            visited = false;
        }


        /// <summary>
        /// Gets the location ID
        /// </summary>
        /// <returns>The location ID</returns>
        public int GetLocationID()
        {
            return locationID;
        }

        /// <summary>
        /// Checks if the string is a valid dummy location
        /// </summary>
        /// <param name="toTest">The string to test</param>
        /// <returns>Bool if it is valid</returns>
        public static bool IsValidDummyLocation(String toTest)
        {
            HashSet<int> tempID = new HashSet<int>();
            int id = -1;
            String[] dlElems = toTest.Split(',');
            if (dlElems.Length != 3)
            {
                return false;
            }
            foreach (String elem in dlElems)
            {
                String[] locElem = elem.Split(':');
                switch (locElem[0])
                {
                    case "Type":
                        if (locElem.Length != 2 || locElem[1] != TAG)
                        {
                            return false;
                        }
                        break;
                    case "ID":
                        if (locElem.Length != 2 || !int.TryParse(locElem[1], out id) || id <= 0)
                        {
                            return false;
                        }
                        
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Parses the dummy location to a string
        /// </summary>
        /// <returns>the string representing the dummy location</returns>
        public virtual String ParseToString()
        {
            return String.Format("Type:{0},ID:{1}",TAG, locationID);
        }
        
    }
}
