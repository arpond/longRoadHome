using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events {
	public abstract class EventEffect : uk.ac.dundee.arpond.longRoadHome.Model.Effect  {
		private int minimum;
		private int maximum;

		public abstract void ResolveEffect(ref object float_eventModifier);

		private Option option;

	}

}
