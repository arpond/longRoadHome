using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.ac.dundee.arpond.longRoadHome.Controller;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;

namespace uk.ac.dundee.arpond.longRoadHome.View
{
    public class DebugView : IGameView
    {
        private MainController controller;
        private int currentDisplay;

        private void BtnStartGame(object object_, object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnChangeSetting(object object_, object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnSelectLocation(object object_, object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnMoveToLocation(object object_, object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnViewSublocations(object object_, object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnSelectSublocation(object object_, object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnMoveToSublocation(object object_, object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnScavengeSublocation(object object_, object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnViewInventory(object object_, object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnUseItem(object object_, object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnDropItem(object object_, object eventArgs)
        {
            throw new System.Exception("Not implemented");
        }
        private void BtnSelectOption(object object_, object eventArgs)
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
        public void Animate(List<String> imageFileNames)
        {
            throw new System.Exception("Not implemented");
        }
        public void PlayAudio(String audioFile)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawEvent(String eventText, List<String> options)
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
        public void DrawInventory(ArrayList inventory)
        {
            throw new System.Exception("Not implemented");
        }
        public bool DrawYesNoOption(String text)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawDialogueBox(String text)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawSublocationMap(List<Sublocation> subloc)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawWorldMap(List<Location> loc)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawDiscoveries(List<Discovery> discs)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawMainMenu()
        {
            throw new System.Exception("Not implemented");
        }
    }
}
