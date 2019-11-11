namespace InteractiveMusicScales.Tests
{
    internal class ActionEventCatcher
    {
        public bool EventWasTriggered { get; private set; } = false;

        public void CatchEvent()
        {
            EventWasTriggered = true;
        }
    }
}