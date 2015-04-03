using System;
namespace uk.ac.dundee.arpond.longRoadHome.View
{
    public interface IGameView
    {
        void DrawMainMenu();
        void DrawDiscoveries(ref ArrayList<Discovery> discs);
        void DrawWorldMap(ref ArrayList<Location> loc);
        void DrawSublocationMap(ref ArrayList<Sublocation> subloc);
        void DrawDialogueBox(ref String text);
        Bool DrawYesNoOption(ref String text);
        void DrawInventory(ref item[] inventory);
        void DrawGameOver();
        void DrawVictory();
        void DrawEvent(ref String eventText, ref ArrayList<String> options);
        void PlayAudio(ref String audioFile);
        void Animate(ref ArrayList<String> imageFileNames);

    }

}
