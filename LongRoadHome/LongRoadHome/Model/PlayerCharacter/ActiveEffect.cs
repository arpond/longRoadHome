using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class ActiveEffect : uk.ac.dundee.arpond.longRoadHome.Model.Effect
    {
        public const String TAG = "AE";

        private int value;
        private PrimaryResource resource;

        public ActiveEffect()
        {
            value = 0;
            resource = new PrimaryResource(value,PlayerCharacter.HEALTH);
        }

        // <summary>
        /// Constructor for a active modifier which takes a string
        /// </summary>
        /// <param name="toParse">The string to parse to a activeeffect</param>
        public ActiveEffect(String toParse)
        {
            String[] effectElements = toParse.Split(':');
            if (int.TryParse(effectElements[2], out value))
            {
                resource = new PrimaryResource(value, effectElements[1]);
            }
        }

        /// <summary>
        /// Accessor method for value
        /// </summary>
        /// <returns>The value of the active effect</returns>
        public int GetValue()
        {
            return this.value;
        }

        /// <summary>
        /// Accessor method for the primary resource
        /// </summary>
        /// <returns>The primary resource of the active effect</returns>
        public PrimaryResource GetResource()
        {
            return this.resource;
        }

        /// <summary>
        /// Adjusts the player character with this items effect
        /// </summary>
        /// <param name="eventModifier">Not used</param>
        /// <param name="pc">The player character to modify</param>
        public void ResolveEffect(double eventModifier, PCModel pcm)
        {
            pcm.ModifyPrimaryResource(resource, value);
        }

        /// <summary>
        /// Parses this Active Effect to a string format suitable for saving
        /// </summary>
        /// <returns>The parsed Active Effect</returns>
        public string ParseToString()
        {
            return String.Format("{0}:{1}:{2}", TAG, resource.GetName(), resource.GetAmount());
        }

        /// <summary>
        /// Checks if a string is a valid Active Effect string
        /// </summary>
        /// <param name="toTest">The string to test</param>
        /// <returns>If the string is valid or invalid</returns>
        public static bool IsValidActiveEffect(String toTest)
        {
            String[] effectElements = toTest.Split(':');

            if(effectElements.Length != 3)
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

            int value;
            if (!int.TryParse(effectElements[2], out value))
            {
                return false;
            }


            return true;
        }
    }
}
