using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events {
	public class ItemEventEffect : EventEffect  {
		private Item item;
        public const String ITEM_EFFECT_TAG = "ItemEventEffect";

        public ItemEventEffect()
        {
            item = new Item("ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements");
        }

        public ItemEventEffect(String toParse)
        {
            String[] effectElemnts = toParse.Split('#');
            if (Item.IsValidItem(effectElemnts[1]))
            {
                item = new Item(effectElemnts[1]);
            }
            result = effectElemnts[2];
        }

        /// <summary>
        /// Resolves the IEE and applies it to the PC
        /// </summary>
        /// <param name="eventModifier">Event modifier for item amount</param>
        /// <param name="pcm">The PC model to apply to</param>
        public override void ResolveEffect(double eventModifier, PCModel pcm)
        {
            int amount = item.GetAmount();
            amount = Convert.ToInt32(amount*eventModifier);
            item.SetAmount(amount);
            if (amount < 0)
            {
                int remove = Math.Abs(amount);
                pcm.RemoveRandomItemFromInventory(remove);
            }
            else
            {
                pcm.ModifyInventory(item, amount);
            }
		}

        /// <summary>
        /// Parses the IEE to a string
        /// </summary>
        /// <returns>The parsed IEE</returns>
        public override string ParseToString()
        {
            return String.Format("{0}#{1}#{2}", ITEM_EFFECT_TAG, item.ParseToString(), result);
        }

        /// <summary>
        /// Accessor method for the item
        /// </summary>
        /// <returns>The item</returns>
        public Item GetItem()
        {
            return item;
        }

        /// <summary>
        /// Checks if the string is a valid IEE
        /// </summary>
        /// <param name="toTest">The string to test</param>
        /// <returns>If the string is valid</returns>
        public static bool IsValidItemEventEffect(String toTest)
        {
            String[] effectElements = toTest.Split('#');

            if (effectElements.Length != 3)
            {
                return false;
            }
            if (effectElements[0] != ITEM_EFFECT_TAG)
            {
                return false;
            }
            if (!Item.IsValidItem(effectElements[1]))
            {
                return false;
            }
            return true;
        }
	}

}
