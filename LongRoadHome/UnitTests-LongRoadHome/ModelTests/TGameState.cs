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
    public class TGameState
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
                String evt = Event.TAG + "$" + i + "$Type$Test text$EventOptions*" + validOption1 + "*" + validOption2 + "*" + validOption3 + "*" + validOption4;
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
                        loc = "Type:Location,ID:" + i + ",Visited:True,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1";
                        dloc = "Type:DummyLocation,ID:" + j;
                    }
                    else
                    {
                        loc = "Type:Location,ID:" + i + ",Visited:True,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1";
                        dloc = "Type:DummyLocation,ID:" + j;
                    }
                }
                else
                {
                    loc = "Type:Location,ID:" + i + ",Visited:True,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1";
                    dloc = "Type:DummyLocation,ID:" + j;
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
            //gs = new GameState(pc, inventory, itemCatalogue,
            //    usedEvents, currentEvent, eventCatalogue,
            //    discovered, discoveryCatalogue,
            //    visitedLocs, unvisitedLocs, currLoc, currSLoc);
        }

        [TestCategory("GameState"), TestCategory("Model"), TestMethod()]
        public void GameState_ValidGameState()
        {
            Assert.IsTrue(GameState.AreValidCatalogues(itemCatalogue, eventCatalogue, discoveryCatalogue), "Catalogues should be valid");
            Assert.IsTrue(GameState.IsValidGameState(pc, inventory, itemCatalogue,
                usedEvents, currentEvent, eventCatalogue,
                discovered, discoveryCatalogue,
                visitedLocs, unvisitedLocs, currLoc, currSLoc), "Game State Should be valid");
        }
    }
}
