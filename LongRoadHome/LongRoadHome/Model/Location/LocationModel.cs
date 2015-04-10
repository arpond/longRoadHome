using System;
using System.Collections.Generic;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class LocationModel
    {
        private List<Location> visitedLocation;
        private List<DummyLocation> unvisitedLocation;
        private Location currentLocation;
        private Sublocation currentSublocation;

        public LocationModel(String visitedLocs, String unvisitedLocs, String currLoc, String currSLoc)
        {
            visitedLocation = new List<Location>();
            unvisitedLocation = new List<DummyLocation>();

            String[] visitedElem = visitedLocs.Split('#');
            foreach(String loc in visitedElem)
            {
                Location temp = new Location(loc);
                visitedLocation.Add(temp);
            }

            String[] unvisitedElem = unvisitedLocs.Split('#');
            foreach(String loc in unvisitedElem)
            {
                DummyLocation temp = new DummyLocation(loc);
                unvisitedLocation.Add(temp);
            }

            int currID;
            if(int.TryParse(currLoc, out currID))
            {
                foreach (Location loc in visitedLocation)
                {
                    if (loc.GetLocationID() == currID)
                    {
                        currentLocation = loc;
                        break;
                    }
                }
            }

            int currSub;
            if (int.TryParse(currSLoc, out currSub))
            {
                currentLocation.GetSublocationByID(currSub);
            }
        }

        public String ParseVisitedToString()
        {
            String parsed = "VisitedLocations";
            foreach(Location visited in visitedLocation)
            {
                parsed += "#" + visited.ParseToString();
            }
            return parsed;
        }

        public String ParseUnvisitedToString()
        {
            String parsed = "UnvisitedLocations";
            foreach (DummyLocation unvisited in unvisitedLocation)
            {
                parsed += "#" + unvisited.ParseToString();
            }
            return parsed;
        }

        public String ParseCurrLocationToString()
        {
            return "" + currentLocation.GetLocationID();
        }

        public String ParseCurrSubLocToString()
        {
            return "" + currentSublocation.GetSublocationID();
        }

        public Location ReplaceDummyLocation(DummyLocation dummy, int minSize, int maxSize, int maxItems, int maxAmount)
        {
            Location location = Location.ConvertToLocation(dummy);
            location.GenerateSubLocations(minSize, maxSize, maxItems, maxAmount);
            return location;
        }

        public void GenerateDummyLocations(ref int maxBranchFactor)
        {
            throw new System.Exception("Not implemented");
        }
        public Sublocation GetSubLocation()
        {
            return this.currentSublocation;
        }
        public String GetSublocType()
        {
            return currentSublocation.GetType().ToString();
        }
        public void ChangeSubLocation(ref int subLocationID)
        {
            throw new System.Exception("Not implemented");
        }
        public void ChangeLocation(ref int locationID)
        {
            throw new System.Exception("Not implemented");
        }
        public List<Item> Scavenge()
        {
            throw new System.Exception("Not implemented");
        }
        public bool IsScavenged()
        {
            return currentSublocation.GetScavenged();
        }
    }
}