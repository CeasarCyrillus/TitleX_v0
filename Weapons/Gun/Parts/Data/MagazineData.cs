namespace Weapons.Gun.Parts.data
{
    public struct MagazineData
    {
        public readonly float mass;
        public readonly int ammoCapacity;

        public MagazineData(float mass, int ammoCapacity)
        {
            this.mass = mass;
            this.ammoCapacity = ammoCapacity;
        }
    }
}