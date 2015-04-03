using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Discovery
{
    public class Discovery
    {
        private int discoveryID;
        private String discoveryText;
        private int minLocationNumber;

        public Discovery(ref String discovery)
        {
            throw new System.Exception("Not implemented");
        }
        public String GetDiscoveryText()
        {
            return this.discoveryText;
        }
        public void SetDiscoveryText(ref String discoveryText)
        {
            throw new System.Exception("Not implemented");
        }
        public int GetDiscoveryID()
        {
            return this.discoveryID;
        }
        public void SetDiscoveryID(ref int discoveryID)
        {
            throw new System.Exception("Not implemented");
        }
        public bool IsDiscoverable(ref int locationsVisited)
        {
            throw new System.Exception("Not implemented");
        }

        private DiscoveryCatalogue discoveryCatalogue;

    }
}
