using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events {
	public class EventCatalogue {
		private List<Event> events;

		public void ParseFromString(ref String eventCatalogue) {
			throw new System.Exception("Not implemented");
		}
		public Event GetRandomEvent() {
			throw new System.Exception("Not implemented");
		}
		public Event GetRandomEvent(ref String type) {
			throw new System.Exception("Not implemented");
		}
		public void GetEvent(ref int eventID) {
			throw new System.Exception("Not implemented");
		}

		//private Event event;

		private EventModel eventModel;

	}

}
