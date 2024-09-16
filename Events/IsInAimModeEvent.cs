namespace Events
{
    public readonly struct IsInAimModeEvent
    {
        public readonly bool isInAimMode;

        public IsInAimModeEvent(bool isInAimMode)
        {
            this.isInAimMode = isInAimMode;
        }
    }
}