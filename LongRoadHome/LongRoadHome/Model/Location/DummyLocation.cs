using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class DummyLocation
    {
        private ArrayList<DummyLocation> connections;
        private int locationID;

        public ArrayList<DummyLocation> GetConnections()
        {
            throw new System.Exception("Not implemented");
        }
        public void SetConnections(ref ArrayList<DummyLocation> connections)
        {
            throw new System.Exception("Not implemented");
        }
        public String ParseToString()
        {
            throw new System.Exception("Not implemented");
        }
        public void ParseFromString(ref String toParse)
        {
            throw new System.Exception("Not implemented");
        }

        private LocationModel locationModel;

    }
}
