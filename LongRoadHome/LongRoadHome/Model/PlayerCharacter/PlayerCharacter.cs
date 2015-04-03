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

        public PlayerCharacter(String input)
        {
            this.primaryResources = new Dictionary<string, PrimaryResource>();
            this.modifierMap = new Dictionary<PrimaryResource, float>();

            String[] resources = input.Split(',');
            foreach (String curr in resources)
            {
                String[] resource = curr.Split(':');
                PrimaryResource temp = new PrimaryResource(Convert.ToInt32(resource[1]), resource[0]);
                primaryResources.Add(resource[0], temp);
                modifierMap.Add(temp, Convert.ToSingle(resource[2]));
            }
        }

        public void AdjustResource(String resourceName, int adjustment)
        {
            PrimaryResource resource = GetPrimaryResource(resourceName);
            if (resource != null)
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
            PrimaryResource resource = GetPrimaryResource(resourceName);
            if (resource != null)
            {
                return resource.GetAmount();
            }
            return -1;
        }

        public String ParseToString()
        {
            String parsed = "";

            PrimaryResource health = GetPrimaryResource(HEALTH);
            PrimaryResource hunger = GetPrimaryResource(HUNGER);
            PrimaryResource thirst = GetPrimaryResource(THIRST);
            PrimaryResource sanity = GetPrimaryResource(SANITY);
            if (health == null || hunger == null || thirst == null || sanity == null)
            {
                parsed = "ERROR";
            }
            else
            {
                parsed = health.ParseToString() + ":" + GetModifier(health) + ","
                       + hunger.ParseToString() + ":" + GetModifier(hunger) + ","
                       + thirst.ParseToString() + ":" + GetModifier(thirst) + "," 
                       + sanity.ParseToString() + ":" + GetModifier(sanity);
            }

            return parsed;
        }

        public void RecalculateModifers(ref List<PassiveMod> modifiers)
        {
            throw new System.Exception("Not implemented");
        }

        private PrimaryResource GetPrimaryResource(String resourceName)
        {
            PrimaryResource resource;
            primaryResources.TryGetValue(resourceName, out resource);
            return resource;
        }

        private float GetModifier(PrimaryResource resource)
        {
            float modifier;
            modifierMap.TryGetValue(resource, out modifier);
            return modifier;
        }

        public static bool isValidPC(String toTest)
        {
            String[] resources = toTest.Split(',');
            if (resources.GetLength(0) != 4)
            {
                return false;
            }
            for (int i = 0; i < 4; i++)
            {
                String curr = resources[i];
                String[] resource = curr.Split(':');

                if (resource.GetLength(0) != 3)
                {
                    return false;
                }

                switch (i)
                {
                    case 0: if (resource[0] != HEALTH)
                            {
                                return false;
                            }
                            break;
                    case 1: if (resource[0] != HUNGER)
                            {
                                return false;
                            }
                            break;
                    case 2: if (resource[0] != THIRST)
                            {
                                return false;
                            }
                            break;
                    case 3: if (resource[0] != SANITY)
                            {
                                return false;
                            }
                            break;
                }

                try
                {
                    int amount = Convert.ToInt32(resource[1]);
                    float mod = Convert.ToSingle(resource[2]);

                    if (amount > 100 || amount <= 0)
                    {
                        return false;
                    }
                }
                catch (FormatException fe)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
