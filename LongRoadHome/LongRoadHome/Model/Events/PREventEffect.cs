using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events 
{
	public class PREventEffect : EventEffect  
    {
		
        public const String PR_EFFECT_TAG = "PREventEffect";
        private PrimaryResource resource;
        private Random rnd = new Random();

        public PREventEffect()
        {
            minimum = 0;
            maximum = 0;
            resource = new PrimaryResource(minimum, PlayerCharacter.PlayerCharacter.HEALTH);
        }

        /// <summary>
        /// Constructor for PREventEffect which takes a string
        /// </summary>
        /// <param name="toParse"></param>
        public PREventEffect(String toParse)
        {
            String[] effectElemnts = toParse.Split(':');
            if (int.TryParse(effectElemnts[2], out minimum) && int.TryParse(effectElemnts[3], out maximum))
            {
                resource = new PrimaryResource(minimum, effectElemnts[1]);
            }
        }

        /// <summary>
        /// Adjusts the player character with this events effect
        /// </summary>
        /// <param name="eventModifier">Modifier for this event</param>
        /// <param name="pcm">The player character model to modify with</param>
        public override void ResolveEffect(float eventModifier, PCModel pcm)
        {
            int value = rnd.Next(minimum, maximum);
            value = Convert.ToInt32(value * eventModifier);
            if(value < minimum)
            {
                value = minimum;
            }
            else if(value > maximum)
            {
                value = maximum;
            }
            pcm.ModifyPrimaryResource(resource, value);
        }

        /// <summary>
        /// Parases the PREvent Effect to a string
        /// </summary>
        /// <returns>The parsed event effect</returns>
        public override string ParseToString()
        {
            return String.Format("{0}:{1}:{2}:{3}", PR_EFFECT_TAG, resource.GetName(), minimum, maximum);
        }

        /// <summary>
        /// Accessor method for the resource
        /// </summary>
        /// <returns>The resource</returns>
        public PrimaryResource GetResource()
        {
            return resource;
        }

        /// <summary>
        /// Checks if a string is a valid PREvent Effect
        /// </summary>
        /// <param name="toTest">The string to check</param>
        /// <returns>If it is valid or invalid</returns>
        public static bool IsValidPREventEffect(String toTest)
        {
            String[] effectElements = toTest.Split(':');

            if (effectElements.Length != 4)
            {
                return false;
            }
            if (effectElements[0] != PR_EFFECT_TAG)
            {
                return false;
            }
            if (!(effectElements[1] == PlayerCharacter.PlayerCharacter.HEALTH || effectElements[1] == PlayerCharacter.PlayerCharacter.HUNGER || effectElements[1] == PlayerCharacter.PlayerCharacter.THIRST || effectElements[1] == PlayerCharacter.PlayerCharacter.SANITY))
            {
                return false;
            }

            int min, max;
            if (!int.TryParse(effectElements[2], out min))
            {
                return false;
            }

            if (!int.TryParse(effectElements[3], out max))
            {
                return false;
            }

            if (min > max)
            {
                return false;
            }


            return true;
        }

	}

}
