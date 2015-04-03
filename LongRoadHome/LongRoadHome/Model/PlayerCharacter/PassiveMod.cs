using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class PassiveMod
    {
        private String resourceName;
        private float modifierVal;

        public PassiveMod(String resourceName, float modifierVal)
        {
            this.resourceName = resourceName;
            this.modifierVal = modifierVal;
        }

        public String GetResourceName()
        {
            return this.resourceName;
        }
        public float GetModifier()
        {
            return this.modifierVal;
        }

    }
}
