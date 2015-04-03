using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class PrimaryResource : Resource
    {
        public PrimaryResource(int amount, String name)
        {
            this.amount = amount;
            this.name = name;
        }

        public override int GetAmount()
        {
            return this.amount;
        }
        public override void SetAmount(int amount)
        {
            this.amount = amount;
        }
        public override String GetName()
        {
            return this.name;
        }
        public override String ParseToString()
        {
            return name + ":" + amount;
        }
    }
}
