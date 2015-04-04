using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Controller
{
    public class DifficultyController
    {
        private float endLocationChance;
        private int endLocationMinimum;
        private float eventModifier;
        private float eventChance;
        private List<float> playerStatusTracker;
        private float playerStatus;

        private void CalcEndLocationChance()
        {
            throw new System.Exception("Not implemented");
        }
        private void CalcEventModifier()
        {
            throw new System.Exception("Not implemented");
        }
        private void CalcPlayerStatus()
        {
            throw new System.Exception("Not implemented");
        }
        public void UpdatePlayerStatus(ref uk.ac.dundee.arpond.longRoadHome.Model.GameState gs)
        {
            throw new System.Exception("Not implemented");
        }
        public void UpdateStatusTracker(ref uk.ac.dundee.arpond.longRoadHome.Model.GameState gs)
        {
            throw new System.Exception("Not implemented");
        }
        public float GetEndLocationChance()
        {
            return this.endLocationChance;
        }
        public float GetEventModifier()
        {
            return this.eventModifier;
        }
        public List<float> GetPlayerStatusTracker()
        {
            throw new System.Exception("Not implemented");
        }
        public float GetPlayerStatus()
        {
            return this.playerStatus;
        }
        public float GetEventChance()
        {
            return this.eventChance;
        }
        public String ParseToString()
        {
            throw new System.Exception("Not implemented");
        }

        private MainController mainController;

    }

}
