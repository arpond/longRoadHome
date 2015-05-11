using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class Commercial : Sublocation
    {
        public const String TYPE = "Commercial";

        static Commercial()
        {
            Commercial.RegisterSublocation();
        }

        public Commercial()
        {

        }

        public Commercial(String toParse)
        {
            String[] sublocationElem = toParse.Split(':');
            int.TryParse(sublocationElem[1], out sublocationID);
            bool.TryParse(sublocationElem[2], out scavenged);
            int.TryParse(sublocationElem[3], out maxItems);
            int.TryParse(sublocationElem[4], out maxAmount);
            imagePath = sublocationElem[5];
        }

        public Commercial(int sublocID, int maxItems, int maxAmount)
        {
            sublocationID = sublocID;
            scavenged = false;
            this.maxItems = maxItems;
            this.maxAmount = maxAmount;
            imagePath = "temp";
        }

        /// <summary>
        /// Registers the sublocation with the factory
        /// </summary>
        public static void RegisterSublocation()
        {
            SubLocationFactory.RegisterSubLocation(TYPE, new Commercial());
        }

        /// <summary>
        /// Parses this sublocation to a string
        /// </summary>
        /// <returns>The parsed string</returns>
        public override String ParseToString()
        {
            return String.Format("{0}:{1}:{2}:{3}:{4}:{5}", TYPE, sublocationID, scavenged, maxItems, maxAmount, imagePath);
        }

        public override bool IsValidSublocation(String toTest)
        {
            String[] slElem = toTest.Split(':');
            int id, maxI, maxA;
            bool scav;

            if (slElem.Length != 6 || slElem[0] != TYPE)
            {
                return false;
            }
            if (!int.TryParse(slElem[1], out id) || !bool.TryParse(slElem[2], out scav) || !int.TryParse(slElem[3], out maxI) || !int.TryParse(slElem[4], out maxA))
            {
                return false;
            }
            if (id < 1 || maxI < 1 || maxA < 1)
            {
                return false;
            }
            if (slElem[5] == "")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Creates a new Commercial sublocation with the values passed
        /// </summary>
        /// <param name="sublocID">Id of the sublocation</param>
        /// <param name="maxItems">Maximum number of items</param>
        /// <param name="maxAmount">Maximum amount of each item</param>
        /// <returns>The sublcoation created</returns>
        public override Sublocation CreateSublocation(int sublocID, int maxItems, int maxAmount)
        {
            return new Commercial(sublocID, maxItems, maxAmount);
        }

        /// <summary>
        /// Creates a new Commercial sublocation with the string passed
        /// </summary>
        /// <param name="toParse">The string to make a sublocation from</param>
        /// <returns>The sublocation created</returns>
        public override Sublocation CreateSublocation(String toParse)
        {
            return new Commercial(toParse);
        }
    }
}
