using System;
using System.Collections;
using System.Collections.Generic;
using uk.ac.dundee.arpond.longRoadHome.Controller;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.View
{
    public interface IGameView
    {
        void StartNewGame();
        void DrawMainMenu();
        void DrawDiscoveries(List<Discovery> discs);
        void DrawWorldMap(List<Location> visited, List<DummyLocation> unvisited);
        void DrawSublocationMap(List<Sublocation> subloc);
        void DrawDialogueBox(String text);
        bool DrawYesNoOption(String text);
        void DrawInventory(ArrayList inventory);
        void DrawGameOver();
        void DrawVictory();
        int DrawEvent(String eventText, List<String> options);
        void DrawEventResult(String optionResult, List<String> results);
        void PlayAudio(String audioFile);
        void Animate(List<String> imageFileNames);
        void DrawScavengeResults(List<Item> scavenged);
        void DrawDiscovery(string discovery);
    }

}
