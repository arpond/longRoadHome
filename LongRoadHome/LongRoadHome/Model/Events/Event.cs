using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events {
	public class Event {
		private String eventText;
		private int eventID;
		private String eventType;
		private List<Option> options;

		public Event(ref String event_) {
			throw new System.Exception("Not implemented");
		}
		public List<String> GetEventOptionsText() {
			throw new System.Exception("Not implemented");
		}
		public String GetEventText() {
			return this.eventText;
		}
		public String GetEventType() {
			return this.eventType;
		}
		public String ParseToString() {
			throw new System.Exception("Not implemented");
		}

		private Option option;

		private EventCatalogue eventCatalogue;
		private EventModel eventModel;

	}

}
