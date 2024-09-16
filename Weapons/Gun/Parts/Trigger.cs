using Weapons.Gun.Parts.data;

namespace Weapons.Gun.Parts
{
    public class Trigger
    {
        private readonly TriggerData triggerData;

        public Trigger(TriggerData triggerData)
        {
            this.triggerData = triggerData;
        }

        public bool Press()
        {
            // TODO: shoot based on if it has been pressed 
            return true;
        }
    }
}