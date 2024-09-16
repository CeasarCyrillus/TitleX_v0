using Weapons.Gun.Parts.data;

namespace Events
{
    public struct GunPickupEvent
    {
        public readonly GunParts gunParts;

        public GunPickupEvent(GunParts gunParts)
        {
            this.gunParts = gunParts;
        }
    }
}