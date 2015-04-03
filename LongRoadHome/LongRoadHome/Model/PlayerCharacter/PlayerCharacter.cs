using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class PlayerCharacter
    {
        private PrimaryResource health;
        private PrimaryResource hunger;
        private PrimaryResource thirst;
        private PrimaryResource sanity;
        private Dictionary<PrimaryResource, float> modifierMap;

        public PlayerCharacter()
        {
            this.health = new PrimaryResource(100, "health");
            this.hunger = new PrimaryResource(100, "hunger");
            this.thirst = new PrimaryResource(100, "thirst");
            this.sanity = new PrimaryResource(100, "sanity");

            this.modifierMap = new Dictionary<PrimaryResource, float>();

            modifierMap.Add(health, 1.0f);
            modifierMap.Add(hunger, 1.0f);
            modifierMap.Add(thirst, 1.0f);
            modifierMap.Add(sanity, 1.0f);
        }

        public PlayerCharacter(ref String pc)
        {
            throw new System.Exception("Not implemented");
        }
        public void AdjustHealth(int adjustment)
        {
            int currentHealth = health.GetAmount();
            health.SetAmount(currentHealth + adjustment);
        }
        public void AdjustHunger(int adjustment)
        {
            int currentHunger = hunger.GetAmount();
            hunger.SetAmount(currentHunger + adjustment);
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
        public void RecalculateModifers(ref List<PassiveMod> modifiers)
        {
            throw new System.Exception("Not implemented");
        }

        private PrimaryResource primaryReource;

        private PCModel pCModel;

    }
}
