using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{

    public abstract class Resource
    {
        public int amount { get; set; }
        public string name { get; set; }

        public abstract int GetAmount();
        public abstract void SetAmount(int amount);
        public abstract String GetName();
        public abstract String ParseToString();
    }
}
