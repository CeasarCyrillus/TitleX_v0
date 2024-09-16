namespace Weapons.Gun.Parts.data
{
    public class GunParts
    {
        public readonly Trigger trigger;
        public readonly Barrel barrel;

        public GunParts(Trigger trigger, Barrel barrel)
        {
            this.trigger = trigger;
            this.barrel = barrel;
        }
    }
}