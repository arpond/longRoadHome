using System;
using System.Collections.Generic;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.View
{
    public interface IGameView
    {
        void DrawMainMenu();
        void DrawDiscoveries(ref List<Discovery> discs);
        void DrawWorldMap(ref List<Location> loc);
        void DrawSublocationMap(ref List<Sublocation> subloc);
        void DrawDialogueBox(ref String text);
        bool DrawYesNoOption(ref String text);
        void DrawInventory(ref Item[] inventory);
        void DrawGameOver();
        void DrawVictory();
        void DrawEvent(ref String eventText, ref List<String> options);
        void PlayAudio(ref String audioFile);
        void Animate(ref List<String> imageFileNames);

    }

}
