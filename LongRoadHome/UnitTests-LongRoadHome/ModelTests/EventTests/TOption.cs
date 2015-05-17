using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;

namespace UnitTests_LongRoadHome.EventTests
{
    /// <summary>
    /// Summary description for TOption
    /// </summary>
    [TestClass]
    public class TOption
    {
        String validPREE, invalidPREE, validIEE, invalidIEE;
        List<Tuple<String, String>> invalidStrings = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validStrings = new List<Tuple<String, String>>();

        [TestInitialize]
        public void Setup()
        {
            String basicItem1 = "ID:2,Name:TestItem,Amount:1,Description:test item 2,ActiveEffect,PassiveEffect,Requirements";
            validPREE = PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":10:20:Test Result";
            invalidPREE = PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":0:20";
            validIEE = ItemEventEffect.ITEM_EFFECT_TAG + "#" + basicItem1 + "#Test Result";
            invalidIEE = ItemEventEffect.ITEM_EFFECT_TAG;

            validStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "1;TestText;TestResult;EventEffects" ,"Standard Option should be valid"));
            validStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "2;TestText;TestResult;EventEffects|" + validPREE, "Option with valid PREE should be valid"));
            validStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "3;TestText;TestResult;EventEffects|" + validPREE + "|" + validPREE, "Option with multiple PREEs should be valid"));
            validStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "4;TestText;TestResult;EventEffects|" + validIEE, "Option with valid IEE should be valid"));
            validStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "5;TestText;TestResult;EventEffects|" + validIEE + "|" + validIEE, "Option with multiple IEEs should be valid"));
            validStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "6;TestText;TestResult;EventEffects|" + validIEE + "|" + validPREE, "Option with valid IEE and PREE should be valid"));
            validStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "7;TestText;TestResult;EventEffects|" + validPREE + "|" + validIEE, "Option with valid PREE and IEE should be valid"));

            invalidStrings.Add(new Tuple<string, string>("", "Empty String should be invalid"));
            invalidStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "1;TestText;TestResult", "Should be at least 5 items"));
            invalidStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "1;TestText;TestResult;EventEffects;blah", "Should be at most 5 items"));
            invalidStrings.Add(new Tuple<string, string>("blah;" + "1;TestText;TestResult;EventEffects", "Should start with " + Option.TAG));
            invalidStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "blah;TestText;TestResult;EventEffects", "Option number should be an int"));
            invalidStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "-1;TestText;TestResult;EventEffects", "Option number should be positive"));
            invalidStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "1;TestText;TestResult;EventEffects|", "If there is an event effect there should be at least one"));
            invalidStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "1;TestText;TestResult;EventEffects|" + invalidIEE, "Invalid IEE should mean invalid option"));
            invalidStrings.Add(new Tuple<string, string>(Option.TAG + ";" + "1;TestText;TestResult;EventEffects|" + invalidPREE, "Invalid PREE should mean invalid option"));
            invalidStrings.Add(new Tuple<string, string>("", ""));
            invalidStrings.Add(new Tuple<string, string>("", ""));
        }

        [TestCategory("Option"), TestCategory("EventModel"), TestMethod()]
        public void Option_StandardConstructor()
        {
            Option opt = new Option();
            Assert.AreEqual(1, opt.GetOptionNumber(), "Option should be number 1");
            Assert.AreEqual("Test text", opt.GetOptionText(), "Option text should be 'Test text'");
            Assert.AreEqual(0, opt.GetOptionEffects().Count, "EventEffects should  be empty");
        }

        [TestCategory("Option"), TestCategory("EventModel"), TestMethod()]
        public void Option_CheckStringValid()
        {
            foreach (Tuple<String, String> test in validStrings)
            {
                Assert.IsTrue(Option.IsValidOption(test.Item1), test.Item2);
            }

            foreach (Tuple<String, String> test in invalidStrings)
            {
                Assert.IsFalse(Option.IsValidOption(test.Item1), test.Item2);
            }
        }

        [TestCategory("Option"), TestCategory("EventModel"), TestMethod()]
        public void Option_ParseFromString()
        {
            int i = 1;
            foreach (Tuple<String, String> test in validStrings)
            {
                Option op = new Option(test.Item1);
                var eventEffects = op.GetOptionEffects();

                Assert.AreEqual(i, op.GetOptionNumber(), "Option number should match parsed number for option " + i);
                Assert.AreEqual("TestText", op.GetOptionText(), "Text should match for option " + i);

                switch (i)
                {
                    case 1:
                        Assert.AreEqual(0, eventEffects.Count, "Should be no effects for option " + i);
                        break;
                    case 2:
                        Assert.AreEqual(1, eventEffects.Count, "Should be one effects for option " + i);
                        Assert.AreEqual(validPREE, eventEffects[0].ParseToString(), "PR event effect should match for option " + i);
                        break;
                    case 3:
                        Assert.AreEqual(2, eventEffects.Count, "Should be two effects for option " + i);
                        Assert.AreEqual(validPREE, eventEffects[0].ParseToString(), "First PR event effect should match for option " + i);
                        Assert.AreEqual(validPREE, eventEffects[1].ParseToString(), "Second PR event effect should match for option " + i);
                        break;
                    case 4:
                        Assert.AreEqual(1, eventEffects.Count, "Should be one effects for option " + i);
                        Assert.AreEqual(validIEE, eventEffects[0].ParseToString(), "Item event effect should match for option " + i);
                        break;
                    case 5:
                        Assert.AreEqual(2, eventEffects.Count, "Should be two effects for option " + i);
                        Assert.AreEqual(validIEE, eventEffects[0].ParseToString(), "First item event effect should match for option " + i);
                        Assert.AreEqual(validIEE, eventEffects[1].ParseToString(), "Second item event effect should match for option " + i);
                        break;
                    case 6:
                        Assert.AreEqual(2, eventEffects.Count, "Should be two effects for option " + i);
                        Assert.AreEqual(validIEE, eventEffects[0].ParseToString(), "First event should be an item event effect and should match for option " + i);
                        Assert.AreEqual(validPREE, eventEffects[1].ParseToString(), "Second event should be a PR event effect and should match for option " + i);
                        break;
                    case 7:
                        Assert.AreEqual(2, eventEffects.Count, "Should be two effects for option " + i);
                        Assert.AreEqual(validPREE, eventEffects[0].ParseToString(), "First event should be a PR event effect and should match for option " + i);
                        Assert.AreEqual(validIEE, eventEffects[1].ParseToString(), "Second event should be an Item event effect and should match for option " + i);
                        break;
                }
                i++;
            }
        }

        [TestCategory("Option"), TestCategory("EventModel"), TestMethod()]
        public void Option_ParseToString()
        {
            int i = 1;
            foreach (Tuple<String, String> test in validStrings)
            {
                Option op = new Option(test.Item1);
                String expected = test.Item1;

                Assert.AreEqual(expected, op.ParseToString(), "Strings should match for option " + i);
                i++;
            }
        }
    }
}
