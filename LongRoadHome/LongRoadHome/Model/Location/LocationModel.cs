using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class LocationModel
    {
        private ArrayList<Location> visitedLocation;
        private ArrayList<DummyLocation> unvistedLocation;
        private Location currentLocation;
        private Sublocation currentSublocation;

        public LocationModel(ref String visitedLocs, ref String unvisitedLocs, ref String currLoc, ref String currSLoc)
        {
            throw new System.Exception("Not implemented");
        }
        public String ParseToString()
        {
            throw new System.Exception("Not implemented");
        }
        public Location ReplaceDummyLocation(ref DummyLocation dummy)
        {
            throw new System.Exception("Not implemented");
        }
        public void GenerateDummyLocations(ref int maxBranchFactor)
        {
            throw new System.Exception("Not implemented");
        }
        public Sublocation GetSubLocation()
        {
            throw new System.Exception("Not implemented");
        }
        public String GetSublocType()
        {
            throw new System.Exception("Not implemented");
        }
        public void ChangeSubLocation(ref int subLocationID)
        {
            throw new System.Exception("Not implemented");
        }
        public void ChangeLocation(ref int locationID)
        {
            throw new System.Exception("Not implemented");
        }
        public ArrayList<item> Scavenge()
        {
            throw new System.Exception("Not implemented");
        }
        public bool IsScavenged()
        {
            throw new System.Exception("Not implemented");
        }

        private DummyLocation dummyLocation;
        private Sublocation sublocation;

        private uk.ac.dundee.arpond.longRoadHome.Model.GameState gameState;

    }
}