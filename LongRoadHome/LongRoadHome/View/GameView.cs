using System;
using System.Collections.Generic;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.View
{
    public class GameView : IGameView
    {
        private uk.ac.dundee.arpond.longRoadHome.Controller.MainController controller;
        private int currentDisplay;

        private void BtnStartGame(ref object object_, ref object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnChangeSetting(ref object object_, ref object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnSelectLocation(ref object object_, ref object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnMoveToLocation(ref object object_, ref object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnViewSublocations(ref object object_, ref object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnSelectSublocation(ref object object_, ref object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnMoveToSublocation(ref object object_, ref object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnScavengeSublocation(ref object object_, ref object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnViewInventory(ref object object_, ref object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnUseItem(ref object object_, ref object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnDropItem(ref object object_, ref object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnSelectOption(ref object object_, ref object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void ChangeLocationAnimation()
        {
            throw new System.Exception("Not implemented");
        }
        private void GuiReady()
        {
            throw new System.Exception("Not implemented");
        }
        public void Animate(ref List<String> imageFileNames)
        {
            throw new System.Exception("Not implemented");
        }
        public void PlayAudio(ref String audioFile)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawEvent(ref String eventText, ref List<String> options)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawVictory()
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawGameOver()
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawInventory(ref Item[] inventory)
        {
            throw new System.Exception("Not implemented");
        }
        public bool DrawYesNoOption(ref String text)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawDialogueBox(ref String text)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawSublocationMap(ref List<Sublocation> subloc)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawWorldMap(ref List<Location> loc)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawDiscoveries(ref List<Discovery> discs)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawMainMenu()
        {
            throw new System.Exception("Not implemented");
        }

        private uk.ac.dundee.arpond.longRoadHome.Controller.MainController mainController;

    }

}
