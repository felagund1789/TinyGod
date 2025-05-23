namespace Events
{
    public struct FaithUsedEvent : EventBus.IEvent
    {
        public int Amount { get; private set; }

        public FaithUsedEvent(int amount)
        {
            Amount = amount;
        }
    }
}