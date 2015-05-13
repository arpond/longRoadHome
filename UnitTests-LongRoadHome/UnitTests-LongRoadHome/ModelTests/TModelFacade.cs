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

        [TestInitialize]
        public void Setup()
        {
            // PC Model
            List<Item> items = new List<Item>();
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

            inventory += "#" + items[0].ParseToString() + "#" + items[3].ParseToString() + "#" + items[2].ParseToString() + "#" + items[5].ParseToString() + "#" + items[7].ParseToString();

            pcm = new PCModel(pc, inventory, itemCatalogue);

            // Event Model
            String validPREE = PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":10:20";
            String validIEE = ItemEventEffect.ITEM_EFFECT_TAG + "#" + items[1].ParseToString();
            String validOption1 = Option.TAG + ";" + "1;TestText1;EventEffects|" + validPREE + "|" + validIEE;
            String validOption2 = Option.TAG + ";" + "2;TestText2;EventEffects|" + validPREE + "|" + validIEE;
            String validOption3 = Option.TAG + ";" + "3;TestText3;EventEffects|" + validPREE + "|" + validIEE;
            String validOption4 = Option.TAG + ";" + "4;TestText4;EventEffects|" + validPREE + "|" + validIEE;

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

            usedEvents = EventModel.USED_TAG + ":1:2:6";
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

            mf.ReduceResourcesByMoveCost(gs);

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
            Assert.IsTrue(mf.CanAffordMove(gs), "Move should be possible");

            mf.ReduceResourcesByMoveCost(gs);
            Assert.IsTrue(mf.CanAffordMove(gs), "Move should be possible");
            mf.ReduceResourcesByMoveCost(gs);
            Assert.IsTrue(mf.CanAffordMove(gs), "Move should be possible");
            mf.ReduceResourcesByMoveCost(gs);
            Assert.IsTrue(mf.CanAffordMove(gs), "Move should be possible");
            mf.ReduceResourcesByMoveCost(gs);

            Assert.IsFalse(mf.CanAffordMove(gs), "Move should not be possible");
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
            Assert.IsTrue(mf.ScavangeSubLocation(gs), "Scavenging should be succesful");
            Assert.IsTrue(workingLM.IsScavenged(), "Current sublocation should now be scavenged");
            Assert.AreNotEqual(workingInv.ParseToString(), temp.ParseToString(), "Inventory should have changed");

            int newTotal = 0;
            foreach(Item item in workingInv.GetInventory())
            {
                newTotal += item.GetAmount();
            }

            Assert.IsTrue(oldTotal < newTotal, "There should be more items in teh inventory");
        }
    }
}
