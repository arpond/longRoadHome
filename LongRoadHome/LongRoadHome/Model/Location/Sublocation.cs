using System;
using System.Collections.Generic;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public abstract class Sublocation
    {
        protected int sublocationID, maxItems, maxAmount;
        protected String imagePath;
        protected bool scavenged;
        public const String TAG = "Sublocation";
        private Random rnd = new Random();
        
        public abstract String ParseToString();
        public abstract Sublocation CreateSublocation(int sublocID, int maxItems, int maxAmount);
        public abstract Sublocation CreateSublocation(String toParse);

        /// <summary>
        /// Provides default implementation for scavange
        /// </summary>
        /// <param name="possibleItems">The items that can be found</param>
        /// <returns>List of items found</returns>
        public virtual List<Item> Scavenge(List<Item> possibleItems)
        {
            int numOfItems, amount, itemIndex;
            var itemsFound = new List<Item>();
            if (!scavenged)
            {
                numOfItems = rnd.Next(1, maxItems);
                for (int i = 0; i < numOfItems; i++)
                {
                    amount = rnd.Next(1, maxAmount);
                    itemIndex = rnd.Next(possibleItems.Count);

                    if (possibleItems.Count == 0)
                    {
                        break;
                    }

                    var selectedItem = possibleItems[itemIndex] as Item;
                    var item = selectedItem.Clone() as Item;

                    item.SetAmount(amount);
                    possibleItems.Remove(selectedItem);
                    itemsFound.Add(item);
                }
                scavenged = true;
            }
            return itemsFound;
        }

        /// <summary>
        /// Accessor method for scavenged status
        /// </summary>
        /// <returns>If scavenged is true or false</returns>
        public bool GetScavenged()
        {
            return this.scavenged;
        }
        
        /// <summary>
        /// Mutator which sets scavenged to true
        /// </summary>
        public void SetScavenged()
        {
            scavenged = true;
        }
        
        /// <summary>
        /// Accessor method for sub location ID
        /// </summary>
        /// <returns>The sublocation ID</returns>
        public int GetSublocationID()
        {
            return this.sublocationID;
        }
        
        /// <summary>
        /// Accessor method for image path
        /// </summary>
        /// <returns>The image path</returns>
        public String GetImagePath()
        {
            return this.imagePath;
        }

        /// <summary>
        /// Accessor method for max items
        /// </summary>
        /// <returns>The maximum number of items</returns>
        public int GetMaxItems()
        {
            return this.maxItems;
        }

        /// <summary>
        /// Accessor method for max amount of each item
        /// </summary>
        /// <returns>The maximum amount of each item</returns>
        public int GetMaxAmount()
        {
            return this.maxAmount;
        }

    }
}
