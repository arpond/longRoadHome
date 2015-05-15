using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Controller;
using uk.ac.dundee.arpond.longRoadHome.Model;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using System.Collections.Generic;

namespace UnitTests_LongRoadHome.ControllerTests
{
    [TestClass]
    public class TMainController
    {
        LocationModel lm;
        EventModel em;
        PCModel pcm;
        DiscoveryModel dm;
        GameState gs;
        String pc, inventory, itemCatalogue,
                usedEvents, currentEvent, eventCatalogue,
                discovered, discoveryCatalogue,
                visitedLocs, unvisitedLocs, currLoc, currSLoc;

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
            String validPREE = PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":10:20:Test Result";
            String validIEE = ItemEventEffect.ITEM_EFFECT_TAG + "#" + items[1].ParseToString() + "#Test Result";
            String validOption1 = Option.TAG + ";" + "1;TestText1;TestResult;EventEffects|" + validPREE + "|" + validIEE;
            String validOption2 = Option.TAG + ";" + "2;TestText2;TestResult;EventEffects|" + validPREE + "|" + validIEE;
            String validOption3 = Option.TAG + ";" + "3;TestText3;TestResult;EventEffects|" + validPREE + "|" + validIEE;
            String validOption4 = Option.TAG + ";" + "4;TestText4;TestResult;EventEffects|" + validPREE + "|" + validIEE;

            List<Event> events = new List<Event>();
            eventCatalogue = EventCatalogue.TAG;
            for (int i = 1; i < 21; i++)
            {
                String evt = Event.TAG + "_" + i + "_Type_Test text_EventOptions*" + validOption1 + "*" + validOption2 + "*" + validOption3 + "*" + validOption4;
                if (!Event.IsValidEvent(evt))
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

            List<Location> locations = new List<Location>();
            List<DummyLocation> dummyLocations = new List<DummyLocation>();

            visitedLocs = "VisitedLocations";
            unvisitedLocs = "UnvisitedLocations";

            for (int i = 1; i < 21; i++)
            {
                int j = i + 20;
                String loc;
                String dloc;
                if (i + 1 < 21)
                {
                    if (i - 1 > 0)
                    {
                        loc = "Type:Location,ID:" + i + ",Connections:" + (i + 1) + ":" + (i - 1) + ",Visited:True,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1";
                        dloc = "Type:DummyLocation,ID:" + j + ",Connections:" + (j - 1) + ":" + (j + 1);
                    }
                    else
                    {
                        loc = "Type:Location,ID:" + i + ",Connections:" + (i + 1) + ",Visited:True,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1";
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

            currLoc = "4";
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

        [TestCategory("MainController"), TestCategory("Controller"), TestMethod()]
        public void MainController_IntialiseNewGame()
        {
            //FileReadWriter frw = new FileReadWriter();
            //frw.WriteSaveDataFile("eventCatalogue", gs.GetEM().ParseCatalogueToString());
            //frw.WriteSaveDataFile("itemCatalogue", gs.GetPCM().GetItemCatalogue().ParseToString());
            //frw.WriteSaveDataFile("discoveryCatalogue", gs.GetDM().ParseCatalogueToString());
            
            MainController mc = new MainController();
            Assert.IsTrue(mc.InitialiseNewGame(), "New game should be succesfully initialized");
            GameState workingGS = mc.GetGameState();

            PCModel workingPCM = workingGS.GetPCM();
            LocationModel workingLM = workingGS.GetLM();
            EventModel workingEM = workingGS.GetEM();
            DiscoveryModel workingDM = workingGS.GetDM();

            EventCatalogue ec = workingEM.GetEventCatalogue();

            String workingEventCat = workingEM.ParseCatalogueToString();
            String workingDiscCat = workingDM.ParseCatalogueToString();
            String workingItemCat = workingPCM.GetItemCatalogue().ParseToString();

            Assert.AreEqual(eventCatalogue, workingEventCat, "Event catalogues should match");
            Assert.AreEqual(discoveryCatalogue, workingDiscCat, "Discovery catalogues should match");
            Assert.AreEqual(itemCatalogue, workingItemCat, "Item catalogues should match");
            Assert.AreEqual(1024, workingLM.GetUnvisited().Count, "Should be 1024 unvisited nodes");
        }

        [TestCategory("MainController"), TestCategory("Controller"), TestMethod()]
        public void MainController_IntialiseGameFromSave()
        {
            //FileReadWriter frw = new FileReadWriter();
            //frw.WriteSaveDataFile(FileReadWriter.PLAYER_CHARACTER, pc);
            //frw.WriteSaveDataFile(FileReadWriter.INVENTORY, inventory);
            //frw.WriteSaveDataFile(FileReadWriter.USED_EVENTS, usedEvents);
            //frw.WriteSaveDataFile(FileReadWriter.CURRENT_EVENT, currentEvent);
            //frw.WriteSaveDataFile(FileReadWriter.DISCOVERED, discovered);
            //frw.WriteSaveDataFile(FileReadWriter.VISITED, visitedLocs);
            //frw.WriteSaveDataFile(FileReadWriter.UNVISISTED, unvisitedLocs);
            //frw.WriteSaveDataFile(FileReadWriter.CURRENT_LOCATION, currLoc);
            //frw.WriteSaveDataFile(FileReadWriter.CURRENT_SUBLOCATION, currSLoc);
            MainController mc = new MainController();
            Assert.IsTrue(mc.InitialiseGameFromSave(), "Save game should be succesfully initialized");
            GameState workingGS = mc.GetGameState();

            PCModel workingPCM = workingGS.GetPCM();
            LocationModel workingLM = workingGS.GetLM();
            EventModel workingEM = workingGS.GetEM();
            DiscoveryModel workingDM = workingGS.GetDM();

            String workingPC = workingPCM.GetPC().ParseToString();
            String workingInventory = workingPCM.GetInventory().ParseToString();
            String workingUsedEvents = workingEM.ParseUsedEventsToString();
            String workingCurrentEvent = workingEM.ParseCurrentEventToString();
            String workingDiscovered = workingDM.ParseDiscoveredToString();
            String workingVisited = workingLM.ParseVisitedToString();
            String workingUnvisited = workingLM.ParseUnvisitedToString();
            String workingCurrLocation = workingLM.ParseCurrLocationToString();
            String workingCurrSLoc = workingLM.ParseCurrSubLocToString();

            Assert.AreEqual(pc,workingPC,"PC should match");
            Assert.AreEqual(inventory,workingInventory,"Inventory should match");
            Assert.AreEqual(usedEvents,workingUsedEvents,"Used Events should match");
            Assert.AreEqual(currentEvent,workingCurrentEvent,"Current events should match");
            Assert.AreEqual(discovered,workingDiscovered,"Discovered should match");
            Assert.AreEqual(visitedLocs,workingVisited,"Visited should match");
            Assert.AreEqual(unvisitedLocs,workingUnvisited,"Unvisisted should match");
            Assert.AreEqual(currLoc,workingCurrLocation,"Curr location should match");
            Assert.AreEqual(currSLoc,workingCurrSLoc,"Curr Sublocation should match");
         }

        [TestCategory("MainController"), TestCategory("Controller"), TestMethod()]
        public void MainController_WriteSaveData()
        {
            MainController mc = new MainController();
            Assert.IsTrue(mc.InitialiseGameFromSave(), "Save game should be succesfully initialized");
            GameState workingGS = mc.GetGameState();

            PCModel workingPCM = workingGS.GetPCM();
            LocationModel workingLM = workingGS.GetLM();
            EventModel workingEM = workingGS.GetEM();
            DiscoveryModel workingDM = workingGS.GetDM();
            Assert.IsTrue(mc.WriteSaveData(), "Save Data should be sucessfully written");

            FileReadWriter frw = new FileReadWriter();
            String pc = frw.ReadSaveDataFile(FileReadWriter.PLAYER_CHARACTER);
            String inventory = frw.ReadSaveDataFile(FileReadWriter.INVENTORY);
            String usedEvents = frw.ReadSaveDataFile(FileReadWriter.USED_EVENTS);
            String currentEvent = frw.ReadSaveDataFile(FileReadWriter.CURRENT_EVENT);
            String discovered = frw.ReadSaveDataFile(FileReadWriter.DISCOVERED);
            String visitedLocs = frw.ReadSaveDataFile(FileReadWriter.VISITED);
            String unvisitedLocs = frw.ReadSaveDataFile(FileReadWriter.UNVISISTED);
            String currLoc = frw.ReadSaveDataFile(FileReadWriter.CURRENT_LOCATION);
            String currSLoc = frw.ReadSaveDataFile(FileReadWriter.CURRENT_SUBLOCATION);

            String workingPC = workingPCM.GetPC().ParseToString();
            String workingInventory = workingPCM.GetInventory().ParseToString();
            String workingUsedEvents = workingEM.ParseUsedEventsToString();
            String workingCurrentEvent = workingEM.ParseCurrentEventToString();
            String workingDiscovered = workingDM.ParseDiscoveredToString();
            String workingVisited = workingLM.ParseVisitedToString();
            String workingUnvisited = workingLM.ParseUnvisitedToString();
            String workingCurrLocation = workingLM.ParseCurrLocationToString();
            String workingCurrSLoc = workingLM.ParseCurrSubLocToString();

            Assert.AreEqual(workingPC, pc, "Saved PC should match");
            Assert.AreEqual(workingInventory, inventory, "Saved Inventory should match");
            Assert.AreEqual(workingUsedEvents, usedEvents, "Saved Used Events should match");
            Assert.AreEqual(workingCurrentEvent, currentEvent, "Saved Current events should match");
            Assert.AreEqual(workingDiscovered, discovered, "Saved Discovered should match");
            Assert.AreEqual(workingVisited, visitedLocs, "Saved Visited should match");
            Assert.AreEqual(workingUnvisited, unvisitedLocs, "Saved Unvisisted should match");
            Assert.AreEqual(workingCurrLocation, currLoc, "Saved Curr location should match");
            Assert.AreEqual(workingCurrSLoc, currSLoc, "Saved Curr Sublocation should match");
        }
    }
}
