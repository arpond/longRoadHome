using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class DummyLocation
    {
        protected HashSet<int> connections = new HashSet<int>();
        private int locationID;
        private String tag = "DummyLocation";

        public DummyLocation()
        {
            locationID = 0;
        }

        public DummyLocation(int locationID)
        {
            this.locationID = locationID;
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
                        int.TryParse(locElem[1], out locationID);
                        break;
                    case "Connections":
                        for (int i = 1; i < locElem.Length; i++ )
                        {
                            int loc;
                            if (int.TryParse(locElem[i], out loc))
                            {
                                connections.Add(loc);
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Checks if two dummy locations are connected
        /// </summary>
        /// <param name="dl">The location to check if it is connected to</param>
        /// <returns>If they are connected</returns>
        public bool IsConnected(DummyLocation dl)
        {
            return connections.Contains(dl.GetLocationID());
        }

        /// <summary>
        /// Gets the connections of a Dummylocation
        /// </summary>
        /// <returns>The connections</returns>
        public HashSet<int> GetConnections()
        {
            return this.connections;
        }
        public void SetConnections(HashSet<int> connections)
        {
            this.connections = connections;
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
        public virtual bool IsValidDummyLocation(String toTest)
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
                        if (locElem.Length != 2 || locElem[1] != tag)
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
                    case "Connections":
                        for (int i = 1; i < locElem.Length; i++)
                        {
                            int loc;
                            if (int.TryParse(locElem[i], out loc))
                            {
                                if (tempID.Contains(loc) || loc == id || loc <= 0)
                                {
                                    return false;
                                }
                                tempID.Add(loc);
                            }
                            else
                            {
                                return false;
                            }
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
            return String.Format("Type:{0},ID:{1},Connections{2}",tag, locationID, ParseConnections());
        }
        
        /// <summary>
        /// Parses the connections
        /// </summary>
        /// <returns>String representing the parsed connections</returns>
        private String ParseConnections()
        {
            String parse = "";
            foreach(int connection in connections)
            {
                parse += ":" + connection;
            }
            return parse;
        }
    }
}
