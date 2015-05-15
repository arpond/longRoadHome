using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.Model.Events 
{
	public abstract class EventEffect : uk.ac.dundee.arpond.longRoadHome.Model.Effect  {
		protected int minimum;
		protected int maximum;
        protected string result;

        public const String TAG = "EventEffect";

        public abstract void ResolveEffect(float eventModifier, PCModel pcm);
        public abstract string ParseToString();

        public int GetMinimum()
        {
            return minimum;
        }

        public int GetMaximum()
        {
            return maximum;
        }
        
        public string GetResult()
        {
            return result;
        }
	}

}
