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
        public const String TAG = "PlayerChar";

        /// <summary>
        /// Generates a standard PC
        /// </summary>
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

        /// <summary>
        /// Generates a PC based on the string input
        /// Check the string is valid before calling this function
        /// </summary>
        /// <param name="toParse">String to be converted into a PC</param>
        public PlayerCharacter(String toParse)
        {
            this.primaryResources = new Dictionary<string, PrimaryResource>();
            this.modifierMap = new Dictionary<PrimaryResource, float>();

            String[] resources = toParse.Split(',');
            foreach (String curr in resources)
            {
                String[] resource = curr.Split(':');
                int amount;
                float mod;
                if (int.TryParse(resource[1], out amount) && float.TryParse(resource[2], out mod))
                {
                    PrimaryResource temp = new PrimaryResource(amount, resource[0]);
                    primaryResources.Add(resource[0], temp);
                    modifierMap.Add(temp, mod);
                }
                
            }
        }

        /// <summary>
        /// Adjusts a resouce based on the resouce name passed and the adjustment value
        /// The resource should be one of the public constants
        /// </summary>
        /// <param name="resourceName">Name of the resource to modify</param>
        /// <param name="adjustment">Adjustment to make</param>
        public void AdjustResource(String resourceName, int adjustment)
        {
            PrimaryResource resource = GetPrimaryResource(resourceName);
            if (resource != null)
            {
                int currentResounce = resource.GetAmount();
                if (adjustment < 0)
                {
                    adjustment = ApplyModifier(resource, adjustment);
                }
                int result = currentResounce + adjustment;

                resource.SetAmount(result);
                primaryResources[resourceName] = resource;
            }
        }

        /// <summary>
        /// Applies the modifier to the adjustment to a minimum of 1
        /// </summary>
        /// <param name="res">The resource to get the modifer for</param>
        /// <param name="adjustment">The adjustment to make</param>
        /// <returns></returns>
        private int ApplyModifier(PrimaryResource res, int adjustment)
        {
            float mod = GetModifier(res);
            adjustment = Convert.ToInt32(adjustment * mod);
            if (Math.Abs(adjustment) < 1)
            {
                return -1;
            }
            return adjustment;
        }

        /// <summary>
        /// Gets the requested resource
        /// The resource should be one of the public constants
        /// </summary>
        /// <param name="resourceName">Name of the resource to get</param>
        /// <returns></returns>
        public int GetResource(String resourceName)
        {
            PrimaryResource resource = GetPrimaryResource(resourceName);
            if (resource != null)
            {
                return resource.GetAmount();
            }
            return -1;
        }

        /// <summary>
        /// Parses this PC to a string format suitable for saving
        /// </summary>
        /// <returns>The parsed PC</returns>
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

        /// <summary>
        /// Updates the modifiers with new modifiers
        /// </summary>
        /// <param name="modifiers">List of modifiers to be updated</param>
        public void UpdateModifers(List<PassiveEffect> modifiers)
        {
            foreach(PassiveEffect mod in modifiers)
            {
                PrimaryResource res = GetPrimaryResource(mod.GetResourceName());
                if (res != null)
                {
                    float modifier;
                    if (modifierMap.TryGetValue(res, out modifier))
                    {
                        modifierMap[res] = mod.GetModifier();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the resource from the dictionary
        /// The resource should be one of the public constants if not returns null
        /// </summary>
        /// <param name="resourceName">The resource to get</param>
        /// <returns>The primary resource requested</returns>
        private PrimaryResource GetPrimaryResource(String resourceName)
        {
            PrimaryResource resource;
            primaryResources.TryGetValue(resourceName, out resource);
            return resource;
        }

        /// <summary>
        /// Gets the modifier from the dictionary
        /// The resource should be one of the public constants if not returns null
        /// </summary>
        /// <param name="resourceName">The resource to get</param>
        /// <returns>The modifier requested</returns>
        private float GetModifier(PrimaryResource resource)
        {
            float modifier;
            modifierMap.TryGetValue(resource, out modifier);
            return modifier;
        }

        /// <summary>
        /// Checks if a string is a valid PC string
        /// </summary>
        /// <param name="toTest">The string to test</param>
        /// <returns>If the string is valid or invalid</returns>
        public static bool IsValidPC(String toTest)
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

                int amount;
                if (int.TryParse(resource[1], out amount))
                {
                    if (amount > 100 || amount <= 0)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }


                float mod;
                if (!float.TryParse(resource[2], out mod))
                {
                    return false;
                }

            }
            return true;
        }

        /// <summary>
        /// Tests if the character is dead
        /// </summary>
        /// <returns>bool representing if character is dead</returns>
        public bool IsDead()
        {
            var resources = primaryResources.Values;

            foreach(PrimaryResource res in resources)
            {
                if (res.GetAmount() <= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
