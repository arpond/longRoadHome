using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class DummyLocation
    {
        private HashSet<int> connections = new HashSet<int>();
        private int locationID;
        private String tag = "DummyLocation";

        public DummyLocation()
        {

        }

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

        public bool IsConnected(DummyLocation dl)
        {
            return connections.Contains(dl.GetLocationID());
        }

        public HashSet<int> GetConnections()
        {
            return this.connections;
        }
        public void SetConnections(HashSet<int> connections)
        {
            this.connections = connections;
        }

        public int GetLocationID()
        {
            return locationID;
        }

        public virtual bool IsValidDummyLocation(String toTest)
        {
            int id;
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
                        if (locElem.Length != 2 || !int.TryParse(locElem[1], out id))
                        {
                            return false;
                        }
                        
                        break;
                    case "Connections":
                        for (int i = 1; i < locElem.Length; i++)
                        {
                            int loc;
                            if (!int.TryParse(locElem[i], out loc))
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

        public virtual String ParseToString()
        {
            return String.Format("Type:{0},ID:{1},Connections{2}",tag, locationID, ParseConnections());
        }
        
        protected String ParseConnections()
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
