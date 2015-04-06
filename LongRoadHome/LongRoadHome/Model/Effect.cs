using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model
{
    public interface Effect
    {
        void ResolveEffect(float eventModifier, uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter.PlayerCharacter pc);
        string ParseToString();
    }

}
