namespace Events
{
    public struct InputNumericKeysEvent
    {
        public readonly int number;

        public InputNumericKeysEvent(int number)
        {
            this.number = number;
        }
    }
}