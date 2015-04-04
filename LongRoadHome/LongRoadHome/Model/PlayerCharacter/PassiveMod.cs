using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class PassiveMod
    {
        private String resourceName;
        private float modifierVal;

        /// <summary>
        /// Constructor for a passive modifier
        /// </summary>
        /// <param name="resourceName">Name of the resource the modifier is for</param>
        /// <param name="modifierVal">Value of the modifier</param>
        public PassiveMod(String resourceName, float modifierVal)
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

    }
}
