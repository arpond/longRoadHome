using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;

using System.Collections.Generic;

namespace UnitTests_LongRoadHome.EventTests
{
    [TestClass]
    public class TEventCatalogue
    {
        EventCatalogue evc;
        List<Tuple<String, String>> invalidEvents = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validEvents = new List<Tuple<String, String>>();
        List<Tuple<String, String>> invalidStrings = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validStrings = new List<Tuple<String, String>>();
        String validPREE, validIEE, validOption, invalidOption;

        List<String> validOptions = new List<string>();

        [TestInitialize]
        public void Setup()
        {
            String basicItem1 = "ID:2,Name:TestItem,Amount:1,Description:test item 2,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            validPREE = PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":10:20:Test Result";
            validIEE = ItemEventEffect.ITEM_EFFECT_TAG + "#" + basicItem1 + "#Test Result";
            validOption = Option.TAG + ";" + "7;TestText;TestResult;EventEffects|" + validPREE + "|" + validIEE;
            invalidOption = Option.TAG + ";" + "-1;TestText;TestResult;EventEffects";

            validOptions.Add(Option.TAG + ";" + "1;TestText;TestResult;EventEffects|" + validIEE);
            validOptions.Add(Option.TAG + ";" + "2;TestText;TestResult;EventEffects|" + validIEE + "|" + validIEE);
            validOptions.Add(Option.TAG + ";" + "3;TestText;TestResult;EventEffects|" + validIEE + "|" + validPREE);
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

            validStrings.Add(new Tuple<string, string>(EventCatalogue.TAG, "Empty event catalogue is valid"));
            validStrings.Add(new Tuple<string, string>(EventCatalogue.TAG + "^" + validEvents[0].Item1, "Event Catalogue with a single event is valid"));
            validStrings.Add(new Tuple<string, string>(EventCatalogue.TAG + "^" + validEvents[0].Item1 + "^" + validEvents[1].Item1, "Event Catalogue with multiple events is valid"));

            invalidStrings.Add(new Tuple<string, string>("", "Empty String is invalid"));
            invalidStrings.Add(new Tuple<string, string>(EventCatalogue.TAG+"^", "If a catalogue has events it should have at least one"));
            invalidStrings.Add(new Tuple<string, string>("WrongTag^" + validEvents[0].Item1, "Should start with" + EventCatalogue.TAG));
            invalidStrings.Add(new Tuple<string, string>(EventCatalogue.TAG + "^" + invalidEvents[0].Item1, "Invalid event should mean invalid catalogue"));
            invalidStrings.Add(new Tuple<string, string>(EventCatalogue.TAG + "^" + validEvents[0].Item1 + "^" + validEvents[0].Item1, "Each event should have a unique ID"));
            invalidStrings.Add(new Tuple<string, string>("", ""));
            invalidStrings.Add(new Tuple<string, string>("", ""));
            invalidStrings.Add(new Tuple<string, string>("", ""));
            invalidStrings.Add(new Tuple<string, string>("", ""));
        }

        [TestCategory("EventCatalogue"), TestCategory("EventModel"), TestMethod()]
        public void EventCatalogue_CheckStringIsValid()
        {
            foreach (Tuple<String, String> test in validStrings)
            {
                Assert.IsTrue(EventCatalogue.IsValidEventCatalogue(test.Item1), test.Item2);
            }

            foreach (Tuple<String, String> test in invalidStrings)
            {
                Assert.IsFalse(EventCatalogue.IsValidEventCatalogue(test.Item1), test.Item2);
            }
        }

        [TestCategory("Event"), TestCategory("EventModel"), TestMethod()]
        public void EventCatalogue_ParseFromString()
        {
            String validCat = EventCatalogue.TAG;
            foreach (Tuple<String, String> eventTest in validEvents)
            {
                validCat += "^" + eventTest.Item1;
            }

            evc = new EventCatalogue(validCat);
            for (int i=1; i<= validEvents.Count; i++)
            {
                Assert.AreEqual(validEvents[i-1].Item1,evc.GetEvent(i).ParseToString(),"Events should match for event " + i);
            }
        }

        [TestCategory("Event"), TestCategory("EventModel"), TestMethod()]
        public void EventCatalogue_ParseToString()
        {
            String validCat = EventCatalogue.TAG;
            foreach (Tuple<String, String> eventTest in validEvents)
            {
                validCat += "^" + eventTest.Item1;
            }

            evc = new EventCatalogue(validCat);

            Assert.AreEqual(validCat, evc.ParseToString(), "Strings should match");
        }

    }
}
