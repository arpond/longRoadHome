using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model
{
    public class GameState
    {
        private PCModel pcModel;
        private uk.ac.dundee.arpond.longRoadHome.Model.Events.EventModel eventModel;
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
        public uk.ac.dundee.arpond.longRoadHome.Model.Events.EventModel GetEM()
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

        private PCModel pCModel;

        private uk.ac.dundee.arpond.longRoadHome.Controller.MainController mainController;

    }

}
