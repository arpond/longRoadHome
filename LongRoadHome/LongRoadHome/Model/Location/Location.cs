using System;
using System.Collections.Generic;
using System.Linq;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class Location : DummyLocation
    {
        private const String TAG = "Location";
        private const int STD_SIZE = 3;
        private const int STD_MAX_ITEMS = SubLocationFactory.STD_MAX_ITEMS;
        private const int STD_MAX_AMOUNT = SubLocationFactory.STD_MAX_AMOUNT;

        private bool visited;
        private Dictionary<int, Sublocation> sublocations;
        private Sublocation currentSubLocation;
        private Random rnd = new Random();
        
        /// <summary>
        /// Standard Constructor defaults to constant values
        /// </summary>
        public Location() : base()
        {
            visited = false;
            sublocations = new Dictionary<int, Sublocation>();
            currentSubLocation = null;
        }

        public Location(int id, HashSet<int> connections) : base(id, connections)
        {
            visited = false;
            sublocations = new Dictionary<int, Sublocation>();
            currentSubLocation = null;
        }

        public Location(String toParse)
        {
            visited = false;
            sublocations = new Dictionary<int, Sublocation>();
            String[] lElems = toParse.Split(',');
            foreach (String elem in lElems)
            {
                String[] locElem = elem.Split(':');
                switch (locElem[0])
                {
                    case "ID":
                        int.TryParse(locElem[1], out locationID);
                        break;
                    case "Connections":
                        for (int i = 1; i < locElem.Length; i++)
                        {
                            int loc;
                            if (int.TryParse(locElem[i], out loc))
                            {
                                connections.Add(loc);
                            }
                        }
                        break;
                    case "Visited":
                        bool.TryParse(locElem[1], out visited);
                        break;
                    case "Sublocations":
                        if (locElem.Length > 1)
                        {
                            for (int i = 1; i < locElem.Length; i = i + 6)
                            {
                                Sublocation temp = SubLocationFactory.CreateSubLocation(locElem[i] + ":" + locElem[i + 1] + ":" + locElem[i + 2] + ":" + locElem[i + 3] + ":" + locElem[i + 4] + ":" + locElem[i + 5]);
                                int id;
                                if (int.TryParse(locElem[i+1], out id))
                                {
                                    sublocations.Add(id, temp);
                                }
                            }
                        }
                        break;
                    case "CurrentSublocation":
                        int currID;
                        if (locElem.Length > 1)
                        {
                            int.TryParse(locElem[1], out currID);
                            sublocations.TryGetValue(currID, out currentSubLocation);   
                        }           
                        break;
                }
            }
        }
     


        public static Location ConvertToLocation(DummyLocation dl)
        {
            return new Location(dl.GetLocationID(), dl.GetConnections());
        }

        /// <summary>
        /// Generates Sub Location with default constants
        /// </summary>
        public void GenerateSubLocations()
        {
            sublocations = new Dictionary<int, Sublocation>();
            var keys = SubLocationFactory.GetRegisteredTypes().ToList();

            for (int i = 1; i < STD_SIZE + 1; i++)
            {
                sublocations.Add(i, GenerateRandomSublocation(i, keys));
            }
            currentSubLocation = null;
        }

        /// <summary>
        /// Size constructor defaults to constant values for everything except size
        /// Invalid Size (size<=0) is set to STD_SIZE
        /// </summary>
        /// <param name="size">Size of the location (Number of sublocations)</param>
        public void GenerateSubLocations(int size)
        {
            if (size <= 0)
            {
                size = STD_SIZE;
            }
            visited = false;
            sublocations = new Dictionary<int, Sublocation>();
            var keys = SubLocationFactory.GetRegisteredTypes().ToList();

            for (int i = 1; i < size + 1; i++)
            {
                sublocations.Add(i, GenerateRandomSublocation(i, keys));
            }
            currentSubLocation = null;
        }

        /// <summary>
        /// Generates sub locations based on attibutes given
        /// Invalid params are set to standard values
        /// </summary>
        /// <param name="size">Size of the location (Number of sublocations)</param>
        /// <param name="maxItems">Max items to be found at each sublocation</param>
        /// <param name="maxAmount">Max amount of each item to be found at each sublocation</param>
        public void GenerateSubLocations(int size, int maxItems, int maxAmount)
        {
            if (size <= 0)
            {
                size = STD_SIZE;
            }
            if (maxItems <= 0)
            {
                maxItems = STD_MAX_ITEMS;
            }
            if (maxAmount <= 0)
            {
                maxAmount = STD_MAX_AMOUNT;
            }
            visited = false;
            sublocations = new Dictionary<int, Sublocation>();
            var keys = SubLocationFactory.GetRegisteredTypes().ToList();

            for (int i = 1; i < size + 1; i++)
            {
                sublocations.Add(i, GenerateRandomSublocation(i, keys, maxItems, maxAmount));
            }
            currentSubLocation = null;
        }

        /// <summary>
        /// Generates sub locations based on attibutes given
        /// Invalid params are set to standard values
        /// </summary>
        /// <param name="sizeMin">Minimum size</param>
        /// <param name="sizeMax">Maximum size</param>
        /// <param name="maxItems">Max items to be found at each sublocation</param>
        /// <param name="maxAmount">Max amount of each item to be found at each sublocation</param>
        public void GenerateSubLocations(int sizeMin, int sizeMax, int maxItems, int maxAmount)
        {
            if (sizeMin <= 0)
            {
                sizeMin = 1;
            }
            if (sizeMax <= 0)
            {
                sizeMax = STD_SIZE;
            }
            if (sizeMax < sizeMin)
            {
                sizeMax = sizeMin + 1;
            }
            if (maxItems <= 0)
            {
                maxItems = STD_MAX_ITEMS;
            }
            if (maxAmount <= 0)
            {
                maxAmount = STD_MAX_AMOUNT;
            }
            visited = false;
            sublocations = new Dictionary<int, Sublocation>();
            var keys = SubLocationFactory.GetRegisteredTypes().ToList();

            int size = rnd.Next(sizeMin, sizeMax + 1);

            for (int i = 1; i < size + 1; i++)
            {
                sublocations.Add(i, GenerateRandomSublocation(i, keys, maxItems, maxAmount));
            }
            currentSubLocation = null;
        }


        /// <summary>
        /// Generates a standard sublocation
        /// </summary>
        /// <param name="id">ID of the sublocation</param>
        /// <param name="keys">Types of sublocations available</param>
        /// <returns></returns>
        private Sublocation GenerateRandomSublocation(int id, List<String> keys)
        {
            int nextType = rnd.Next(keys.Count);
            String tempType = keys[nextType];

            return SubLocationFactory.CreateSubLocation(tempType, id, STD_MAX_ITEMS, STD_MAX_AMOUNT);            
        }

        /// <summary>
        /// Generates a custom sublocation
        /// </summary>
        /// <param name="id">ID of the sublocation</param>
        /// <param name="keys">Types of sublocations available</param>
        /// <param name="maxItems">Max items to be found at sublocation</param>
        /// <param name="maxAmount">Max amount of each item to be found at sublocation</param>
        /// <returns></returns>
        private Sublocation GenerateRandomSublocation(int id, List<String> keys, int maxItems, int maxAmount)
        {
            int nextType = rnd.Next(keys.Count);
            String tempType = keys[nextType];

            return SubLocationFactory.CreateSubLocation(tempType, id, maxItems, maxAmount);  
        }

        /// <summary>
        /// Parses a sublocation from a string
        /// </summary>
        /// <param name="toParse">The string to parse to a sublocation</param>
        /// <returns>The sublocation parsed</returns>
        private Sublocation ParseSublocationFromString(String toParse)
        {
            return SubLocationFactory.CreateSubLocation(toParse);
        }

        /// <summary>
        /// Checks if the current sublocation is null
        /// </summary>
        /// <returns>If the sublocation is null or not</returns>
        public bool IsCurrentSublocationNull()
        {
            if (currentSubLocation == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Accessor method for visited
        /// </summary>
        /// <returns>Visited bool</returns>
        public bool GetVisited()
        {
            return this.visited;
        }

        /// <summary>
        /// Sets a location to be visited
        /// </summary>
        public void SetVisited()
        {
            visited = true;
        }

        /// <summary>
        /// Accesor method for current sublocation
        /// </summary>
        /// <returns>The current sublocation</returns>
        public Sublocation GetCurrentSubLocation()
        {
            return this.currentSubLocation;
        }

        /// <summary>
        /// Accesor method for the size of the location
        /// </summary>
        /// <returns>Size of the location</returns>
        public int GetSize()
        {
            return sublocations.Count;
        }

        /// <summary>
        /// Gets a sublocation by ID
        /// </summary>
        /// <param name="id">ID of the sublocation to get</param>
        /// <returns>The sublocation ID</returns>
        public Sublocation GetSublocationByID(int id)
        {
            Sublocation temp;
            sublocations.TryGetValue(id, out temp);
            return temp;
        }

        /// <summary>
        /// Trys to set the current sublocation to the ID passed
        /// </summary>
        /// <param name="sublocationID">ID of the sublocation</param>
        /// <returns>If the sublocation was successfully set or not</returns>
        public bool SetCurrentSubLocation(int sublocationID)
        {
            Sublocation temp;
            if (sublocations.TryGetValue(sublocationID, out temp))
            {
                currentSubLocation = temp;
                return true;
            }
            return false;
        }

        public static bool IsValidLocation(String toTest)
        {
            HashSet<int> tempSubID = new HashSet<int>();
            HashSet<int> tempID = new HashSet<int>();
            bool visited;
            int id = -1;
            String[] lElems = toTest.Split(',');
            if (lElems.Length != 6)
            {
                return false;
            }
            foreach (String elem in lElems)
            {
                String[] locElem = elem.Split(':');
                switch (locElem[0])
                {
                    case "Type":
                        if (locElem.Length != 2 || locElem[1] != TAG)
                        {
                            return false;
                        }
                        break;
                    case "ID":
                        if (locElem.Length != 2 || !int.TryParse(locElem[1], out id) || id <= 0)
                        {
                            return false;
                        }
                        
                        break;
                    case "Connections":
                        for (int i = 1; i < locElem.Length; i++)
                        {
                            int loc;
                            if (int.TryParse(locElem[i], out loc))
                            {
                                if (tempID.Contains(loc) || loc == id || loc <= 0)
                                {
                                    return false;
                                }
                                tempID.Add(loc);
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;
                    case "Visited":
                        if (locElem.Length != 2 || !bool.TryParse(locElem[1], out visited))
                        {
                            return false;
                        }
                        break;
                    case "Sublocations":
                        if (locElem.Length > 1)
                        {
                            if (locElem.Length % 6 != 1 || locElem[1] == "")
                            {
                                return false;
                            }
                            for (int i = 1; i < locElem.Length; i = i + 6)
                            {
                                int slid;
                                Sublocation type = SubLocationFactory.GetRegisteredSub(locElem[i]);
                                if (type == null || !int.TryParse(locElem[i + 1], out slid) || tempSubID.Contains(slid) || !type.IsValidSublocation(locElem[i] + ":" + locElem[i + 1] + ":" + locElem[i + 2] + ":" + locElem[i + 3] + ":" + locElem[i + 4] + ":" + locElem[i + 5]))
                                {
                                    return false;
                                }
                                tempSubID.Add(slid);
                            }
                        }
                        break;
                    case "CurrentSublocation":
                        int currID;
                        if (locElem.Length > 1)
                        {
                            if (locElem.Length != 2 || !int.TryParse(locElem[1], out currID) || currID <= 0 || !tempSubID.Contains(currID))
                            {
                                return false;
                            }
                        }
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }

        public override String ParseToString()
        {
            String parsed, currentSubloc = "", sublocStr = "";

            foreach (Sublocation subloc in sublocations.Values)
            {
                sublocStr += ":" + subloc.ParseToString();
            }

            if(currentSubLocation != null)
            {
                currentSubloc = ":" + currentSubLocation.GetSublocationID();
            }

            parsed = String.Format("Type:{0},ID:{1},Connections{2},Visited:{3},Sublocations{4},CurrentSublocation{5}",TAG,locationID,ParseConnections(),visited, sublocStr, currentSubloc);
            return parsed;
        }
    }
}
