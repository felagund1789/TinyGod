namespace Events
{
    public struct FoodProducedEvent : EventBus.IEvent
    {
        public int Amount { get; private set; }

        public FoodProducedEvent(int amount)
        {
            Amount = amount;
        }
    }
}