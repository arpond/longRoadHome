using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class PlayerCharacter
    {
        private Dictionary<String, PrimaryResource> primaryResources;
        private Dictionary<PrimaryResource, float> modifierMap;

        public const String HEALTH = "health";
        public const String HUNGER = "hunger";
        public const String THIRST = "thirst";
        public const String SANITY = "sanity";

        public PlayerCharacter()
        {
            PrimaryResource health = new PrimaryResource(100, HEALTH);
            PrimaryResource hunger = new PrimaryResource(100, HUNGER);
            PrimaryResource thirst = new PrimaryResource(100, THIRST);
            PrimaryResource sanity = new PrimaryResource(100, SANITY);

            this.primaryResources = new Dictionary<string, PrimaryResource>();

            primaryResources.Add(HEALTH, health);
            primaryResources.Add(HUNGER, hunger);
            primaryResources.Add(THIRST, thirst);
            primaryResources.Add(SANITY, sanity);

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

        public void AdjustResource(String resourceName, int adjustment)
        {
            PrimaryResource resource;
            if (primaryResources.TryGetValue(resourceName, out resource))
            {
                int currentResounce = resource.GetAmount();
                int result = currentResounce + adjustment;

                if (result < 0)
                {
                    result = 0;
                }
                else if (result > 100)
                {
                    result = 100;
                }

                resource.SetAmount(result);
                primaryResources[resourceName] = resource;
            }
        }

        public int GetResource(String resourceName)
        {
            PrimaryResource resource;
            if (primaryResources.TryGetValue(resourceName, out resource))
            {
                return resource.GetAmount();
            }
            return -1;
        }
        public String ParseToString()
        {
            throw new System.Exception("Not implemented");
        }
        public void RecalculateModifers(ref List<PassiveMod> modifiers)
        {
            throw new System.Exception("Not implemented");
        }
    }
}
