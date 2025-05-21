namespace Events
{
    public struct FaithGeneratedEvent : EventBus.IEvent
    {
        public int Amount { get; private set; }

        public FaithGeneratedEvent(int amount)
        {
            Amount = amount;
        }
    }
}