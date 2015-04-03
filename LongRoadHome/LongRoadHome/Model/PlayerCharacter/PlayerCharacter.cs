using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class PlayerCharacter
    {
        private PrimaryResource health;
        private PrimaryResource hunger;
        private PrimaryResource thirst;
        private PrimaryResource sanity;
        private HashMap<PrimaryResource, float> modifierMap;

        public PlayerCharacter(ref String pc)
        {
            throw new System.Exception("Not implemented");
        }
        public void AdjustHealth(ref int adjustment)
        {
            throw new System.Exception("Not implemented");
        }
        public void AdjustHunger(ref int adjustment)
        {
            throw new System.Exception("Not implemented");
        }
        public void AdjustThirst(ref int adjustment)
        {
            throw new System.Exception("Not implemented");
        }
        public void AdjustSanity(ref int adjustment)
        {
            throw new System.Exception("Not implemented");
        }
        public int GetHealth()
        {
            throw new System.Exception("Not implemented");
        }
        public int GetHunger()
        {
            throw new System.Exception("Not implemented");
        }
        public int GetThirst()
        {
            throw new System.Exception("Not implemented");
        }
        public int GetSanity()
        {
            throw new System.Exception("Not implemented");
        }
        public String ParseToString()
        {
            throw new System.Exception("Not implemented");
        }
        public void RecalculateModifers(ref ArrayList<PassiveMod> modifiers)
        {
            throw new System.Exception("Not implemented");
        }

        private PrimaryReource primaryReource;

        private PCModel pCModel;

    }
}
