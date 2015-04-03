using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class DummyLocation
    {
        private List<DummyLocation> connections;
        private int locationID;

        public List<DummyLocation> GetConnections()
        {
            throw new System.Exception("Not implemented");
        }
        public void SetConnections(ref List<DummyLocation> connections)
        {
            throw new System.Exception("Not implemented");
        }
        public virtual String ParseToString()
        {
            throw new System.Exception("Not implemented");
        }
        public virtual void ParseFromString(ref String toParse)
        {
            throw new System.Exception("Not implemented");
        }

        private LocationModel locationModel;

    }
}
