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

        public ActiveEffect(String toParse)
        {
            String[] effectElements = toParse.Split(':');
            if (int.TryParse(effectElements[2], out value))
            {
                resource = new PrimaryResource(value, effectElements[1]);
            }
        }

        public int GetValue()
        {
            return this.value;
        }

        public PrimaryResource GetResource()
        {
            return this.resource;
        }

        public void ResolveEffect(ref object float_eventModifier)
        {
            throw new System.Exception("Not implemented");
        }

        public string ParseToString()
        {
            return String.Format("{0}:{1}:{2}", TAG, resource.GetName(), resource.GetAmount());
        }

        public static bool isValidActiveEffect(String toTest)
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
