using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Discovery
{
    public class DiscoveryCatalogue
    {
        public const String TAG = "DiscoveryCatalogue";
        private SortedList<int, Discovery> discoveries;

        public DiscoveryCatalogue()
        {
            discoveries = new SortedList<int, Discovery>();
        }

        public DiscoveryCatalogue(String toParse)
        {
            discoveries = new SortedList<int, Discovery>();
            String[] cataElements = toParse.Split('#');
            for(int i = 1; i<cataElements.Length; i++)
            {
                Discovery temp = new Discovery(cataElements[i]);
                discoveries.Add(temp.GetDiscoveryID(), temp);
            }
        }

        /// <summary>
        /// Accessor Method for discoveries
        /// </summary>
        /// <returns>List of discoveries</returns>
        public SortedList<int, Discovery> GetDiscoveries()
        {
            return discoveries;
        }

        /// <summary>
        /// Gets the discovery with the ID if it exists
        /// </summary>
        /// <param name="id">ID to get</param>
        /// <returns></returns>
        public Discovery GetDiscovery(int id)
        {
            Discovery dc;
            discoveries.TryGetValue(id, out dc);
            return dc;
        }

        /// <summary>
        /// Parses this catalogue to a string
        /// </summary>
        /// <returns>The catalogue as a string</returns>
        public String ParseToString()
        {
            String parsed = TAG;
            foreach(Discovery dc in discoveries.Values)
            {
                parsed += "#" + dc.ParseToString();
            }
            return parsed;
        }

        /// <summary>
        /// Checks if a string is a valid discovery catalogue
        /// </summary>
        /// <param name="toTest">String to check</param>
        /// <returns>If the string is a valid catalogue</returns>
        public static bool IsValidDiscoveryCatalogue(String toTest)
        {
            String[] cataElements = toTest.Split('#');
            if (cataElements.Length < 1 || cataElements[0] != TAG)
            {
                return false;
            }
            HashSet<int> ids = new HashSet<int>();
            for (int i = 1; i < cataElements.Length; i++)
            {
                String[] discElements = cataElements[i].Split(':');
                if (!Discovery.IsValidDiscovery(cataElements[i]))
                {
                    return false;
                }
                int id;
                if (int.TryParse(discElements[1],out id) && !ids.Contains(id))
                {
                    ids.Add(id);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

    }
}
