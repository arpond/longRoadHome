using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Discovery
{
    public class DiscoveryModel
    {
        public const String DISCOVERED_TAG = "Discovered";
        private HashSet<int> discovered;
        private DiscoveryCatalogue dc;

        public DiscoveryModel()
        {
            discovered = new HashSet<int>();
            dc = new DiscoveryCatalogue();
        }

        /// <summary>
        /// Constructor for new game
        /// </summary>
        /// <param name="catalogue">Discovery Catalogue string</param>
        public DiscoveryModel(String catalogue)
        {
            discovered = new HashSet<int>();
            dc = new DiscoveryCatalogue(catalogue);
        }

        /// <summary>
        /// Constructor for loading from save game
        /// </summary>
        /// <param name="discovered">Discovered string</param>
        /// <param name="catalogue">Discovery catalogue string</param>
        public DiscoveryModel(String discovered, String catalogue)
        {
            this.discovered = new HashSet<int>();
            dc = new DiscoveryCatalogue(catalogue);

            String[] discoveredElems = discovered.Split(':');
            for (int i = 1; i < discoveredElems.Length; i++)
            {
                int id;
                if (int.TryParse(discoveredElems[i], out id))
                {
                    this.discovered.Add(id);
                }
            }
        }

        /// <summary>
        /// Parses discovered to String
        /// </summary>
        /// <returns>Discovered as a string</returns>
        public String ParseDiscoveredToString()
        {
            String disc = DISCOVERED_TAG;
            foreach (int id in discovered)
            {
                disc += ":" + id;
            }
            return disc;
        }

        /// <summary>
        /// Parses the catalogue to a string
        /// </summary>
        /// <returns>The parsed catalogue</returns>
        public String ParseCatalogueToString()
        {
            return dc.ParseToString();
        }

        /// <summary>
        /// Gets a new discovery
        /// </summary>
        /// <param name="numOfVisited">Number of visited locations</param>
        /// <returns>Discovery text if one is found (ie not previously found and requirements met)</returns>
        public String GetNewDiscovery(int numOfVisited)
        {
            Discovery disc = dc.GetRandomDiscovery();
            if(discovered.Count == 0)
            {
                disc = dc.GetDiscovery(1);
            }
            if (discovered.Contains(disc.GetDiscoveryID()) || !disc.IsDiscoverable(numOfVisited))
            {
                return "";
            }

            discovered.Add(disc.GetDiscoveryID());
            return disc.GetDiscoveryText();
        }

        /// <summary>
        /// Accessor method for discovered
        /// </summary>
        /// <returns>Discovered</returns>
        public HashSet<int> GetDiscovered()
        {
            return discovered;
        }

        /// <summary>
        /// Accessor method for discovery catalogue
        /// </summary>
        /// <returns>The discovery catalogue</returns>
        public DiscoveryCatalogue GetDiscoveryCatalogue()
        {
            return dc;
        }

        /// <summary>
        /// Checks if a string is a valid discovery catalogue
        /// </summary>
        /// <param name="toTest">String to check</param>
        /// <returns>If it is valid or not</returns>
        public static bool IsValidDiscoveryCatalogue(String toTest)
        {
            return DiscoveryCatalogue.IsValidDiscoveryCatalogue(toTest);
        }

        /// <summary>
        /// Checks if a string is a valid set of discovered
        /// </summary>
        /// <param name="toTest">String to check</param>
        /// <returns>If it is valid or not</returns>
        public static bool IsValidDiscovered(String toTest)
        {
            HashSet<int> tempID = new HashSet<int>();
            String[] discoveredElems = toTest.Split(':');
            if (discoveredElems[0] != DISCOVERED_TAG)
            {
                return false;
            }
            for (int i = 1; i < discoveredElems.Length; i++)
            {
                int id;
                if (int.TryParse(discoveredElems[i], out id))
                {
                    if (tempID.Contains(id) || id < 0)
                    {
                        return false;
                    }
                    tempID.Add(id);
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