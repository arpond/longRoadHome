using System;
using System.Threading;
namespace uk.ac.dundee.arpond.longRoadHome.Controller
{
    public class MainController
    {
        private DifficultyController dc;
        private uk.ac.dundee.arpond.longRoadHome.Model.GameState gs;
        private uk.ac.dundee.arpond.longRoadHome.View.IGameView gameView;
        private AutoResetEvent difficultyEvent;
        private AutoResetEvent guiEvent;
        private AutoResetEvent modelEvent;

        public void InitialiseGame()
        {
            throw new System.Exception("Not implemented");
        }
        public void CreateNewSaveFile()
        {
            throw new System.Exception("Not implemented");
        }
        public void WriteSaveData()
        {
            throw new System.Exception("Not implemented");
        }
        public void ChangeLocation(ref int locationID)
        {
            throw new System.Exception("Not implemented");
        }
        public void ChangeSubLocation(ref int sublocationID)
        {
            throw new System.Exception("Not implemented");
        }
        private void TriggerEvent()
        {
            throw new System.Exception("Not implemented");
        }
        public void ResolveEvent(ref object int_optionSelected)
        {
            throw new System.Exception("Not implemented");
        }
        public void ScavangeLocation()
        {
            throw new System.Exception("Not implemented");
        }
        public void OpenInventory()
        {
            throw new System.Exception("Not implemented");
        }
        public void UseItem(ref int inventorySlot)
        {
            throw new System.Exception("Not implemented");
        }
        public void DiscardItem(ref int inventorySlot)
        {
            throw new System.Exception("Not implemented");
        }
        public void DisplayDiscoveries()
        {
            throw new System.Exception("Not implemented");
        }
        public void DisplaySubLocations()
        {
            throw new System.Exception("Not implemented");
        }
        public void DisplayLocations()
        {
            throw new System.Exception("Not implemented");
        }
        public void DisplayEndGameScreen()
        {
            throw new System.Exception("Not implemented");
        }
        public void ExitGame()
        {
            throw new System.Exception("Not implemented");
        }

        private DifficultyController difficultyController;
        private uk.ac.dundee.arpond.longRoadHome.View.IGameView iGameView;
        private uk.ac.dundee.arpond.longRoadHome.Model.GameState gameState;

    }

}
