using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.Model
{
    public interface Effect
    {
        void ResolveEffect(double eventModifier, PCModel pcm);
        string ParseToString();
    }

}
