using System;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
namespace uk.ac.dundee.arpond.longRoadHome.Model
{
    public class GameState
    {
        private PCModel pcm;
        private EventModel em;
        private LocationModel lm;
        private DiscoveryModel dm;

        /// <summary>
        /// Constructor for new game state (for a new game)
        /// </summary>
        /// <param name="itemCatalogue">Item Catalogue String</param>
        /// <param name="eventCatalogue">Event Catalogue String</param>
        /// <param name="discoveryCatalogue">Discovery Catalogue String</param>
        public GameState(String itemCatalogue, String eventCatalogue, String discoveryCatalogue)
        {
            pcm = new PCModel(itemCatalogue);
            em = new EventModel(eventCatalogue);
            lm = new LocationModel(1024);
            dm = new DiscoveryModel(discoveryCatalogue);
        }

        /// <summary>
        /// Constructor for game state from save data
        /// </summary>
        /// <param name="pc">Player character string</param>
        /// <param name="inventory">Inventory string</param>
        /// <param name="itemCatalogue">Item catalogue string</param>
        /// <param name="usedEvents">used events string</param>
        /// <param name="currentEvent">current events string</param>
        /// <param name="eventCatalogue">event catalogue string</param>
        /// <param name="discovered">discovered string</param>
        /// <param name="discoveryCatalogue">discovered catalogue string</param>
        /// <param name="visitedLocs">visisted locations string</param>
        /// <param name="unvisitedLocs">unvisited locations string</param>
        /// <param name="currLoc">current location string</param>
        /// <param name="currSLoc">current sublocation string</param>
        public GameState(String pc, String inventory, String itemCatalogue, String usedEvents, String currentEvent, String eventCatalogue, String discovered, String discoveryCatalogue, String visitedLocs, String unvisitedLocs, String currLoc, String currSLoc)
        {
            pcm = new PCModel(pc, inventory, itemCatalogue);
            em = new EventModel(usedEvents, eventCatalogue, currentEvent);
            dm = new DiscoveryModel(discovered, discoveryCatalogue);
            lm = new LocationModel(visitedLocs, unvisitedLocs, currLoc, currSLoc);
        }

        /// <summary>
        /// Accessor method for PCModel 
        /// </summary>
        /// <returns>The PCModel</returns>
        public PCModel GetPCM()
        {
            return pcm;
        }

        /// <summary>
        /// Accessor method for Event Model
        /// </summary>
        /// <returns>The Event Model</returns>
        public EventModel GetEM()
        {
            return em;
        }

        /// <summary>
        /// Accessor method for location model
        /// </summary>
        /// <returns>The location model</returns>
        public LocationModel GetLM()
        {
            return lm;
        }

        /// <summary>
        /// Accessor method for Discovery Model
        /// </summary>
        /// <returns>The Discovery Model</returns>
        public DiscoveryModel GetDM()
        {
            return dm;
        }

        /// <summary>
        /// Parses the PC to String
        /// </summary>
        /// <returns>The PC as a string</returns>
        public String ParsePCToString()
        {
            return pcm.ParsePCToString();
        }

        /// <summary>
        /// Parses the Inventory to String
        /// </summary>
        /// <returns>The Inventory as a string</returns>
        public String ParseInventoryToString()
        {
            return pcm.ParseInventoryToString();
        }

        /// <summary>
        /// Parses the used events to String
        /// </summary>
        /// <returns>The used events as a string</returns>
        public String ParseUsedEventsToString()
        {
            return em.ParseUsedEventsToString();
        }

        /// <summary>
        /// Parses the Current Event to String
        /// </summary>
        /// <returns>The Current Event as a string</returns>
        public String ParseCurrentEventToString()
        {
            return em.ParseCurrentEventToString();
        }

        /// <summary>
        /// Parses Discovered to String
        /// </summary>
        /// <returns>Discovered as a string</returns>
        public String ParseDiscoveredToString()
        {
            return dm.ParseDiscoveredToString();
        }

        /// <summary>
        /// Parses the visited locations to String
        /// </summary>
        /// <returns>The visited locations as a string</returns>
        public String ParseVisitedToString()
        {
            return lm.ParseVisitedToString();
        }

        /// <summary>
        /// Parses the unvisited locations to String
        /// </summary>
        /// <returns>The unvisited locations as a string</returns>
        public String ParseUnvisitedToString()
        {
            return lm.ParseUnvisitedToString();
        }

        /// <summary>
        /// Parses the current location to String
        /// </summary>
        /// <returns>The current location as a string</returns>
        public String ParseCurrLocationToString()
        {
            return lm.ParseCurrLocationToString();
        }

        /// <summary>
        /// Parses the current sublocation to String
        /// </summary>
        /// <returns>The current sublocation as a string</returns>
        public String ParseCurrSublocationToString()
        {
            return lm.ParseCurrSubLocToString();
        }

        /// <summary>
        /// Checks if strings are valid catalogues
        /// </summary>
        /// <param name="itemCatalogue">Item Catalogue String</param>
        /// <param name="eventCatalogue">Event Catalogue String</param>
        /// <param name="discoveryCatalogue">Discovery Catalogue String</param>
        /// <returns>If the catalogues are valid</returns>
        public static bool AreValidCatalogues(String itemCatalogue, String eventCatalogue, String discoveryCatalogue)
        {
            return  PCModel.IsValidItemCatalogue(itemCatalogue) && 
                    EventModel.IsValidCatalogue(eventCatalogue) && 
                    DiscoveryModel.IsValidDiscoveryCatalogue(discoveryCatalogue);
        }

        /// <summary>
        /// Checks if string make up a valid game state
        /// </summary>
        /// <param name="pc">Player character string</param>
        /// <param name="inventory">Inventory string</param>
        /// <param name="itemCatalogue">Item catalogue string</param>
        /// <param name="usedEvents">used events string</param>
        /// <param name="currentEvent">current events string</param>
        /// <param name="eventCatalogue">event catalogue string</param>
        /// <param name="discovered">discovered string</param>
        /// <param name="discoveryCatalogue">discovered catalogue string</param>
        /// <param name="visitedLocs">visisted locations string</param>
        /// <param name="unvisitedLocs">unvisited locations string</param>
        /// <param name="currLoc">current location string</param>
        /// <param name="currSLoc">current sublocation string</param>
        /// <returns>If game state is valid</returns>
        public static bool IsValidGameState(String pc, String inventory, String itemCatalogue, String usedEvents, String currentEvent, String eventCatalogue, String discovered, String discoveryCatalogue, String visitedLocs, String unvisitedLocs, String currLoc, String currSLoc)
        {
            int currID, currSub;
            return  PCModel.IsValidPCModel(pc, inventory, itemCatalogue) && 
                    EventModel.IsValidCatalogue(eventCatalogue) &&
                    EventModel.IsValidCurrentEvent(currentEvent) &&
                    EventModel.IsValidUsedEvents(usedEvents) &&
                    DiscoveryModel.IsValidDiscoveryCatalogue(discoveryCatalogue) &&
                    DiscoveryModel.IsValidDiscovered(discovered) &&
                    LocationModel.IsValidUnvisitedLocations(unvisitedLocs) &&
                    LocationModel.IsValidVisitedLocations(visitedLocs) &&
                    int.TryParse(currLoc, out currID) &&
                    int.TryParse(currSLoc, out currSub);
        }
    }

}
