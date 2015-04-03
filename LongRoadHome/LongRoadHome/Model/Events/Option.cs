using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events {
	public class Option {
		private int optionNumber;
		private String optionText;
		private ArrayList<EventEffect> effects;

		public String GetOptionText() {
			return this.optionText;
		}
		public int GetOptionNumber() {
			return this.optionNumber;
		}
		public ArrayList<EventEffect> GetOptionEffects() {
			throw new System.Exception("Not implemented");
		}

		private EventEffect eventEffect;

		private Event event;

	}

}
