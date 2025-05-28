using UnityEngine;

namespace Events
{
    public struct LightningEvent : EventBus.IEvent
    {
        public Vector3 Location { get; private set; }

        public LightningEvent(Vector3 location)
        {
            Location = location;
        }
    }
}