using System;
using System.Collections;
using System.Collections.Generic;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class LocationModel
    {
        public const String VISITED_TAG = "VisitedLocations";
        public const String UNVISITED_TAG = "UnvisitedLocations";

        private const int STD_MIN_SIZE = 1, STD_MAX_SIZE = 5, STD_MAX_ITEMS = 5, STD_MAX_AMOUNT = 10;
        private const int MAX_CONNECTIONS = 4, MIN_CONNECTIONS = 2;

        private SortedList<int,Location> visitedLocation;
        private SortedList<int,DummyLocation> unvisitedLocation;

        private Location currentLocation;
        private Sublocation currentSublocation;

        private Random rnd = new Random();

        static LocationModel()
        {
            Civic civ = new Civic();
            Residential res = new Residential();
            Commercial com = new Commercial();
        }

        public LocationModel()
        {
            visitedLocation = new SortedList<int, Location>();
            unvisitedLocation = new SortedList<int, DummyLocation>();

        }

        /// <summary>
        /// Constructor for starting a new game
        /// </summary>
        /// <param name="numOfLocations">Number of locations to generate</param>
        public LocationModel(int numOfLocations)
        {
            visitedLocation = new SortedList<int, Location>();
            unvisitedLocation = new SortedList<int, DummyLocation>();

            InitializeLocationModel(numOfLocations);
        }

        /// <summary>
        /// Constructor for loading a game from save data
        /// </summary>
        /// <param name="visitedLocs">Visisted locations string</param>
        /// <param name="unvisitedLocs">Unvisited locations string</param>
        /// <param name="currLoc">Current location string</param>
        /// <param name="currSLoc">Current sublocation string</param>
        public LocationModel(String visitedLocs, String unvisitedLocs, String currLoc, String currSLoc)
        {
            visitedLocation = new SortedList<int, Location>();
            unvisitedLocation = new SortedList<int, DummyLocation>();

            String[] visitedElem = visitedLocs.Split('#');
            for (int i = 1; i < visitedElem.Length; i++ )
            {
                Location temp = new Location(visitedElem[i]);
                visitedLocation.Add(temp.GetLocationID(), temp);
            }

            String[] unvisitedElem = unvisitedLocs.Split('#');
            for(int j = 1; j < unvisitedElem.Length; j++ )
            {
                DummyLocation temp = new DummyLocation(unvisitedElem[j]);
                unvisitedLocation.Add(temp.GetLocationID(), temp);
            }

            int currID;
            if(int.TryParse(currLoc, out currID))
            {
                visitedLocation.TryGetValue(currID, out currentLocation);
            }

            int currSub;
            if (int.TryParse(currSLoc, out currSub))
            {
                if (currSub == 0)
                {
                    currentSublocation = null;
                }
                else
                {
                    currentSublocation = currentLocation.GetSublocationByID(currSub);
                }
            }
        }

        public static bool IsValidVisitedLocations(String toTest)
        {
            String[] visitedElem = toTest.Split('#');
            if (visitedElem[0] != VISITED_TAG)
            {
                return false;
            }
            for (int i = 1; i < visitedElem.Length; i++)
            {
                if(!Location.IsValidLocation(visitedElem[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsValidUnvisitedLocations(String toTest)
        {
            String[] visitedElem = toTest.Split('#');
            if (visitedElem[0] != UNVISITED_TAG)
            {
                return false;
            }
            for (int i = 1; i < visitedElem.Length; i++)
            {
                if (!DummyLocation.IsValidDummyLocation(visitedElem[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Parse visted locations to a string
        /// </summary>
        /// <returns>The string of visited locations</returns>
        public String ParseVisitedToString()
        {
            String parsed = "VisitedLocations";
            foreach(Location visited in visitedLocation.Values)
            {
                parsed += "#" + visited.ParseToString();
            }
            return parsed;
        }

        /// <summary>
        /// Parse unvisted locations to a string
        /// </summary>
        /// <returns>The string of unvisited locations</returns>
        public String ParseUnvisitedToString()
        {
            String parsed = "UnvisitedLocations";
            foreach (DummyLocation unvisited in unvisitedLocation.Values)
            {
                parsed += "#" + unvisited.ParseToString();
            }
            return parsed;
        }

        /// <summary>
        /// Parse the current location to string
        /// </summary>The current location as a string</returns>
        public String ParseCurrLocationToString()
        {
            return "" + currentLocation.GetLocationID();
        }

        /// <summary>
        /// Parse the current sub location to a string
        /// </summary>
        /// <returns>The current sub location as a string</returns>
        public String ParseCurrSubLocToString()
        {
            if (currentSublocation == null)
            {
                return "" + 0;
            }
            return "" + currentSublocation.GetSublocationID();
        }

        /// <summary>
        /// Replace a dummy location with an actual location
        /// </summary>
        /// <param name="dummy">Dummy location to replace</param>
        /// <param name="minSize">Minimum size of the location</param>
        /// <param name="maxSize">Max size of the location</param>
        /// <param name="maxItems">Max items for the location</param>
        /// <param name="maxAmount">Max amount of each item</param>
        /// <returns>A location with the values specified</returns>
        public Location ReplaceDummyLocation(DummyLocation dummy, int minSize, int maxSize, int maxItems, int maxAmount)
        {
            Location location = Location.ConvertToLocation(dummy);
            location.GenerateSubLocations(minSize, maxSize, maxItems, maxAmount);
            return location;
        }

        /// <summary>
        /// Gets the current sublocation
        /// </summary>
        /// <returns>Current sublocation</returns>
        public Sublocation GetSubLocation()
        {
            return this.currentSublocation;
        }

        /// <summary>
        /// Gets the type of the sublocation
        /// </summary>
        /// <returns>The type of the sublocation</returns>
        public String GetSublocType()
        {
            return currentSublocation.GetType().ToString();
        }

        /// <summary>
        /// Change the current sublocation
        /// </summary>
        /// <param name="subLocationID">The id of the sublocation to change to</param>
        public bool ChangeSubLocation(int subLocationID)
        {
            if (currentLocation.SetCurrentSubLocation(subLocationID))
            {
                currentSublocation = currentLocation.GetCurrentSubLocation();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a location as visited
        /// </summary>
        /// <param name="locationID">The id of the location to check</param>
        /// <returns>If the location is visited or unvisited</returns>
        public bool LocationVisited(int locationID)
        {
            return visitedLocation.ContainsKey(locationID);
        }

        /// <summary>
        /// Checks if the move to the locationID is valid
        /// </summary>
        /// <param name="locationID">The location ID to check</param>
        /// <returns>If current is connected to the ID</returns>
        public bool IsValidMove(int locationID)
        {
            HashSet<int> connections = currentLocation.GetConnections();
            return connections.Contains(locationID);
        }

        /// <summary>
        /// Moves from current location to the location ID
        /// Will not move if move is invalid
        /// </summary>
        /// <param name="locationID">The location ID to move to</param>
        /// <returns>If the move was successful</returns>
        public bool MoveToLocation(int locationID)
        {
            if(!IsValidMove(locationID) || locationID < 0 || locationID == currentLocation.GetLocationID() || (!visitedLocation.ContainsKey(locationID) && !unvisitedLocation.ContainsKey(locationID)))
            {
                return false;
            }

            if (LocationVisited(locationID))
            {
                return MoveToVisitedLocation(locationID);
            }
            else
            {
                return MoveToUnvisitedLocation(locationID);
            }
        }

        /// <summary>
        /// Moves from the current location to an unvisited location
        /// </summary>
        /// <param name="locationID">ID of the unvisited location</param>
        /// <returns>If the move was successful</returns>
        private bool MoveToUnvisitedLocation(int locationID)
        {
            DummyLocation toChangeTo;
            if (IsValidMove(locationID) && unvisitedLocation.TryGetValue(locationID, out toChangeTo))
            {
                Location temp = Location.ConvertToLocation(toChangeTo);

                int minSize = STD_MIN_SIZE;
                int maxSize = STD_MAX_SIZE;
                int maxItems = rnd.Next(1, STD_MAX_ITEMS+1);
                int maxAmount = rnd.Next(1, STD_MAX_AMOUNT+1);

                temp.GenerateSubLocations(minSize, maxSize, maxItems, maxAmount);
                temp.SetVisited();
                unvisitedLocation.Remove(locationID);
                visitedLocation.Add(locationID, temp);
                currentLocation = temp;
                currentSublocation = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Moves from the current location to a visited location
        /// </summary>
        /// <param name="locationID">ID of the visited location</param>
        /// <returns>If the move was successful</returns>
        private bool MoveToVisitedLocation(int locationID)
        {
            Location toChangeTo;
            if (IsValidMove(locationID) && visitedLocation.TryGetValue(locationID, out toChangeTo))
            {
                currentLocation = toChangeTo;
                currentSublocation = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Scavenges the current sublocation
        /// </summary>
        /// <param name="possibleItems">Items that can be found here</param>
        /// <returns>List of items found</returns>
        public List<Item> Scavenge(List<Item> possibleItems)
        {
            List<Item> itemsScavenged = new List<Item>();
            if (currentSublocation == null || currentSublocation.GetScavenged())
            {
                return itemsScavenged;
            }

            itemsScavenged = currentSublocation.Scavenge(possibleItems);

            return itemsScavenged;
        }

        /// <summary>
        /// Initializes Location Model 
        /// </summary>
        /// <param name="totalLocations">Total number of locations</param>
        public void InitializeLocationModel(int total)
        {
            CreateDummyLocations(total);
            ConnectUnvisitedIntoGroupsOfFour();
            GenerateConnections();
            
            HashSet<int> tempConn = new HashSet<int>();
            tempConn.Add(1);
            Location startLocation = new Location(0, tempConn);

            startLocation.SetVisited();
            startLocation.GenerateSubLocations();
            visitedLocation.Add(0, startLocation);

            DummyLocation dl1;
            if (unvisitedLocation.TryGetValue(1, out dl1))
            {
                dl1.AddConnection(0);
            }

            currentLocation = startLocation;
            currentSublocation = null;
        }



        /// <summary>
        /// Checks if the current sublocation is scavenged
        /// </summary>
        /// <returns>If the current sublocation is scavenged or not</returns>
        public bool IsScavenged()
        {
            return currentSublocation.GetScavenged();
        }

        /// <summary>
        /// Returns the lsit of unvisited locations
        /// </summary>
        /// <returns>Sorted list of unvisted locations</returns>
        public  SortedList<int, DummyLocation> GetUnvisited()
        {
            return this.unvisitedLocation;
        }

        public SortedList<int, Location> GetVisited()
        {
            return this.visitedLocation;
        }

        /// <summary>
        /// Creates a number of dummy locations
        /// Always rounds these to the next multiple of 4.
        /// </summary>
        /// <param name="numberOfLocations">Number of locations to generate (rounded to the next multiple of 4)</param>
        public void CreateDummyLocations(int numberOfLocations)
        {
            if (numberOfLocations % 4 != 0)
            {
                numberOfLocations = numberOfLocations + (4 - numberOfLocations % 4); 
            }
            for(int i = 1; i<numberOfLocations+1; i++)
            {
                DummyLocation temp = new DummyLocation(i);
                unvisitedLocation.Add(i,temp);
            }
        }

        /// <summary>
        /// Connects all unvisited locations into randomly connected groups of 4 
        /// </summary>
        public void ConnectUnvisitedIntoGroupsOfFour()
        {
            for(int i = 1; i<unvisitedLocation.Count+1; i = i+4)
            {
                List<DummyLocation> tempDLs = new List<DummyLocation>();
                for (int j = 0; j<4; j++)
                {
                    DummyLocation tempDL;
                    if(unvisitedLocation.TryGetValue(i+j, out tempDL))
                    {
                        tempDLs.Add(tempDL);
                    }
                }

                ConnectDummyLocations(tempDLs);
                int removalCase, rand1, rand2, rand3;
                removalCase = rnd.Next(3);
                rand1 = rnd.Next(1, 101);
                rand2 = rnd.Next(1, 101);
                rand3 = rnd.Next(1, 101);
                RemoveConnections(tempDLs[0], tempDLs[1], tempDLs[2], tempDLs[3], removalCase, rand1, rand2, rand3);
            }
        }

        /// <summary>
        /// Generates connections between unvisited locations
        /// </summary>
        public void GenerateConnections()
        {
            List<DummyLocation> dls = new List<DummyLocation>(unvisitedLocation.Values);

            int groupSize = 16;
            int powerOfFour = Convert.ToInt32(Math.Ceiling(Math.Log(unvisitedLocation.Count) / Math.Log(4)));

            for (int i = 1; i <= powerOfFour; i++)
            {
                groupSize = Convert.ToInt32(Math.Pow(4, i));
                int numOfReps = unvisitedLocation.Count / groupSize;

                for (int j = 0; j < numOfReps; j++)
                {
                    int start = j * groupSize;
                    int count = groupSize;
                    if (start + groupSize > unvisitedLocation.Count)
                    {
                        count = unvisitedLocation.Count - start;
                    }
                    List<DummyLocation> group = dls.GetRange(start, count);
                    ConnectByGroupsOfFour(group);
                }
            }
        }

        /// <summary>
        /// Connects the locations passed when split into 4 groups
        /// </summary>
        /// <param name="dummyLocations">Locations to connect</param>
        /// <returns>If it was succesful</returns>
        public bool ConnectByGroupsOfFour(List<DummyLocation> dummyLocations)
        {
            int numberOfDL = dummyLocations.Count;
            int sizeOfGroup = numberOfDL / 4;
            List<DummyLocation> selectedDLs = new List<DummyLocation>();

            if (numberOfDL % 4 != 0)
            {
                //Then there is a problem
                return false;
            }
            else
            {
                for (int i = 0; i< 4; i++)
                {
                    var group = dummyLocations.GetRange(i * sizeOfGroup, sizeOfGroup);
                    for (int j = 1; j < 4; j++)
                    {
                        var temp = GroupByConnectionNumber(j, group);
                        if (temp.Count > 0)
                        {
                            int index = rnd.Next(temp.Count);
                            selectedDLs.Add(temp[index]);
                            break;
                        }
                    }
                }
            }
            var connected = ConnectDummyLocations(selectedDLs);
            int removalCase, rand1, rand2, rand3;
            removalCase = rnd.Next(3);
            rand1 = rnd.Next(1, 101);
            rand2 = rnd.Next(1, 101);
            rand3 = rnd.Next(1, 101);
            RemoveConnections(connected[0], connected[1], connected[2], connected[3], removalCase, rand1, rand2, rand3);
            return true;
        }

        /// <summary>
        /// Groups by number of connections
        /// </summary>
        /// <param name="numberToGroup">Number of connections</param>
        /// <param name="dummyLocations">Location to get group from</param>
        /// <returns>Locations with that number of connections</returns>
        public List<DummyLocation> GroupByConnectionNumber(int numberToGroup, List<DummyLocation> dummyLocations)
        {
            var list = new List<DummyLocation>();
            foreach(DummyLocation dl in dummyLocations)
            {
                if (dl.NumberOfConnections() == numberToGroup)
                {
                    list.Add(dl);
                }
            }
            return list;
        }

        /// <summary>
        /// Connects dummy locations in a list to each otehr dummmy location in the list
        /// </summary>
        /// <param name="dummyLocations">List of dummyLocations to connect togehter</param>
        /// <returns>SortedList of dummy locations</returns>
        public List<DummyLocation> ConnectDummyLocations(List<DummyLocation> dummyLocations)
        {
           var connected = new List<DummyLocation>();

            foreach(DummyLocation dl in dummyLocations)
            {
                for (int i = 0; i < dummyLocations.Count; i++ )
                {
                    DummyLocation temp = dummyLocations[i];
                    if (!temp.Equals(dl))
                    {
                        dl.AddConnection(temp.GetLocationID());
                    }
                }
                connected.Add(dl);
            }
            return connected;
        }

        /// <summary>
        /// Removes connections between the 4 dummy locations given based on the values passed
        /// </summary>
        /// <param name="a">Dummy location a</param>
        /// <param name="b">Dummy location b</param>
        /// <param name="c">Dummy location c</param>
        /// <param name="d">Dummy location d</param>
        /// <param name="removalCase">The case for removal, 0 - Both diagonals (a,d and b,c)  , 1 - One diagonal (a,d), 2 - One diagonal (b,c) 
        /// In case  0 an exterior connection may be removed, in cases 1 and 2, 2,1 or 0 exterior connections may be removed</param>
        /// <param name="rand1">Random number between 1-100</param>
        /// <param name="rand2">Random number between 1-100</param>
        /// <param name="rand3">Random number between 1-100</param>
        public void RemoveConnections(DummyLocation a, DummyLocation b, DummyLocation c, DummyLocation d, int removalCase, int rand1, int rand2, int rand3)
        {
            switch (removalCase)
            {
                case 0: 
                    RemoveTwoDiagonals(a, b, c, d, rand1, rand2);
                    break;
                case 1:
                    RemoveOneDiagonal(a, b, c, d, rand1, rand2, rand3);
                    break;
                case 2:
                    RemoveOneDiagonal(c, a, d, b, rand1, rand2, rand3);
                    break;
            }
        }

        /// <summary>
        /// Removes two diagonals (a,d and b,c) may remove an exterior
        /// </summary>
        /// <param name="a">Dummy location a</param>
        /// <param name="b">Dummy location b</param>
        /// <param name="c">Dummy location c</param>
        /// <param name="d">Dummy location d</param>
        /// <param name="rand1">Random number between 1-100</param>
        /// <param name="rand2">Random number between 1-100</param>
        public void RemoveTwoDiagonals(DummyLocation a, DummyLocation b, DummyLocation c, DummyLocation d, int rand1, int rand2)
        {
            // Remove 2 diagonals
            RemoveConnection(a, d);
            RemoveConnection(c, b);
            // Remove at most one side
            // 25% to remove a side
            if (rand1 > 75)
            {
                RemoveOneExterior(a, b, c, d, rand2);
            }
            // Else remove nothing
        }

        /// <summary>
        /// Removes one diagonal (a,d)
        /// </summary>
        /// <param name="a">Dummy location a</param>
        /// <param name="b">Dummy location b</param>
        /// <param name="c">Dummy location c</param>
        /// <param name="d">Dummy location d</param>
        /// <param name="rand1">Random number between 1-100</param>
        /// <param name="rand2">Random number between 1-100</param>
        /// <param name="rand3">Random number between 1-100</param>
        public void RemoveOneDiagonal(DummyLocation a, DummyLocation b, DummyLocation c, DummyLocation d, int rand1, int rand2, int rand3)
        {
            // Remove a,d
            RemoveConnection(a, d);
            if (rand1 > 80)
            {
                RemoveTwoExterior(a, b, c, d, rand2, rand3);
            }
            else if (rand1 > 33)
            {
                //Remove 1 exterior connections
                RemoveOneExterior(a, b, c, d, rand2);
            }
            // Remove nothing
        }

        /// <summary>
        /// Removes one exterior
        /// </summary>
        /// <param name="a">Dummy location a</param>
        /// <param name="b">Dummy location b</param>
        /// <param name="c">Dummy location c</param>
        /// <param name="d">Dummy location d</param>
        /// <param name="rand1">Random number between 1-100</param>
        public void RemoveOneExterior(DummyLocation a, DummyLocation b, DummyLocation c, DummyLocation d, int rand1)
        {
            if (rand1 > 75)
            {
                RemoveConnection(a, b);
            }
            else if (rand1 > 50)
            {
                RemoveConnection(a, c);
            }
            else if (rand1 > 25)
            {
                RemoveConnection(b, d);
            }
            else
            {
                RemoveConnection(c, d);
            }
        }

        /// <summary>
        /// Removes two exterior
        /// </summary>
        /// <param name="a">Dummy location a</param>
        /// <param name="b">Dummy location b</param>
        /// <param name="c">Dummy location c</param>
        /// <param name="d">Dummy location d</param>
        /// <param name="rand1">Random number between 1-100</param>
        /// <param name="rand2">Random number between 1-100</param>
        public void RemoveTwoExterior(DummyLocation a, DummyLocation b, DummyLocation c, DummyLocation d, int rand1, int rand2)
        {
            if (rand1 > 50)
            {
                RemoveConnection(a, b);
            }
            else
            {
                RemoveConnection(a, c);
            }

            if (rand2 > 50)
            {
                RemoveConnection(c, d);
            }
            else
            {
                RemoveConnection(b, d);
            }
        }

        /// <summary>
        /// Removes connection between two dummy locations
        /// </summary>
        /// <param name="dl1">First dummy location</param>
        /// <param name="dl2">Second dummy location</param>
        public void RemoveConnection(DummyLocation dl1, DummyLocation dl2)
        {
            dl1.RemoveConnection(dl2);
            dl2.RemoveConnection(dl1);
        }

        public Location GetCurentLocation()
        {
            return currentLocation;
        }
    }
}