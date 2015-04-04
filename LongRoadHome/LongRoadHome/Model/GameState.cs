using System;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
namespace uk.ac.dundee.arpond.longRoadHome.Model
{
    public class GameState
    {
        private PCModel pcModel;
        private EventModel eventModel;
        private LocationModel locationModel;
        private DiscoveryModel discoveryModel;

        public GameState(ref String data, ref String catalogues)
        {
            throw new System.Exception("Not implemented");
        }
        public PCModel GetPCM()
        {
            throw new System.Exception("Not implemented");
        }
        public EventModel GetEM()
        {
            throw new System.Exception("Not implemented");
        }
        public LocationModel GetLM()
        {
            throw new System.Exception("Not implemented");
        }
        public DiscoveryModel GetDM()
        {
            throw new System.Exception("Not implemented");
        }
        public void ParseToString()
        {
            throw new System.Exception("Not implemented");
        }

    }

}
