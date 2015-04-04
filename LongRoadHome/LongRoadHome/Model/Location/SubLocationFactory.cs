using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class SubLocationFactory
    {
        private Dictionary<int, Sublocation> registeredSublocations;

        public Sublocation CreateSubLocation(ref int subTypeID)
        {
            throw new System.Exception("Not implemented");
        }
        public void RegisterSubLocation(ref int subTypeID, ref object subLocation_sl)
        {
            throw new System.Exception("Not implemented");
        }

        private Residential residential;
        private Commercial commercial;
        private Civic civic;
        private Location location;

    }
}
