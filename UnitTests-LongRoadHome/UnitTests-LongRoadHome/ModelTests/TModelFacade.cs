using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Model;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using System.Collections.Generic;
namespace UnitTests_LongRoadHome.ModelTests
{
    [TestClass]
    public class TModelFacade
    {
        LocationModel lm;
        EventModel em;
        PCModel pcm;
        DiscoveryModel dm;
        GameState gs;
        String  pc, inventory, itemCatalogue,
                usedEvents, currentEvent, eventCatalogue,
                discovered, discoveryCatalogue,
                visitedLocs, unvisitedLocs, currLoc, currSLoc;
        ModelFacade mf = new ModelFacade();
        List<Location> locations = new List<Location>();
        List<DummyLocation> dummyLocations = new List<DummyLocation>();
        List<Item> items;

        [TestInitialize]
        public void Setup()
        {
            // PC Model
            items = new List<Item>();

            String itemStr1 = "ID:21,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements";
            String itemStr2 = "ID:22,Name:TestItem,Amount:1,Description:test item 2,ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":10" + ",PassiveEffect,Requirements:1";
            String itemStr3 = "ID:23,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements:1:2";
            String itemStr4 = "ID:24,Name:TestItem,Amount:1,Description:test item 4,ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":30:" + ActiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":10" + ",PassiveEffect,Requirements:10";

            itemCatalogue = ItemCatalogue.TAG;
            inventory = Inventory.TAG;
            pc = PlayerCharacter.HEALTH + ":80:1," + PlayerCharacter.HUNGER + ":50:1,"
             + PlayerCharacter.THIRST + ":60:1," + PlayerCharacter.SANITY + ":70:1";
            for (int i = 1; i < 21; i++)
            {
                Item tmp = new Item(StringMaker.makeItemStr(i));
                items.Add(tmp);
                itemCatalogue += ";" + tmp.ParseToString();
            }

            itemCatalogue += ";" + itemStr1 + ";" + itemStr2 + ";" + itemStr3 + ";" + itemStr4;

            items.Add(new Item(itemStr1));
            items.Add(new Item(itemStr2));
            items.Add(new Item(itemStr3));
            items.Add(new Item(itemStr4));

            inventory += "#" + items[0].ParseToString() + "#" + items[3].ParseToString() + "#" + items[2].ParseToString() + "#" + items[5].ParseToString() + "#" + items[7].ParseToString() + "#" + itemStr2 + "#" + itemStr4;

            pcm = new PCModel(pc, inventory, itemCatalogue);

            // Event Model
            String validPREE = PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":10:20:Test Result";
            String validIEE = ItemEventEffect.ITEM_EFFECT_TAG + "#" + items[1].ParseToString() + "#Test Result";
            String validOption1 = Option.TAG + ";" + "1;TestText1;TestResult;EventEffects|" + validPREE + "|" + validIEE;
            String validOption2 = Option.TAG + ";" + "2;TestText2;TestResult;EventEffects|" + validPREE + "|" + validIEE;
            String validOption3 = Option.TAG + ";" + "3;TestText3;TestResult;EventEffects|" + validPREE + "|" + validIEE;
            String validOption4 = Option.TAG + ";" + "4;TestText4;TestResult;EventEffects|" + validPREE + "|" + validIEE;

            List<Event> events = new List<Event>();
            eventCatalogue = EventCatalogue.TAG;
            for (int i= 1; i<21; i++)
            {
                String evt = Event.TAG + "_" + i + "_Type_Test text_EventOptions*" + validOption1 + "*" + validOption2 + "*" + validOption3 + "*" + validOption4;
                if(!Event.IsValidEvent(evt))
                {
                    String wrong = evt;
                    Event.IsValidEvent(wrong);
                }
                
                Event temp = new Event(evt);
                events.Add(temp);
                eventCatalogue += "^" + evt;
            }

            usedEvents = EventModel.USED_TAG + ":1:2:8";
            currentEvent = events[7].ParseToString();
            em = new EventModel(usedEvents, eventCatalogue, currentEvent);

            // Location Model
            Residential res = new Residential(1, 3, 5);
            Commercial com = new Commercial(2, 4, 7);
            Civic civ = new Civic(3, 6, 3);

            

            visitedLocs = "VisitedLocations";
            unvisitedLocs = "UnvisitedLocations";

            for (int i = 1; i < 21; i++)
            {
                int j = i +20;
                String loc;
                String dloc;
                if(i+1 < 21)
                {
                    if (i-1 > 0)
                    {
                        loc = "Type:Location,ID:" + i + ",Connections:" + (i + 1) + ":" + (i - 1) + ",Visited:True,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1";
                        dloc = "Type:DummyLocation,ID:" + j + ",Connections:" + (j - 1) + ":" + (j + 1);
                    }
                    else
                    {
                        loc = "Type:Location,ID:"+ i + ",Connections:"+ (i+1) + ",Visited:True,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1";
                        dloc = "Type:DummyLocation,ID:" + j + ",Connections:" + (j - 1) + ":" + (j + 1);
                    }
                }
                else
                {
                    loc = "Type:Location,ID:" + i + ",Connections:" + (i + 1) + ":" + (i - 1) + ",Visited:True,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1";
                    dloc = "Type:DummyLocation,ID:" + j + ",Connections:" + (j - 1);
                }

                Location temp = new Location(loc);
                DummyLocation dTemp = new DummyLocation(dloc);
                locations.Add(temp);
                dummyLocations.Add(dTemp);

                visitedLocs += "#" + loc;
                unvisitedLocs += "#" + dloc;
            }

            currLoc = "19";
            currSLoc = "1";

            lm = new LocationModel(visitedLocs, unvisitedLocs, currLoc, currSLoc);


            // Discovery Model

            List<Discovery> discoveries = new List<Discovery>();
            discoveryCatalogue = DiscoveryCatalogue.TAG;
            for (int i = 1; i < 21; i++)
            {
                String disc = Discovery.TAG + ":" + i + ":Text:" + i;
                Discovery temp = new Discovery(disc);
                discoveries.Add(temp);
                discoveryCatalogue += "#" + disc;
            }

            discovered = DiscoveryModel.DISCOVERED_TAG + ":1:2:7:8";

            dm = new DiscoveryModel(discovered, discoveryCatalogue);

            // Game State
            gs = new GameState(pc, inventory, itemCatalogue,
                usedEvents, currentEvent, eventCatalogue,
                discovered, discoveryCatalogue,
                visitedLocs, unvisitedLocs, currLoc, currSLoc);
        }



        [TestCategory("ModelFacade"), TestCategory("Model"), TestMethod()]
        public void ModelFacade_ReduceResourcesByMoveCost()
        {
            PCModel workingPCM = gs.GetPCM();
            PlayerCharacter workingPC = workingPCM.GetPC();

            Assert.AreEqual(80, workingPC.GetResource(PlayerCharacter.HEALTH), "Health should be 80");
            Assert.AreEqual(50, workingPC.GetResource(PlayerCharacter.HUNGER), "Hunger should be 50");
            Assert.AreEqual(60, workingPC.GetResource(PlayerCharacter.THIRST), "Thirst should be 60");
            Assert.AreEqual(70, workingPC.GetResource(PlayerCharacter.SANITY), "Sanity should be 70");

            mf.ReduceResourcesByMoveCost(gs, ModelFacade.LOCATION_MOVE_COST);

            Assert.AreEqual(80, workingPC.GetResource(PlayerCharacter.HEALTH), "Health should stay the same");
            Assert.AreEqual(40, workingPC.GetResource(PlayerCharacter.HUNGER), "Hunger should be 40");
            Assert.AreEqual(50, workingPC.GetResource(PlayerCharacter.THIRST), "Thirst should be 50");
            Assert.AreEqual(70, workingPC.GetResource(PlayerCharacter.SANITY), "Sanity should stay the same");
        }

        [TestCategory("ModelFacade"), TestCategory("Model"), TestMethod()]
        public void ModelFacade_CanAffordMove()
        {
            PCModel workingPCM = gs.GetPCM();
            PlayerCharacter workingPC = workingPCM.GetPC();

            Assert.AreEqual(80, workingPC.GetResource(PlayerCharacter.HEALTH), "Health should be 80");
            Assert.AreEqual(50, workingPC.GetResource(PlayerCharacter.HUNGER), "Hunger should be 50");
            Assert.AreEqual(60, workingPC.GetResource(PlayerCharacter.THIRST), "Thirst should be 60");
            Assert.AreEqual(70, workingPC.GetResource(PlayerCharacter.SANITY), "Sanity should be 70");
            Assert.IsTrue(mf.CanAffordMove(gs, ModelFacade.LOCATION_MOVE_COST), "Move should be possible");

            mf.ReduceResourcesByMoveCost(gs, ModelFacade.LOCATION_MOVE_COST);
            Assert.IsTrue(mf.CanAffordMove(gs, ModelFacade.LOCATION_MOVE_COST), "Move should be possible");
            mf.ReduceResourcesByMoveCost(gs, ModelFacade.LOCATION_MOVE_COST);
            Assert.IsTrue(mf.CanAffordMove(gs, ModelFacade.LOCATION_MOVE_COST), "Move should be possible");
            mf.ReduceResourcesByMoveCost(gs, ModelFacade.LOCATION_MOVE_COST);
            Assert.IsTrue(mf.CanAffordMove(gs, ModelFacade.LOCATION_MOVE_COST), "Move should be possible");
            mf.ReduceResourcesByMoveCost(gs, ModelFacade.LOCATION_MOVE_COST);

            Assert.IsFalse(mf.CanAffordMove(gs, ModelFacade.LOCATION_MOVE_COST), "Move should not be possible");
            Assert.AreEqual(80, workingPC.GetResource(PlayerCharacter.HEALTH), "Health should be 80");
            Assert.AreEqual(10, workingPC.GetResource(PlayerCharacter.HUNGER), "Hunger should be 10");
            Assert.AreEqual(20, workingPC.GetResource(PlayerCharacter.THIRST), "Thirst should be 20");
            Assert.AreEqual(70, workingPC.GetResource(PlayerCharacter.SANITY), "Sanity should be 70");
        }

        [TestCategory("ModelFacade"), TestCategory("Model"), TestMethod()]
        public void ModelFacade_MoveToLocation()
        {
            LocationModel workingLM = gs.GetLM();
            Assert.AreEqual(locations[18].ParseToString(), workingLM.GetCurentLocation().ParseToString(), "Location should be location 19");
            Assert.IsTrue(mf.ChangeLocation(gs, 20), "Moving to 20 should be successful");
            Assert.AreEqual(locations[19].ParseToString(), workingLM.GetCurentLocation().ParseToString(), "Location should now be location 20");
            Assert.IsFalse(mf.ChangeLocation(gs, 7), "Moving to 7 should be unsuccessful");
            Assert.AreEqual(locations[19].ParseToString(), workingLM.GetCurentLocation().ParseToString(), "Location should still be 20");
            Assert.IsFalse(mf.ChangeLocation(gs, 20), "Moving to 20 should be unsuccessful");
            Assert.AreEqual(locations[19].ParseToString(), workingLM.GetCurentLocation().ParseToString(), "Location should still be location 20");
            Assert.IsTrue(mf.ChangeLocation(gs, 21), "Moving to 21 should be successful");
            Assert.AreEqual(21, workingLM.GetCurentLocation().GetLocationID(), "Location should now be location 21");
        }

        [TestCategory("ModelFacade"), TestCategory("Model"), TestMethod()]
        public void ModelFacade_MoveToSublocation()
        {
            LocationModel workingLM = gs.GetLM();
            Assert.AreEqual(1, workingLM.GetSubLocation().GetSublocationID(), "Should be at sublocation 1");
            Assert.IsTrue(mf.ChangeSubLocation(gs, 2), "Should be able to goto sublocation 2");
            Assert.AreEqual(2, workingLM.GetSubLocation().GetSublocationID(), "Should be at sublocation 2");
            Assert.IsFalse(mf.ChangeSubLocation(gs, 3), "Should not be able to goto sublocation 3");
            Assert.AreEqual(2, workingLM.GetSubLocation().GetSublocationID(), "Should still be at sublocation 2");

            Assert.IsTrue(mf.ChangeLocation(gs, 20), "Moving to 20 should be successful");
            Assert.IsNull(workingLM.GetSubLocation(), "Sublocation should be null");
            Assert.IsTrue(mf.ChangeSubLocation(gs, 1), "Should be able to goto sublocation 1");
            Assert.AreEqual(1, workingLM.GetSubLocation().GetSublocationID(), "Should be at sublocation 1");
            Assert.IsTrue(mf.ChangeSubLocation(gs, 2), "Should be able to goto sublocation 2");
            Assert.AreEqual(2, workingLM.GetSubLocation().GetSublocationID(), "Should be at sublocation 2");
            Assert.IsFalse(mf.ChangeSubLocation(gs, 10), "Should not be able to goto sublocation 10");
        }

        [TestCategory("ModelFacade"), TestCategory("Model"), TestMethod()]
        public void ModelFacade_ScavengeSublocation()
        {
            LocationModel workingLM = gs.GetLM();
            PCModel pcm = gs.GetPCM();
            Inventory temp = new Inventory(inventory);
            Inventory workingInv = pcm.GetInventory();

            int oldTotal = 0;
            foreach (Item item in workingInv.GetInventory())
            {
                oldTotal += item.GetAmount();
            }

            Assert.AreEqual(workingInv.ParseToString(), temp.ParseToString(), "Inventory should match");
            Assert.IsFalse(workingLM.IsScavenged(), "Current sublocation should not be scavenged");
            Assert.IsFalse(workingInv.IsInventoryFull(), "Inventody should not be full");
            Assert.IsTrue(mf.ScavangeSubLocation(gs).Count > 0, "Scavenging should be succesful");
            Assert.IsTrue(workingLM.IsScavenged(), "Current sublocation should now be scavenged");
            Assert.AreNotEqual(workingInv.ParseToString(), temp.ParseToString(), "Inventory should have changed");

            int newTotal = 0;
            foreach (Item item in workingInv.GetInventory())
            {
                newTotal += item.GetAmount();
            }

            Assert.IsTrue(oldTotal < newTotal, "There should be more items in the inventory");
            var tempScav = mf.ScavangeSubLocation(gs);
            Assert.IsTrue(tempScav.Count == 0, "Scavenging should fail");

            oldTotal = newTotal;
            newTotal = 0;
            foreach (Item item in workingInv.GetInventory())
            {
                newTotal += item.GetAmount();
            }
            Assert.AreEqual(oldTotal, newTotal, "no new items should have been added");
        }

        [TestCategory("ModelFacade"), TestCategory("Model"), TestMethod()]
        public void ModelFacade_UseItem()
        {
            PCModel pcm = gs.GetPCM();
            PlayerCharacter workingPC = pcm.GetPC();
            Inventory workingInv = pcm.GetInventory();
            Assert.IsTrue(workingInv.Contains(items[21]), "Inventory should contain Item 22");
            Assert.IsTrue(workingInv.Contains(items[23]), "Inventory should contain Item 24");

            int invSlot = workingInv.GetInventorySlot(items[21]);

            Assert.AreEqual(80, workingPC.GetResource(PlayerCharacter.HEALTH), "Health should be 80");
            Assert.AreEqual(50, workingPC.GetResource(PlayerCharacter.HUNGER), "Hunger should be 50");
            Assert.AreEqual(60, workingPC.GetResource(PlayerCharacter.THIRST), "Thirst should be 60");
            Assert.AreEqual(70, workingPC.GetResource(PlayerCharacter.SANITY), "Sanity should be 70");

            Assert.IsTrue(mf.UseItem(gs,invSlot), "Item 22 should be used");

            Assert.AreEqual(90, workingPC.GetResource(PlayerCharacter.HEALTH), "Health should be 90");
            Assert.AreEqual(50, workingPC.GetResource(PlayerCharacter.HUNGER), "Hunger should stay the same");
            Assert.AreEqual(60, workingPC.GetResource(PlayerCharacter.THIRST), "Thirst should stay the same");
            Assert.AreEqual(70, workingPC.GetResource(PlayerCharacter.SANITY), "Sanity should stay the same");

            Assert.IsFalse(workingInv.Contains(items[21]), "Inventory should no longer contain Item 22");

            invSlot = workingInv.GetInventorySlot(items[23]);
            Assert.IsFalse(mf.UseItem(gs, invSlot), "Should not be possible to item 24 due to lack of prerequisites");
        }

        [TestCategory("ModelFacade"), TestCategory("Model"), TestMethod()]
        public void ModelFacade_DiscardItem()
        {
            PCModel pcm = gs.GetPCM();
            PlayerCharacter workingPC = pcm.GetPC();
            Inventory workingInv = pcm.GetInventory();

            Assert.IsTrue(workingInv.Contains(items[0]), "Inventory should contain Item 1");
            Assert.IsTrue(workingInv.Contains(items[2]), "Inventory should contain Item 3");
            Assert.IsTrue(workingInv.Contains(items[3]), "Inventory should contain Item 4");
            Assert.IsTrue(workingInv.Contains(items[5]), "Inventory should contain Item 6");
            Assert.IsTrue(workingInv.Contains(items[7]), "Inventory should contain Item 8");
            Assert.IsTrue(workingInv.Contains(items[21]), "Inventory should contain Item 22");
            Assert.IsTrue(workingInv.Contains(items[23]), "Inventory should contain Item 24");

            int invSlot = workingInv.GetInventorySlot(items[2]);
            Assert.IsTrue(mf.DiscardItem(gs, invSlot), "Should be possible to discard Item 3");
            Assert.IsFalse(workingInv.Contains(items[2]), "Inventory should not contain Item 3");

            invSlot = workingInv.GetInventorySlot(items[7]);
            Assert.IsTrue(workingInv.Contains(items[7]), "Inventory should contain Item 8");
            Assert.IsTrue(mf.DiscardItem(gs, invSlot), "Should be possible to discard Item 8");
            Assert.IsFalse(workingInv.Contains(items[7]), "Inventory should not contain Item 8");

            Assert.IsFalse(mf.DiscardItem(gs, 10), "Should not be possible to discard empty slot");
        }

        [TestCategory("ModelFacade"), TestCategory("Model"), TestMethod()]
        public void ModelFacade_ItemUsable()
        {
            PCModel pcm = gs.GetPCM();
            PlayerCharacter workingPC = pcm.GetPC();
            Inventory workingInv = pcm.GetInventory();

            Assert.IsTrue(workingInv.Contains(items[21]), "Inventory should contain Item 22");
            Assert.IsTrue(workingInv.Contains(items[23]), "Inventory should contain Item 24");

            int invSlot = workingInv.GetInventorySlot(items[21]);
            Assert.IsTrue(mf.ItemUsable(gs, invSlot), "Item 22 should be usable");

            invSlot = workingInv.GetInventorySlot(items[23]);
            Assert.IsFalse(mf.ItemUsable(gs, invSlot), "Item 24 should not be usable");

            Assert.IsFalse(mf.ItemUsable(gs, 10), "Should not be possible to use empty slot");
        }

        [TestCategory("ModelFacade"), TestCategory("Model"), TestMethod()]
        public void ModelFacade_GetNewRandomEvent()
        {
            EventModel em = gs.GetEM();

            Event curr = em.GetCurrentEvent();
            Assert.AreEqual(curr.GetEventID(), em.GetCurrentEventID(), "Event ID should be the same");

            Event newEvent = mf.GetNewRandomEvent(gs);

            Assert.AreEqual(newEvent.GetEventID(), em.GetCurrentEventID(), "Event ID should be the same");
            Assert.AreNotEqual(curr.GetEventID(), em.GetCurrentEventID(), "Event ID should be different");
        }

        [TestCategory("ModelFacade"), TestCategory("Model"), TestMethod()]
        public void ModelFacade_GetNewSpecificEvent()
        {
            EventModel em = gs.GetEM();

            Event curr = em.GetCurrentEvent();
            Assert.AreEqual(curr.GetEventID(), em.GetCurrentEventID(), "Event ID should be the same");

            Event newEvent = mf.GetNewSpecificEvent(gs, 2);

            Assert.AreEqual(2, em.GetCurrentEventID(), "Event ID should be 2");
            newEvent = mf.GetNewSpecificEvent(gs, 50);
            Assert.IsNull(newEvent, "Event should be null after fetching an event that doesn't exist");
        }

        [TestCategory("ModelFacade"), TestCategory("Model"), TestMethod()]
        public void ModelFacade_ResolveEvent()
        {
            EventModel em = gs.GetEM();
            PCModel pcm = gs.GetPCM();
            PlayerCharacter workingPC = pcm.GetPC();
            Inventory workingInv = pcm.GetInventory();
            int optionSelected = 1;
            float eventModifier = 1.0f;

            Assert.AreEqual(80, workingPC.GetResource(PlayerCharacter.HEALTH), "Health should be 80");
            Assert.AreEqual(50, workingPC.GetResource(PlayerCharacter.HUNGER), "Hunger should be 50");
            Assert.AreEqual(60, workingPC.GetResource(PlayerCharacter.THIRST), "Thirst should be 60");
            Assert.AreEqual(70, workingPC.GetResource(PlayerCharacter.SANITY), "Sanity should be 70");

            Assert.IsFalse(workingInv.Contains(items[1]), "Inventory should contain Item 2");

            mf.ResolveEvent(gs, optionSelected, eventModifier);

            int newHealth = workingPC.GetResource(PlayerCharacter.HEALTH);
            int currentHealth = newHealth;
            Assert.IsTrue(newHealth >= 90 && newHealth <= 100, "Health should be increased by somewhere between 10 to 20");
            Assert.IsTrue(workingInv.Contains(items[1]), "Inventory should contain Item 2");
            Assert.AreEqual(1, workingInv.GetAmount(items[1]), "Should be one of Item 2");

            optionSelected = 10;
            mf.ResolveEvent(gs, optionSelected, eventModifier);
            newHealth = workingPC.GetResource(PlayerCharacter.HEALTH);
            Assert.AreEqual(currentHealth, newHealth, "Health should be the same as you can't select a non existent option");
            Assert.IsTrue(workingInv.Contains(items[1]), "Inventory should contain Item 2");
            Assert.AreEqual(1, workingInv.GetAmount(items[1]), "Should only be one of Item 2");

            em.FetchSpecificEvent(100);
            mf.ResolveEvent(gs, optionSelected, eventModifier);
            newHealth = workingPC.GetResource(PlayerCharacter.HEALTH);
            Assert.AreEqual(currentHealth, newHealth, "Health should be the same as you can't select an option of a null event");
            Assert.IsTrue(workingInv.Contains(items[1]), "Inventory should contain Item 2");
            Assert.AreEqual(1, workingInv.GetAmount(items[1]), "Should only be one of Item 2");

        }
    }
}
