using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class Item : Resource
    {
        private int itemID;
        private String description;
        private HashSet<Int> requirements;
        private ArrayList<ItemEffect> effects;
        private ArrayList<PassiveMod> passiveEffects;

        public String ParseFromString()
        {
            throw new System.Exception("Not implemented");
        }
        public bool CheckReqs(ref item[] items)
        {
            throw new System.Exception("Not implemented");
        }
        public ArrayList<PassiveMod> GetPassive()
        {
            throw new System.Exception("Not implemented");
        }

        private ItemEffect itemEffect;
        private Inventory inventory;
        private PassiveMod passiveMod;

        private ItemCatalogue itemCatalogue;

    }
}