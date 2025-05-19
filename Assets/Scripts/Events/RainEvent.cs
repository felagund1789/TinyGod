using UnityEngine;

namespace Events
{
    public struct RainEvent : EventBus.IEvent
    {
        public Vector3 Location { get; private set; }
        public RainType Type { get; private set; }
        
        public RainEvent(Vector3 location, RainType type)
        {
            Location = location;
            Type = type;
        }
    }
}