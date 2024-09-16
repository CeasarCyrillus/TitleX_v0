namespace Events
{
    public readonly struct InputToggleInventoryEvent
    {
        public readonly bool isOpen;

        public InputToggleInventoryEvent(bool isOpen)
        {
            this.isOpen = isOpen;
        }
    }
}