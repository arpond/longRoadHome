using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events {
	public class ItemEventEffect : EventEffect  {
		private Item item;

        public override void ResolveEffect(float eventModifier, PCModel pcm)
        {
			throw new System.Exception("Not implemented");
		}

        public override string ParseToString()
        {
            throw new NotImplementedException();
        }
	}

}
