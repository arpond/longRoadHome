using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class Location : DummyLocation
    {
        private bool visited;
        private Dictionary<int, Sublocation> sublocations;
        private Sublocation currentSubLocation;

        public void TriggerEvent()
        {
            throw new System.Exception("Not implemented");
        }
        public void GenerateSubLocations(ref int min, ref int max)
        {
            throw new System.Exception("Not implemented");
        }
        public bool GetVisited()
        {
            return this.visited;
        }
        public void SetVisited()
        {
            throw new System.Exception("Not implemented");
        }
        public Sublocation GetCurrentSubLocation()
        {
            return this.currentSubLocation;
        }
        public void SetCurrentSubLocation(ref int sublocationID)
        {
            throw new System.Exception("Not implemented");
        }
        public override String ParseToString()
        {
            throw new System.Exception("Not implemented");
        }
        public override void ParseFromString(ref String toParse)
        {
            throw new System.Exception("Not implemented");
        }

        private Sublocation sublocation;
        private SubLocationFactory subLocationFactory;

    }
}
