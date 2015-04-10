using System;
using System.Collections;
using System.Collections.Generic;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    public class LocationModel
    {
        private const int STD_MIN_SIZE = 1, STD_MAX_SIZE = 5, STD_MAX_ITEMS = 5, STD_MAX_AMOUNT = 10;
        private const int MAX_CONNECTIONS = 4, MIN_CONNECTIONS = 2;

        private SortedList<int,Location> visitedLocation;
        private SortedList<int,DummyLocation> unvisitedLocation;

        private Location currentLocation;
        private Sublocation currentSublocation;

        private Random rnd = new Random();

        public LocationModel()
        {
            visitedLocation = new SortedList<int, Location>();
            unvisitedLocation = new SortedList<int, DummyLocation>();

        }

        public LocationModel(String visitedLocs, String unvisitedLocs, String currLoc, String currSLoc)
        {
            visitedLocation = new SortedList<int, Location>();
            unvisitedLocation = new SortedList<int, DummyLocation>();

            String[] visitedElem = visitedLocs.Split('#');
            foreach(String loc in visitedElem)
            {
                Location temp = new Location(loc);
                visitedLocation.Add(temp.GetLocationID(), temp);
            }

            String[] unvisitedElem = unvisitedLocs.Split('#');
            foreach(String loc in unvisitedElem)
            {
                DummyLocation temp = new DummyLocation(loc);
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
                currentLocation.GetSublocationByID(currSub);
            }
        }

        public String ParseVisitedToString()
        {
            String parsed = "VisitedLocations";
            foreach(Location visited in visitedLocation.Values)
            {
                parsed += "#" + visited.ParseToString();
            }
            return parsed;
        }

        public String ParseUnvisitedToString()
        {
            String parsed = "UnvisitedLocations";
            foreach (DummyLocation unvisited in unvisitedLocation.Values)
            {
                parsed += "#" + unvisited.ParseToString();
            }
            return parsed;
        }

        public String ParseCurrLocationToString()
        {
            return "" + currentLocation.GetLocationID();
        }

        public String ParseCurrSubLocToString()
        {
            return "" + currentSublocation.GetSublocationID();
        }

        public Location ReplaceDummyLocation(DummyLocation dummy, int minSize, int maxSize, int maxItems, int maxAmount)
        {
            Location location = Location.ConvertToLocation(dummy);
            location.GenerateSubLocations(minSize, maxSize, maxItems, maxAmount);
            return location;
        }

        public Sublocation GetSubLocation()
        {
            return this.currentSublocation;
        }

        public String GetSublocType()
        {
            return currentSublocation.GetType().ToString();
        }

        public void ChangeSubLocation(int subLocationID)
        {
            currentLocation.SetCurrentSubLocation(subLocationID);
            currentSublocation = currentLocation.GetCurrentSubLocation();
        }

        public bool LocationVisited(int locationID)
        {
            return visitedLocation.ContainsKey(locationID);
        }

        private void ChangeToUnvisitedLocation(int locationID)
        {
            DummyLocation toChangeTo;
            if (unvisitedLocation.TryGetValue(locationID, out toChangeTo))
            {
                Location temp = Location.ConvertToLocation(toChangeTo);

                int minSize = STD_MIN_SIZE;
                int maxSize = STD_MAX_SIZE;
                int maxItems = rnd.Next(1, STD_MAX_ITEMS+1);
                int maxAmount = rnd.Next(1, STD_MAX_AMOUNT+1);

                temp.GenerateSubLocations(minSize, maxSize, maxItems, maxAmount);
                unvisitedLocation.Remove(locationID);
                visitedLocation.Add(locationID, temp);
                currentLocation = temp;
            }
        }

        private void ChangeToVisitedLocation(int locationID)
        {
            Location toChangeTo;
            if (visitedLocation.TryGetValue(locationID, out toChangeTo))
            {
                currentLocation = toChangeTo;
            }
        }

        public List<Item> Scavenge()
        {
            throw new System.Exception("Not implemented");
        }

        public void GenerateDummyLocations(int totalLocations, int groupSize, int numOfGroups)
        {
            List<HashSet<int>[]> masterList = new List<HashSet<int>[]>();
            HashSet<int>[] singleList;

            for (int i=0; i<numOfGroups; i++)
            {
                HashSet<int>[] generated = GenerateAdjacencyLists(groupSize, MIN_CONNECTIONS, MAX_CONNECTIONS);
                HashSet<int>[] folded = foldList(generated);
                masterList.Add(folded);
            }
            singleList = JoinAdjacencyLists(masterList[0], masterList[1]);
            for(int i = 2; i < masterList.Count; i++)
            {
                singleList = JoinAdjacencyLists(singleList, masterList[i]);
            }

            
            for (int i = 1; i < singleList.Length+1; i++)
            {
                DummyLocation dummy = new DummyLocation(i, singleList[i-1]);
                unvisitedLocation.Add(i, dummy);
            }
        }

        private HashSet<int>[] foldList(HashSet<int>[] adjlist)
        {
            float startIndex = adjlist.Length / 4;
            float endIndex = adjlist.Length - adjlist.Length / 4;

            int counter = 0, conPoint1, conPoint2;
            do
            {
                counter++;
                conPoint1 = rnd.Next(0, (int)startIndex);
            } while (adjlist[conPoint1].Count >= MAX_CONNECTIONS && counter <= 100);

            do
            {
                counter++;
                conPoint2 = rnd.Next((int)endIndex, adjlist.Length);
            } while (adjlist[conPoint2].Count >= MAX_CONNECTIONS && counter <= 100);

            adjlist[conPoint1].Add(conPoint2 + 1);
            adjlist[conPoint2].Add(conPoint1 + 1);
            return adjlist;
        }

        private HashSet<int>[] JoinAdjacencyLists(HashSet<int>[] adjlist1, HashSet<int>[] adjlist2)
        {
            for (int i = 0; i < adjlist2.Length; i++ )
            {
                HashSet<int> temp = new HashSet<int>();
                foreach (int id in adjlist2[i])
                {
                    temp.Add(id + adjlist1.Length);
                }
                adjlist2[i] = temp;
            }
            float conList1 = adjlist1.Length / 3;
            float conList2 = adjlist1.Length - adjlist1.Length / 3;
            float conList3 = adjlist2.Length / 3;
            float conList4 = adjlist2.Length - adjlist2.Length / 3;
            int connectionPoint1;
            int connectionPoint2;
            int counter = 0;
            do
            {
                connectionPoint1 = rnd.Next((int)conList1, (int)conList2);
                counter++;
            } while (adjlist1[connectionPoint1].Count >= MAX_CONNECTIONS && counter <= 100);

            counter = 0;
            do
            {
                connectionPoint2 = rnd.Next((int)conList3, (int)conList4);
            } while (adjlist2[connectionPoint2].Count >= MAX_CONNECTIONS && counter <= 100);
            

            adjlist1[connectionPoint1].Add(connectionPoint2 + adjlist1.Length  +1);
            adjlist2[connectionPoint2].Add(connectionPoint1+1);

            HashSet<int>[] fullList = new HashSet<int>[adjlist1.Length + adjlist2.Length];
            Array.Copy(adjlist1, fullList, adjlist1.Length);
            Array.Copy(adjlist2, 0, fullList, adjlist1.Length, adjlist2.Length);

            return fullList;
        }

        private HashSet<int>[] GenerateAdjacencyLists(int maxNodes, int minConnections, int maxConnections)
        {
            HashSet<int>[] lists = new HashSet<int>[maxNodes];
            for (int i = 0; i < maxNodes; i++)
            {
                lists[i] = new HashSet<int>();
            }

            for (int i = 0; i < maxNodes; i++)
            {
                int topRange = i + maxConnections -1;
                int bottomRange = i - maxConnections +1;
                if(bottomRange < 0)
                {
                    bottomRange = 0;
                    topRange += 1;
                }
                
                if (topRange >= maxNodes)
                {
                    topRange = maxNodes-1;
                    bottomRange -= 1;
                }

                int numConnections = lists[i].Count;
                List<int> tempList = new List<int>();
                for (int j = bottomRange; j < topRange; j++ )
                {
                    if(i != j && lists[j].Count < maxConnections && !lists[i].Contains(j+1))
                    {
                        tempList.Add(j);
                    }
                }

                do
                {
                    int currentNode, index;
                    do
                    {
                        index = rnd.Next(tempList.Count);
                        currentNode = tempList[index];
                    } while (currentNode == i || lists[i].Contains(currentNode+1) || lists[currentNode].Count == maxConnections);
                    tempList.RemoveAt(index);

                    int p = (int)((1.0f - numConnections / (float) maxConnections) * 100.0f);
                    if (numConnections < minConnections || numConnections == minConnections && rnd.Next(1, 100) < p)
                    {
                        lists[i].Add(currentNode + 1);
                        lists[currentNode].Add(i + 1);
                        numConnections++;
                    }

                    if (numConnections >= maxConnections)
                    {
                        break;
                    }
                } while (tempList.Count > 0);
            }
            return lists;
        }

        //private HashSet<int> GenerateConnections(HashSet<int> connections, int minConnections, int maxConnections, int nodeNumber, int maxNode)
        //{
        //    int topRange = nodeNumber + maxConnections + 2;
        //    int bottomRange = nodeNumber - maxConnections - 2;

        //    if(bottomRange <= 0)
        //    {
        //        bottomRange = 1;
        //        topRange += 2;
        //    }

        //    if (topRange > maxNode)
        //    {
        //        topRange = maxNode;
        //        bottomRange -= 2;
        //    }

        //    for (int i = bottomRange; i < topRange +1 ; i++)
        //    {
        //        int currentNode = rnd.Next(bottomRange, topRange + 1);
        //        if (connections.Count < minConnections)
        //        {
        //            connections.Add(currentNode);
        //        }
        //        else if (connections.Count == minConnections && rnd.Next(1,100) > )
        //        {

        //        }
        //        else if (connections.Count >= maxConnections)
        //        {
        //            break;
        //        }
                
        //    }

        //    return connections;
        //}

        public bool IsScavenged()
        {
            return currentSublocation.GetScavenged();
        }

        public  SortedList<int, DummyLocation> GetUnvisited()
        {
            return this.unvisitedLocation;
        }
    }
}