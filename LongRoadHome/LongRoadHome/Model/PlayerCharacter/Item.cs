using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class Item : Resource
    {
        private int itemID;
        private String description;
        private HashSet<int> requirements;
        private List<ActiveEffect> activeEffects;
        private List<PassiveEffect> passiveEffects;

        public Item()
        {
            itemID = 1;
            name = "TestItem";
            amount = 1;
            description = "test item 1";
            requirements = new HashSet<int>();
            activeEffects = new List<ActiveEffect>();
            passiveEffects = new List<PassiveEffect>();
        }

        public Item(String toParse)
        {
            requirements = new HashSet<int>();
            activeEffects = new List<ActiveEffect>();
            passiveEffects = new List<PassiveEffect>();

            String[] itemElements = toParse.Split(',');
            foreach (String curr in itemElements)
            {
                String[] itemElement = curr.Split(':');
                switch (itemElement[0])
                {
                    case "ID": 
                        itemID = Convert.ToInt32(itemElement[1]);
                        break;
                    case "Name":
                        name = itemElement[1];
                        break;
                    case "Amount":
                        amount = Convert.ToInt32(itemElement[1]);
                        break;
                    case "Description":
                        description = itemElement[1];
                        break;
                    case "ActiveEffect": 
                        if (itemElement.Length > 1)
                        {
                            for(int i = 1;i<itemElement.Length; i=i+3)
                            {
                                activeEffects.Add(new ActiveEffect(itemElement[i] + ":" + itemElement[i+1] + ":" + itemElement[i+2]));
                            }
                        }
                        break;
                    case "PassiveEffect": 
                        if (itemElement.Length > 1)
                        {
                            for (int i = 1; i < itemElement.Length; i = i + 3)
                            {
                                passiveEffects.Add(new PassiveEffect(itemElement[i] + ":" + itemElement[i+1] + ":" + itemElement[i+2]));
                            }
                        }
                        break;
                    case "Requirements": 
                        if (itemElement.Length > 1)
                        {
                            foreach(String id in itemElement)
                            {
                                int reqID;
                                if (int.TryParse(id, out reqID))
                                {
                                    requirements.Add(reqID);
                                }
                            }
                        }
                        break;
                    //default: throw new FormatException();
                }
            }
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

        public int GetID()
        {
            return this.itemID;
        }

        public String GetDescription()
        {
            return this.description;
        }
        
        public List<ActiveEffect> GetActiveEffects()
        {
            return this.activeEffects;
        }

        public List<PassiveEffect> GetPassiveEffects()
        {
            return this.passiveEffects;
        }

        public bool hasActiveEffect()
        {
            return activeEffects.Count > 0;
        }

        public bool hasPassiveEffect()
        {
            return passiveEffects.Count > 0;
        }

        public bool hasRequirements()
        {
            return requirements.Count > 0;
        }

        public bool CheckReqs(HashSet<int> itemIDs)
        {
            if (requirements.Count == 0)
            {
                return true;
            }

            return requirements.IsSubsetOf(itemIDs);
        }

        public override String ParseToString()
        {
            String parsed = "";
            parsed += "ID:" + itemID + ",Name:" + name + ",Amount:" + amount + ",Description:" + description + ",";

            parsed += "ActiveEffect";

            foreach (Effect active in activeEffects)
            {
                parsed += ":" + active.ParseToString();
            }

            parsed += ",PassiveEffect";

            foreach (PassiveEffect passive in passiveEffects)
            {
                parsed += ":" + passive.ParseToString();
            }

            parsed += ",Requirements";

            foreach (int id in requirements)
            {
                parsed += ":" + id;
            }

            return parsed;
        }

        public static bool IsValidItem(String toTest)
        {
            String[] itemElements = toTest.Split(',');
            foreach (String curr in itemElements)
            {
                String[] itemElement = curr.Split(':');
                switch (itemElement[0])
                {
                    case "ID":
                    case "Amount" :
                        if (itemElement.Length < 2 || itemElement.Length > 2)
                        {
                            return false;
                        }
                        int val;
                        if (int.TryParse(itemElement[1], out val))
                        {
                            if (val < 0)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case "Name":
                    case "Description":
                        if (itemElement.Length < 2 || itemElement.Length > 2 || itemElement[1] == "")
                        {
                            return false;
                        }
                        break;
                    case "ActiveEffect":
                        if (itemElement.Length > 1)
                        {
                            if (itemElement.Length % 3 != 1 || itemElement[1] == "")
                            {
                                return false;
                            }
                            for (int i = 1; i < itemElement.Length; i = i + 3)
                            {
                                if (!ActiveEffect.isValidActiveEffect(itemElement[i] + ":" + itemElement[i + 1] + ":" + itemElement[i + 2]))
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case "PassiveEffect":
                        if (itemElement.Length > 1)
                        {
                            if (itemElement.Length % 3 != 1 || itemElement[1] == "")
                            {
                                return false;
                            }
                            for (int i = 1; i < itemElement.Length; i = i + 3)
                            {
                                if (!PassiveEffect.isValidPassiveEffect(itemElement[i] + ":" + itemElement[i + 1] + ":" + itemElement[i + 2]))
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case "Requirements":
                        if (itemElement.Length > 1)
                        {
                            HashSet<int> temp = new HashSet<int>();
                            for (int i = 1; i < itemElement.Length; i++ )
                            {
                                String id = itemElement[i];
                                int reqID;
                                if (int.TryParse(id, out reqID))
                                {
                                    if (reqID <= 0)
                                    {
                                        return false;
                                    }
                                    temp.Add(reqID);
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            if (temp.Count != itemElement.Length - 1)
                            {
                                return false;
                            }
                        }
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }

    }
}