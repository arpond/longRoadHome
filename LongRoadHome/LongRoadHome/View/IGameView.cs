using System;
using System.Collections;
using System.Collections.Generic;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.View
{
    public interface IGameView
    {
        void DrawMainMenu();
        void DrawDiscoveries(List<Discovery> discs);
        void DrawWorldMap(List<Location> loc);
        void DrawSublocationMap(List<Sublocation> subloc);
        void DrawDialogueBox(String text);
        bool DrawYesNoOption(String text);
        void DrawInventory(ArrayList inventory);
        void DrawGameOver();
        void DrawVictory();
        void DrawEvent(String eventText, List<String> options);
        void PlayAudio(String audioFile);
        void Animate(List<String> imageFileNames);

    }

}
