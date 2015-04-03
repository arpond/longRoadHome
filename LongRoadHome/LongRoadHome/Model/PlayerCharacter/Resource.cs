using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{

    public abstract class Resource
    {
        private int amount;
        private String name;

        public Resource(int amount, String name)
        {
            this.amount = amount;
            this.name = name;
        }

        public int GetAmount()
        {
            return this.amount;
        }
        public void SetAmount(int amount)
        {
            this.amount = amount;
        }
        public String GetName()
        {
            return this.name;
        }
        public String ParseToString()
        {
            throw new System.Exception("Not implemented");
        }

    }
}
