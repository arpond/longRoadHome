using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class SubLocationFactory
    {
        private static Dictionary<String, Sublocation> registeredSublocations = new Dictionary<string,Sublocation>();
        
        /// <summary>
        /// Registers a sublocation with the factory
        /// </summary>
        /// <param name="subTypeID">ID of the sublocation type</param>
        /// <param name="subloc">Instance of the sublocation</param>
        public static void RegisterSubLocation(String subTypeID, Sublocation subloc)
        {
            registeredSublocations.Add(subTypeID, subloc);
        }

        /// <summary>
        /// Creates a sublcoation with the params passed
        /// </summary>
        /// <param name="subTypeID">The type of sublocation to create</param>
        /// <param name="sublocID">Id of the sublocation</param>
        /// <param name="maxItems">Maximum number of items</param>
        /// <param name="maxAmount">Maximum amount of each item</param>
        /// <returns>The sublcoation created</returns>
        public Sublocation CreateSubLocation(String subTypeID, int sublocID, int maxItems, int maxAmount)
        {
            Sublocation temp;
            registeredSublocations.TryGetValue(subTypeID, out temp);
            return temp.CreateSublocation(sublocID, maxItems, maxAmount);
        }

        /// <summary>
        /// Creates a sublocation with the string passed
        /// </summary>
        /// <param name="toParse">The string to parse into a sublcoation</param>
        /// <returns>The sublocation created</returns>
        public Sublocation CreateSubLocation(String toParse)
        {
            Sublocation temp;
            String[] slElem = toParse.Split(':');
            registeredSublocations.TryGetValue(slElem[0], out temp);
            return temp.CreateSublocation(toParse);
        }
    }
}
