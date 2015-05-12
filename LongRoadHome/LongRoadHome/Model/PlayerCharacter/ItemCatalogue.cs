using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class ItemCatalogue
    {
        public const String TAG = "ItemCatalogue";
        private List<Item> items;
        private Random rnd = new Random();
        private List<int> ids;

        /// <summary>
        /// Constructor for an item catalogue from a String
        /// </summary>
        /// <param name="toParse">The String to parse to an Item catalogue</param>
        public ItemCatalogue(String toParse)
        {
            items = new List<Item>();
            ids = new List<int>();

            String[] catalogueElements = toParse.Split(';');
            if (catalogueElements.Length > 1)
            {
                for (int i = 1; i < catalogueElements.Length; i++)
                {
                    Item temp = new Item(catalogueElements[i]);
                    items.Add(temp);
                    ids.Add(temp.GetID());
                }
            }
        }

        /// <summary>
        /// Get any random item from item catalogue
        /// </summary>
        /// <returns>An item from the catalogue</returns>
        public Item GetRandomItem()
        {
            int i = rnd.Next(items.Count);
            return (Item)items[i].Clone();
        }

        /// <summary>
        /// Get a random item with an id between the min and max
        /// Will automatically restrict max and min to be between the max and min indexes
        /// </summary>
        /// <param name="min">Minimum ID</param>
        /// <param name="max">Maximum ID</param>
        /// <returns>An item with an id between min and max</returns>
        public Item GetRandomItem(int min, int max)
        {
            int rndMin = min, rndMax = max;
            if (min > max)
            {
                rndMin = max;
                rndMax = min;
            }

            if (max > items.Count)
            {
                rndMax = ids.Count;
            }
            
            if (min < 0)
            {
                rndMin = 0;
            }
            
            int i = rnd.Next(rndMin, rndMax);
            int id = ids[i];
            
            if (id > max)
            {
                while (id > max && i > 0)
                {
                    i--;
                    id = ids[i];
                }
            }
            else if (id < min)
            {
                while (id < min && i < ids.Count)
                {
                    i++;
                    id = ids[i];
                }
            }

            return (Item)items[i].Clone();
        }

        /// <summary>
        /// Gets an item from the catalogue based on it's ID
        /// Null is returned if item does not exist
        /// </summary>
        /// <param name="itemID">The id of the item to get</param>
        /// <returns>The item with the ID passed</returns>
        public Item GetItem(int itemID)
        {
            foreach(Item item in items)
            {
                if (item.GetID() == itemID)
                {
                    return (Item)item.Clone();
                }
            }
            return null;
        }

        /// <summary>
        /// Checks if an item catalogue string is a valid item catalogue
        /// </summary>
        /// <param name="toTest">The string to test</param>
        /// <returns>bool representing if the string is a valid item catalogue or not</returns>
        public static bool IsValidItemCatalogue(String toTest)
        {
            List<int> idList = new List<int>();
            String[] catalogueElements = toTest.Split(';');
            if (catalogueElements[0] != TAG)
            {
                return false;
            }
            if (catalogueElements.Length > 1)
            {
                for (int i = 1; i < catalogueElements.Length; i++)
                {
                    if (!Item.IsValidItem(catalogueElements[i]))
                    {
                        return false;
                    }
                    var itemElements = catalogueElements[i].Split(',');
                    var idElements = itemElements[0].Split(':');
                    var amountElements = itemElements[2].Split(':');
                    int currID = Convert.ToInt32(idElements[1]);
                    int currAmount = Convert.ToInt32(amountElements[1]);

                    if (idList.Contains(currID))
                    {
                        return false;
                    }
                    else if (currAmount != 1)
                    {
                        return false;
                    }
                    idList.Add(currID);
                }
            }
            return true;
        }
    }
}
