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
        }

        public override void ResolveEffect(float eventModifier, PCModel pcm)
        {
            int amount = item.GetAmount();
            amount = Convert.ToInt32(amount*eventModifier);
            item.SetAmount(amount);
            pcm.ModifyInventory(item, amount);
		}

        public override string ParseToString()
        {
            return String.Format("{0}#{1}", ITEM_EFFECT_TAG, item.ParseToString());
        }

        public Item GetItem()
        {
            return item;
        }

        public static bool IsValidItemEventEffect(String toTest)
        {
            String[] effectElements = toTest.Split('#');

            if (effectElements.Length != 2)
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
