using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events {
	public class EventModel {
		private Event currentEvent;
		private ArrayList<int> usedEvents;
		private EventCatalogue eventCatalogue;

		public EventModel(ref String usedEvents, ref String catalogue, ref String curEvent) {
			throw new System.Exception("Not implemented");
		}
		public void FetchNewCurrentEvent() {
			throw new System.Exception("Not implemented");
		}
		public void FetchNewCurrentEvent(ref String type) {
			throw new System.Exception("Not implemented");
		}
		public void FetchSpecificEvent(ref int eventID) {
			throw new System.Exception("Not implemented");
		}
		public String GetCurrentEventText() {
			throw new System.Exception("Not implemented");
		}
		public ArrayList<String> GetCurrentEventOptionsText() {
			throw new System.Exception("Not implemented");
		}
		public int GetCurrentEventID() {
			throw new System.Exception("Not implemented");
		}
		public uk.ac.dundee.arpond.longRoadHome.Model.Effect GetOptionEffect(ref int optionNumber) {
			throw new System.Exception("Not implemented");
		}
		public String ParseUsedEventsToString() {
			throw new System.Exception("Not implemented");
		}

		private Event event;

		private uk.ac.dundee.arpond.longRoadHome.Model.GameState gameState;

	}

}
