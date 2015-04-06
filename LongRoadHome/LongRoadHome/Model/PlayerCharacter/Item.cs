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

        /// <summary>
        /// Standard Constructor for an Item
        /// </summary>
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

        /// <summary>
        /// Generates an Item based on the string input
        /// Check the string is valid before calling this function
        /// </summary>
        /// <param name="toParse">String to be converted into a PC</param>
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

        /// <summary>
        /// Accessor method for amount
        /// </summary>
        /// <returns>Returns the amount</returns>
        public override int GetAmount()
        {
            return this.amount;
        }
        
        /// <summary>
        /// Mutator method for the amount
        /// </summary>
        /// <param name="amount">The value to set the amount to</param>
        public override void SetAmount(int amount)
        {
            this.amount = amount;
        }
        
        /// <summary>
        /// Accessor method for the item name
        /// </summary>
        /// <returns>The item name</returns>
        public override String GetName()
        {
            return this.name;
        }

        /// <summary>
        /// Accessor method for the item id
        /// </summary>
        /// <returns>The item ID</returns>
        public int GetID()
        {
            return this.itemID;
        }

        /// <summary>
        /// Accessor method for the Description
        /// </summary>
        /// <returns>The item description</returns>
        public String GetDescription()
        {
            return this.description;
        }
        
        /// <summary>
        /// Accessor method for the item's active effects
        /// </summary>
        /// <returns>List of the Item's Active effects</returns>
        public List<ActiveEffect> GetActiveEffects()
        {
            return this.activeEffects;
        }

        /// <summary>
        /// Accessor method for the item's passive effects
        /// </summary>
        /// <returns>List of the Item's Passive Effects</returns>
        public List<PassiveEffect> GetPassiveEffects()
        {
            return this.passiveEffects;
        }

        /// <summary>
        /// Checks if the item has any active effects
        /// </summary>
        /// <returns>Bool representing if there are any active effects</returns>
        public bool HasActiveEffect()
        {
            return activeEffects.Count > 0;
        }

        /// <summary>
        /// Checks if the item has any passive effects
        /// </summary>
        /// <returns>Bool representing if there are any passive effects</returns>
        public bool HasPassiveEffect()
        {
            return passiveEffects.Count > 0;
        }

        /// <summary>
        /// Checks if the item has any requirements
        /// </summary>
        /// <returns>Bool representing if there are any requirements</returns>
        public bool HasRequirements()
        {
            return requirements.Count > 0;
        }

        /// <summary>
        /// Checks if the items IDs passed meet the requirements of this Item
        /// </summary>
        /// <param name="itemIDs">HashSet of item IDs</param>
        /// <returns>If the requirements are met</returns>
        public bool CheckReqs(HashSet<int> itemIDs)
        {
            if (requirements.Count == 0)
            {
                return true;
            }

            return requirements.IsSubsetOf(itemIDs);
        }

        /// <summary>
        /// Parses this item to String suitable for saving
        /// </summary>
        /// <returns>String representing this item</returns>
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

        /// <summary>
        /// Checks if a string is a valid Item string
        /// </summary>
        /// <param name="toTest">The string to test</param>
        /// <returns>If the string is valid or invalid</returns>
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
                                if (!ActiveEffect.IsValidActiveEffect(itemElement[i] + ":" + itemElement[i + 1] + ":" + itemElement[i + 2]))
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
                                if (!PassiveEffect.IsValidPassiveEffect(itemElement[i] + ":" + itemElement[i + 1] + ":" + itemElement[i + 2]))
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

        /// <summary>
        /// Two items are equal if they have the same item ID
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>If the obj and this item are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Item i = (Item)obj;
            return itemID == i.itemID;
        }

    }
}