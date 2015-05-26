using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using System.Collections.Generic;
namespace UnitTests_LongRoadHome.EventTests
{
    [TestClass]
    public class TEventModel
    {
        List<Tuple<String, String>> invalidEvents = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validEvents = new List<Tuple<String, String>>();
        List<Tuple<String, String>> invalidCat = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validCat = new List<Tuple<String, String>>();
        List<Tuple<String, String>> invalidStrings = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validStrings = new List<Tuple<String, String>>();
        String validPREE, validIEE, validOption, invalidOption;

        List<String> validOptions = new List<string>();

        [TestInitialize]
        public void Setup()
        {
            String basicItem1 = "ID:2,Name:TestItem,Amount:1,Description:test item 2,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            validPREE = PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":10:20";
            validIEE = ItemEventEffect.ITEM_EFFECT_TAG + "#" + basicItem1;
            validOption = Option.TAG + ";" + "7;TestText;EventEffects|" + validPREE + "|" + validIEE;
            invalidOption = Option.TAG + ";" + "-1;TestText;EventEffects";

            validOptions.Add(Option.TAG + ";" + "1;TestText;EventEffects|" + validIEE);
            validOptions.Add(Option.TAG + ";" + "2;TestText;EventEffects|" + validIEE + "|" + validIEE);
            validOptions.Add(Option.TAG + ";" + "3;TestText;EventEffects|" + validIEE + "|" + validPREE);
            validOptions.Add(validOption);

            validEvents.Add(new Tuple<string, string>(Event.TAG + "$1$Type$Test text$EventOptions", "Basic Event is valid"));
            validEvents.Add(new Tuple<string, string>(Event.TAG + "$2$Type$Test text$EventOptions*" + validOption, "Event with a valid option should be valid"));
            validEvents.Add(new Tuple<string, string>(Event.TAG + "$3$Type$Test text$EventOptions*" + validOptions[0] + "*" + validOptions[1] + "*" + validOptions[2] + "*" + validOptions[3], "Event with valid options should be valid"));
            validEvents.Add(new Tuple<string, string>(Event.TAG + "$4$Type$Test text$EventOptions*" + validOptions[0] + "*" + validOptions[1] + "*" + validOptions[2] + "*" + validOptions[3], "Event with valid options should be valid"));
            validEvents.Add(new Tuple<string, string>(Event.TAG + "$5$Type$Test text$EventOptions*" + validOptions[0] + "*" + validOptions[1] + "*" + validOptions[2] + "*" + validOptions[3], "Event with valid options should be valid"));
            validEvents.Add(new Tuple<string, string>(Event.TAG + "$6$Type$Test text$EventOptions*" + validOptions[0] + "*" + validOptions[1] + "*" + validOptions[2] + "*" + validOptions[3], "Event with valid options should be valid"));
            validEvents.Add(new Tuple<string, string>(Event.TAG + "$7$Type$Test text$EventOptions*" + validOptions[0] + "*" + validOptions[1] + "*" + validOptions[2] + "*" + validOptions[3], "Event with valid options should be valid"));
            validEvents.Add(new Tuple<string, string>(Event.TAG + "$8$Type$Test text$EventOptions*" + validOptions[0] + "*" + validOptions[1] + "*" + validOptions[2] + "*" + validOptions[3], "Event with valid options should be valid"));
            validEvents.Add(new Tuple<string, string>(Event.TAG + "$9$Type$Test text$EventOptions*" + validOptions[0] + "*" + validOptions[1] + "*" + validOptions[2] + "*" + validOptions[3], "Event with valid options should be valid"));
            validEvents.Add(new Tuple<string, string>(Event.TAG + "$10$Type$Test text$EventOptions*" + validOptions[0] + "*" + validOptions[1] + "*" + validOptions[2] + "*" + validOptions[3], "Event with valid options should be valid"));
            validEvents.Add(new Tuple<string, string>(Event.TAG + "$11$Type$Test text$EventOptions*" + validOptions[0] + "*" + validOptions[1] + "*" + validOptions[2] + "*" + validOptions[3], "Event with valid options should be valid"));
            validEvents.Add(new Tuple<string, string>(Event.TAG + "$12$Type$Test text$EventOptions*" + validOptions[0] + "*" + validOptions[1] + "*" + validOptions[2] + "*" + validOptions[3], "Event with valid options should be valid"));

            invalidEvents.Add(new Tuple<string, string>("", "Empty String is invalid"));
            invalidEvents.Add(new Tuple<string, string>(Event.TAG + "$1$Type$Test text", "Should have at least 5 elements"));
            invalidEvents.Add(new Tuple<string, string>(Event.TAG + "$1$Type$Test text$EventOptions$haha", "Should have at most 5 elements"));
            invalidEvents.Add(new Tuple<string, string>(Event.TAG + "$1$Type$Test text$EventOptions*", "If there are event options there should be at least one"));
            invalidEvents.Add(new Tuple<string, string>("Not a tag$1$Type$Test text$EventOptions", "Should start with " + Event.TAG));
            invalidEvents.Add(new Tuple<string, string>(Event.TAG + "$ha$Type$Test text$EventOptions", "ID should be an int"));
            invalidEvents.Add(new Tuple<string, string>(Event.TAG + "$-1$Type$Test text$EventOptions", "ID should be positive"));
            invalidEvents.Add(new Tuple<string, string>(Event.TAG + "$1$Type$Test text$EventOptions*" + invalidOption, "Invalid option should mean invalid event"));

            validCat.Add(new Tuple<string, string>(EventCatalogue.TAG, "Empty event catalogue is valid"));
            validCat.Add(new Tuple<string, string>(EventCatalogue.TAG + "^" + validEvents[0].Item1, "Event Catalogue with a single event is valid"));
            validCat.Add(new Tuple<string, string>(EventCatalogue.TAG + "^" + validEvents[0].Item1 + "^" + validEvents[1].Item1, "Event Catalogue with multiple events is valid"));

            invalidCat.Add(new Tuple<string, string>("", "Empty String is invalid"));
            invalidCat.Add(new Tuple<string, string>(EventCatalogue.TAG + "^", "If a catalogue has events it should have at least one"));
            invalidCat.Add(new Tuple<string, string>("WrongTag^" + validEvents[0].Item1, "Should start with" + EventCatalogue.TAG));
            invalidCat.Add(new Tuple<string, string>(EventCatalogue.TAG + "^" + invalidEvents[0].Item1, "Invalid event should mean invalid catalogue"));
            invalidCat.Add(new Tuple<string, string>(EventCatalogue.TAG + "^" + validEvents[0].Item1 + "^" + validEvents[0].Item1, "Each event should have a unique ID"));
            invalidCat.Add(new Tuple<string, string>("", ""));
            invalidCat.Add(new Tuple<string, string>("", ""));
            invalidCat.Add(new Tuple<string, string>("", ""));
            invalidCat.Add(new Tuple<string, string>("", ""));

            validStrings.Add(new Tuple<string, string>(EventModel.USED_TAG, "Empty used events is valid"));
            validStrings.Add(new Tuple<string, string>(EventModel.USED_TAG + ":1", "One used event is valid"));
            validStrings.Add(new Tuple<string, string>(EventModel.USED_TAG + ":1:2:3:4:5:6:7:8", "Mulitple used events are valid"));

            invalidStrings.Add(new Tuple<string, string>(EventModel.USED_TAG + ":", "If there are used events there should be at least one"));
            invalidStrings.Add(new Tuple<string, string>(EventModel.USED_TAG + ":1:1", "Used events should be unique"));
            invalidStrings.Add(new Tuple<string, string>(EventModel.USED_TAG + ":a", "Used events should be ints"));
            invalidStrings.Add(new Tuple<string, string>(EventModel.USED_TAG + ":-1", "Used events should be positive"));
        }

        [TestCategory("EventModelClass"), TestCategory("EventModel"), TestMethod()]
        public void EventModel_StandardConstructor()
        {
            EventModel em = new EventModel();
            Assert.AreEqual(0, em.GetUsedEvents().Count, "Used events should be empty");
            Assert.IsNull(em.GetCurrentEvent(), "Current event should be null");
        }

        [TestCategory("EventModelClass"), TestCategory("EventModel"), TestMethod()]
        public void EventModel_CheckusedEventStringIsValid()
        {
            foreach (Tuple<String, String> test in validStrings)
            {
                Assert.IsTrue(EventModel.IsValidUsedEvents(test.Item1), test.Item2);
            }

            foreach (Tuple<String, String> test in invalidStrings)
            {
                Assert.IsFalse(EventModel.IsValidUsedEvents(test.Item1), test.Item2);
            }
        }

        [TestCategory("EventModelClass"), TestCategory("EventModel"), TestMethod()]
        public void EventModel_ParseFromString()
        {
            EventModel em = new EventModel(validStrings[2].Item1, validCat[2].Item1, validEvents[0].Item1);
            EventCatalogue ec = new EventCatalogue(validCat[2].Item1);

            Assert.AreEqual(ec.ParseToString(), em.GetEventCatalogue().ParseToString());

            Event eve = new Event(validEvents[0].Item1);
            Event temp = em.GetCurrentEvent();

            Assert.AreEqual(eve.GetEventID(), em.GetCurrentEventID(), "Event IDs should match");
            Assert.AreEqual(eve.GetEventText(), em.GetCurrentEventText(), "Event text should match");
            Assert.AreEqual(eve.ParseToString(), temp.ParseToString(), "Strings of parsed events should match");

            HashSet<int> used = em.GetUsedEvents();
            for (int i = 1; i < 8; i++)
            {
                Assert.IsTrue(used.Contains(i), "Used should contain " + i);
            }
        }

        [TestCategory("EventModelClass"), TestCategory("EventModel"), TestMethod()]
        public void EventModel_ParseToString()
        {
            EventModel em = new EventModel(validStrings[2].Item1, validCat[2].Item1, validEvents[0].Item1);

            Assert.AreEqual(validStrings[2].Item1, em.ParseUsedEventsToString(), "Strings hsould match");
        }
    }
}
