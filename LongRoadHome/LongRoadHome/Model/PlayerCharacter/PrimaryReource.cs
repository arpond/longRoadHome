using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class PrimaryResource : Resource
    {
        /// <summary>
        /// Creates a new primary resource
        /// </summary>
        /// <param name="amount">The amount of this resource</param>
        /// <param name="name">The name of the resource</param>
        public PrimaryResource(int amount, String name)
        {
            this.amount = amount;
            this.name = name;
        }

        /// <summary>
        /// Gets the amount
        /// </summary>
        /// <returns>The resources current amount</returns>
        public override int GetAmount()
        {
            return this.amount;
        }

        //todo Potentially make the maximum customisable
        /// <summary>
        /// Sets the amount of the resource
        /// Max of 100, min of 0
        /// </summary>
        /// <param name="amount">The amount to set the resource to</param>
        public override void SetAmount(int amount)
        {
            if (amount > 100)
            {
                this.amount = 100;
            }
            else if (amount < 0)
            {
                this.amount = 0;
            }
            else
            {
                this.amount = amount;
            }
        }

        /// <summary>
        /// Gets the name of the resource
        /// </summary>
        /// <returns>Name of the resource</returns>
        public override String GetName()
        {
            return this.name;
        }

        /// <summary>
        /// Parses the resource to a string suitable for saving
        /// </summary>
        /// <returns>The resource parsed to a string</returns>
        public override String ParseToString()
        {
            return name + ":" + amount;
        }
    }
}
