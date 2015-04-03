using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model
{
    public class ModelFacade
    {
        public void TriggerNewEvent(ref GameState gs, ref float eventModifier)
        {
            throw new System.Exception("Not implemented");
        }
        public void TriggerEvent(ref GameState gs, ref int eventID)
        {
            throw new System.Exception("Not implemented");
        }
        public void ResolveEvent(ref GameState gs, ref int optionSelected)
        {
            throw new System.Exception("Not implemented");
        }
        public bool CanAffordMove(ref GameState gs)
        {
            throw new System.Exception("Not implemented");
        }
        public void ChangeLocation(ref GameState gs, ref int locationID)
        {
            throw new System.Exception("Not implemented");
        }
        public void ChangeSubLocation(ref GameState gs, ref int subLocationID)
        {
            throw new System.Exception("Not implemented");
        }
        public bool ItemUsable(ref GameState gs, ref int invSlot)
        {
            throw new System.Exception("Not implemented");
        }
        public void UseItem(ref GameState gs, ref int invSlot)
        {
            throw new System.Exception("Not implemented");
        }
        public void DiscardItem(ref GameState gs, ref int invSlot)
        {
            throw new System.Exception("Not implemented");
        }
        public void ScavangeSubLocation(ref GameState gs)
        {
            throw new System.Exception("Not implemented");
        }
        public bool IsScavenged(ref GameState gs)
        {
            throw new System.Exception("Not implemented");
        }

    }

}
