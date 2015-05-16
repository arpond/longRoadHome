using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events
{
    public class Option
    {
        public const String TAG = "Option";
        private int optionNumber;
        private String optionText;
        private List<EventEffect> effects;
        private String optionResult;

        public Option()
        {
            optionNumber = 1;
            optionText = "Test text";
            effects = new List<EventEffect>();
        }

        public Option(String toParse)
        {
            effects = new List<EventEffect>();
            String[] optionElements = toParse.Split(';');
            if (int.TryParse(optionElements[1], out optionNumber))
            {
                optionText = optionElements[2];
                optionResult = optionElements[3];
                String[] effectElements = optionElements[4].Split('|');
                if(effectElements.Length > 1)
                {
                    for(int i=1; i < effectElements.Length; i++)
                    {
                        if(ItemEventEffect.IsValidItemEventEffect(effectElements[i]))
                        {
                            effects.Add(new ItemEventEffect(effectElements[i]));
                        }
                        else if (PREventEffect.IsValidPREventEffect(effectElements[i]))
                        {
                            effects.Add(new PREventEffect(effectElements[i]));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Accessor method for Option Text
        /// </summary>
        /// <returns>The option text stored</returns>
        public String GetOptionText()
        {
            return this.optionText;
        }

        /// <summary>
        /// Acessor method for Option Number
        /// </summary>
        /// <returns>The option number stored</returns>
        public int GetOptionNumber()
        {
            return this.optionNumber;
        }

        public String GetOptionResult()
        {
            return this.optionResult;
        }

        /// <summary>
        /// Accessor method for option effects
        /// </summary>
        /// <returns>The option effects stored</returns>
        public List<EventEffect> GetOptionEffects()
        {
            return this.effects;
        }

        /// <summary>
        /// Parses this option to a string
        /// </summary>
        /// <returns>The parsed option</returns>
        public String ParseToString()
        {
            String effectsString = "EventEffects";
            foreach(EventEffect ee in effects)
            {
                effectsString += "|" + ee.ParseToString();
            }

            return String.Format("{0};{1};{2};{3};{4}", TAG, optionNumber, optionText, optionResult, effectsString);
        }

        /// <summary>
        /// Checks if the string is a valid option
        /// </summary>
        /// <param name="toTest">The string to test</param>
        /// <returns>If the string is valid</returns>
        public static bool IsValidOption(String toTest)
        {
            int optionNum;
            String[] optionElements = toTest.Split(';');
            if (optionElements[0] != TAG || optionElements.Length != 5)
            {
                return false;
            }
            if (!int.TryParse(optionElements[1], out optionNum) || optionNum < 1)
            {
                return false;
            }
            String[] effectElements = optionElements[4].Split('|');
            if (effectElements.Length > 1)
            {
                for (int i=1; i < effectElements.Length; i++)
                {
                    if(!ItemEventEffect.IsValidItemEventEffect(effectElements[i]) && !PREventEffect.IsValidPREventEffect(effectElements[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

}
