using Weapons.Gun.Parts;

namespace Events
{
    public readonly struct ChangeAmmoEvent
    {
        public readonly Magazine magazine;

        public ChangeAmmoEvent(Magazine magazine)
        {
            this.magazine = magazine;
        }
    }
}