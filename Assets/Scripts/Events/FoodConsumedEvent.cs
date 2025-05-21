namespace Events
{
    public struct FoodConsumedEvent : EventBus.IEvent
    {
        public int Amount { get; private set; }

        public FoodConsumedEvent(int amount)
        {
            Amount = amount;
        }
    }
}