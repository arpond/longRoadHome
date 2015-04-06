using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events {
	public abstract class EventEffect : uk.ac.dundee.arpond.longRoadHome.Model.Effect  {
		private int minimum;
		private int maximum;

        public abstract void ResolveEffect(float eventModifier, uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter.PlayerCharacter pc);

        public string ParseToString()
        {
            throw new System.Exception("Not implemented");
        }

		private Option option;

	}

}
