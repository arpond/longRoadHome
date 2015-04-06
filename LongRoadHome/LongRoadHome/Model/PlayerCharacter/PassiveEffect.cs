using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class PassiveEffect
    {
        public const String TAG = "PE";

        private String resourceName;
        private float modifierVal;

        /// <summary>
        /// Constructor for a passive modifier which takes a string
        /// </summary>
        /// <param name="toParse">The string to parse to a passive effect</param>
        public PassiveEffect(String toParse)
        {
            String[] effectElements = toParse.Split(':');
            if (float.TryParse(effectElements[2], out modifierVal))
            {
                resourceName = effectElements[1];
            }            
        }

        /// <summary>
        /// Constructor for a passive modifier
        /// </summary>
        /// <param name="resourceName">Name of the resource the modifier is for</param>
        /// <param name="modifierVal">Value of the modifier</param>
        public PassiveEffect(String resourceName, float modifierVal)
        {
            this.resourceName = resourceName;
            this.modifierVal = modifierVal;
        }

        /// <summary>
        /// Gets the name of the resource the modifier is for
        /// </summary>
        /// <returns></returns>
        public String GetResourceName()
        {
            return this.resourceName;
        }

        /// <summary>
        /// Gets the modifier value
        /// </summary>
        /// <returns></returns>
        public float GetModifier()
        {
            return this.modifierVal;
        }

        /// <summary>
        /// Parses this Passive Effect to a string format suitable for saving
        /// </summary>
        /// <returns>The parsed Passive Effect</returns>
        public string ParseToString()
        {
            return String.Format("{0}:{1}:{2}", TAG, resourceName, modifierVal);
        }

        /// <summary>
        /// Checks if a string is a valid Passive Effect string
        /// </summary>
        /// <param name="toTest">The string to test</param>
        /// <returns>If the string is valid or invalid</returns>
        public static bool IsValidPassiveEffect(String toTest)
        {
            String[] effectElements = toTest.Split(':');

            if (effectElements.Length != 3)
            {
                return false;
            }
            if (effectElements[0] != TAG)
            {
                return false;
            }
            if (!(effectElements[1] == PlayerCharacter.HEALTH || effectElements[1] == PlayerCharacter.HUNGER || effectElements[1] == PlayerCharacter.THIRST || effectElements[1] == PlayerCharacter.SANITY))
            {
                return false;
            }

            float value;
            if (!float.TryParse(effectElements[2], out value))
            {
                return false;
            }

            return true;
        }
    }
}
