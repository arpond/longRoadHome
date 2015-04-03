using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{

    public abstract class Resource
    {
        protected int amount;
        protected String name;

        public abstract int GetAmount();
        public abstract void SetAmount(int amount);
        public abstract String GetName();
        public abstract String ParseToString();
    }
}
