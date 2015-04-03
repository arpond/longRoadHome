using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public abstract class Sublocation
    {
        private bool scavenged;
        private int sublocationID;
        private String imagePath;

        public ArrayList<Item> Scavenge()
        {
            throw new System.Exception("Not implemented");
        }
        public bool GetScavenged()
        {
            return this.scavenged;
        }
        public void SetScavenged()
        {
            throw new System.Exception("Not implemented");
        }
        public int GetSublocationID()
        {
            return this.sublocationID;
        }
        public void SetSublocationID(ref int sublocationID)
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
        public String GetImagePath()
        {
            return this.imagePath;
        }
        public Sublocation CreateSublocation()
        {
            throw new System.Exception("Not implemented");
        }

        private Location location;
        private LocationModel locationModel;

    }
}
