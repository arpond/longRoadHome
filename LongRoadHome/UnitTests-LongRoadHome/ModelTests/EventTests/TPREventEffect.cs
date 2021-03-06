﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using System.Collections.Generic;

namespace UnitTests_LongRoadHome.EventTests
{
    [TestClass]
    public class TPREventEffect
    {
        List<Tuple<String, String>> invalidStrings = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validStrings = new List<Tuple<String, String>>();
        
        [TestInitialize]
        public void Setup()
        {
            invalidStrings.Add(new Tuple<String, String>("", "Empty String is invalid"));
            invalidStrings.Add(new Tuple<String, String>(PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":0:10", "Should be at least 5 items in a resource"));
            invalidStrings.Add(new Tuple<String, String>(PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":10:20:Test Result:blah", "Should be at most 5 items in a resource"));
            invalidStrings.Add(new Tuple<String, String>(PREventEffect.PR_EFFECT_TAG + ":" + ":blah:10:12:Test Result", "Should affect one of the 4 primary character resources"));
            invalidStrings.Add(new Tuple<String, String>(PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":" + PlayerCharacter.HEALTH + ":10:Test Result", "Third item should be an int"));
            invalidStrings.Add(new Tuple<String, String>(PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":" + "10:" + PlayerCharacter.HEALTH + ":Test Result", "Fourth item should be an int"));
            invalidStrings.Add(new Tuple<String, String>("PE:" + PlayerCharacter.HEALTH + ":10:20:Test Result", "First item should be " + PREventEffect.PR_EFFECT_TAG));
            invalidStrings.Add(new Tuple<String, String>(PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":20:11:Test Result", "Minimum should be less than maximum"));
            invalidStrings.Add(new Tuple<String, String>("", "Empty String is invalid"));

            validStrings.Add(new Tuple<String, String>(PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":0:0:Test Result", "Basic Active Effect is valid"));
            validStrings.Add(new Tuple<String, String>(PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":10:20:Test Result", "Should be valid Active Effect"));
        }

        [TestCategory("PREventEffect"), TestCategory("EventModel"), TestMethod()]
        public void PREventEffect_StandardConstructor()
        {
            PREventEffect pree = new PREventEffect();
            String expectedPR = PlayerCharacter.HEALTH + ":0";

            Assert.AreEqual(0, pree.GetMinimum(), "The min should be 0");
            Assert.AreEqual(0, pree.GetMaximum(), "The max should be 0");
            Assert.AreEqual(expectedPR, pree.GetResource().ParseToString(), "The string should match the expected value");
        }

        [TestCategory("PREventEffect"), TestCategory("EventModel"), TestMethod()]
        public void PREventEffect_CheckStringValid()
        {
            foreach (Tuple<String, String> test in validStrings)
            {
                Assert.IsTrue(PREventEffect.IsValidPREventEffect(test.Item1), test.Item2);
            }

            foreach (Tuple<String, String> test in invalidStrings)
            {
                Assert.IsFalse(PREventEffect.IsValidPREventEffect(test.Item1), test.Item2);
            }   
        }

        [TestCategory("PREventEffect"), TestCategory("EventModel"), TestMethod()]
        public void PREventEffect_ParseFromString()
        {
            PREventEffect pree = new PREventEffect(PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":0:0:Test Result");
            String expectedPR = PlayerCharacter.HEALTH + ":0";

            Assert.AreEqual(0, pree.GetMinimum(), "The min should be 0");
            Assert.AreEqual(0, pree.GetMaximum(), "The max should be 0");
            Assert.AreEqual(expectedPR, pree.GetResource().ParseToString(), "The string should match the expected value");
        }

        [TestCategory("PREventEffect"), TestCategory("EventModel"), TestMethod()]
        public void PREventEffect_ParseToString()
        {
            String expected = PREventEffect.PR_EFFECT_TAG + ":" + PlayerCharacter.HEALTH + ":10:20:Test Result";
            PREventEffect pree = new PREventEffect(expected);
            String expectedPR = PlayerCharacter.HEALTH + ":10";

            Assert.AreEqual(10, pree.GetMinimum(), "The min should be 10");
            Assert.AreEqual(20, pree.GetMaximum(), "The max should be 20");
            Assert.AreEqual(expectedPR, pree.GetResource().ParseToString(), "The string should match the expected value");
            Assert.AreEqual(expected, pree.ParseToString(), "String should be " + expected);
        }
    }
}
