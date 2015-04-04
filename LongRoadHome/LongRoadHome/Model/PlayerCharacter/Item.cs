using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class Item : Resource
    {
        private int itemID;
        private String description;
        private HashSet<int> requirements;
        private List<ItemEffect> effects;
        private List<PassiveMod> passiveEffects;

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
            throw new System.Exception("Not implemented");
        }

        public String ParseFromString()
        {
            throw new System.Exception("Not implemented");
        }
        public bool CheckReqs(ref Item[] items)
        {
            throw new System.Exception("Not implemented");
        }
        public List<PassiveMod> GetPassive()
        {
            throw new System.Exception("Not implemented");
        }

        private ItemEffect itemEffect;
        private Inventory inventory;
        private PassiveMod passiveMod;

        private ItemCatalogue itemCatalogue;

    }
}