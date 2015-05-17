using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;

using System.Collections.Generic;

namespace UnitTests_LongRoadHome.EventTests
{
    [TestClass]
    public class TEvent
    {
        Event ev;
        List<Tuple<String, String>> invalidStrings = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validStrings = new List<Tuple<String, String>>();

        String validPREE, validIEE, validOption, invalidOption;

        List<String> validOptions = new List<string>();


        [TestInitialize]
        public void Setup()
        {
            String basicItem1 = "ID:2,Name:TestItem,Amount:1,Description:test item 2,ActiveEffect,PassiveEffect,Requirements";
            validPREE = PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":10:20:Test Result";
            validIEE = ItemEventEffect.ITEM_EFFECT_TAG + "#" + basicItem1 + "#Test Result";
            validOption = Option.TAG + ";" + "7;TestText;TestResult;EventEffects|" + validPREE + "|" + validIEE;
            invalidOption = Option.TAG + ";" + "-1;TestText;TestResult;EventEffects";

            validOptions.Add(Option.TAG + ";" + "1;TestText;TestResult;EventEffects|" + validIEE);
            validOptions.Add(Option.TAG + ";" + "2;TestText;TestResult;EventEffects|" + validIEE + "|" + validIEE);
            validOptions.Add(Option.TAG + ";" + "3;TestText;TestResult;EventEffects|" + validIEE + "|" + validPREE);
            validOptions.Add(validOption);            

            validStrings.Add(new Tuple<string, string>(Event.TAG + "_1_Type_Test text_EventOptions", "Basic Event is valid"));
            validStrings.Add(new Tuple<string, string>(Event.TAG + "_2_Type_Test text_EventOptions*" + validOption, "Event with a valid option should be valid"));
            validStrings.Add(new Tuple<string, string>(Event.TAG + "_3_Type_Test text_EventOptions*" + validOptions[0] + "*" + validOptions[1] + "*" + validOptions[2] + "*" + validOptions[3], "Event with valid options should be valid"));


            invalidStrings.Add(new Tuple<string, string>("", "Empty String is invalid"));
            invalidStrings.Add(new Tuple<string, string>(Event.TAG + "_1_Type_Test text", "Should have at least 5 elements"));
            invalidStrings.Add(new Tuple<string, string>(Event.TAG + "_1_Type_Test text_EventOptions_haha", "Should have at most 5 elements"));
            invalidStrings.Add(new Tuple<string, string>(Event.TAG + "_1_Type_Test text_EventOptions*", "If there are event options there should be at least one"));
            invalidStrings.Add(new Tuple<string, string>("Not a tag_1_Type_Test text_EventOptions", "Should start with " + Event.TAG));
            invalidStrings.Add(new Tuple<string, string>(Event.TAG + "_ha_Type_Test text_EventOptions", "ID should be an int"));
            invalidStrings.Add(new Tuple<string, string>(Event.TAG + "_-1_Type_Test text_EventOptions", "ID should be positive"));
            invalidStrings.Add(new Tuple<string, string>(Event.TAG + "_1_Type_Test text_EventOptions*"+invalidOption, "Invalid option should mean invalid event"));
        }

        [TestCategory("Event"), TestCategory("EventModel"), TestMethod()]
        public void Event_StandardConstructor()
        {
            ev = new Event();

            Assert.AreEqual(1, ev.GetEventID(), "Event ID should be 1");
            Assert.AreEqual("Temp", ev.GetEventType(), "Event type should be Temp");
            Assert.AreEqual("Test Text", ev.GetEventText(), "Event text should be Test Text");
            Assert.AreEqual(0, ev.GetEventOptions().Count, "There should be no event options");
        }

        [TestCategory("Event"), TestCategory("EventModel"), TestMethod()]
        public void Event_CheckStringIsValid()
        {
            foreach (Tuple<String, String> test in validStrings)
            {
                Assert.IsTrue(Event.IsValidEvent(test.Item1), test.Item2);
            }

            foreach (Tuple<String, String> test in invalidStrings)
            {
                Assert.IsFalse(Event.IsValidEvent(test.Item1), test.Item2);
            }
        }

        [TestCategory("Event"), TestCategory("EventModel"), TestMethod()]
        public void Event_ParseFromString()
        {
            int i = 1;
            foreach (Tuple<String, String> test in validStrings)
            {
                Event ev = new Event(test.Item1);
                var options = ev.GetEventOptions();

                Assert.AreEqual(i, ev.GetEventID(), "Event id should match for event " + i);
                Assert.AreEqual("Type", ev.GetEventType(), "Type should match for option " + i);
                Assert.AreEqual("Test text", ev.GetEventText(), "Text should match for option " + i);

                switch (i)
                {
                    case 1:
                        Assert.AreEqual(0, options.Count, "Should be no options for event " + i);
                        break;
                    case 2:
                        Assert.AreEqual(1, options.Count, "Should be one option for event " + i);
                        Assert.AreEqual(validOption, options[0].ParseToString(), "Option should match for event " + i);
                        break;
                    case 3:
                        Assert.AreEqual(4, options.Count, "Should be four options for event " + i);
                        Assert.AreEqual(validOptions[0], options[0].ParseToString(), "Option 1 should match for event " + i);
                        Assert.AreEqual(validOptions[1], options[1].ParseToString(), "Option 2 should match for event " + i);
                        Assert.AreEqual(validOptions[2], options[2].ParseToString(), "Option 3 should match for event " + i);
                        Assert.AreEqual(validOptions[3], options[3].ParseToString(), "Option 4 should match for event " + i);
                        break;
                }
                i++;
            }
        }

        [TestCategory("Event"), TestCategory("EventModel"), TestMethod()]
        public void Event_ParseToString()
        {
            int i = 1;
            foreach (Tuple<String, String> test in validStrings)
            {
                Event ev = new Event(test.Item1);
                String expected = test.Item1;

                Assert.AreEqual(expected, ev.ParseToString(), "Strings should match for event " + i);
                i++;
            }
        }
    }
}
