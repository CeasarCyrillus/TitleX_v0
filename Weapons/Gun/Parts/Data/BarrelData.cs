namespace Weapons.Gun.Parts.data
{
    public struct BarrelData
    {
        public readonly float lengthCm;
        public readonly float mass;

        public BarrelData(float mass, float lengthCm)
        {
            this.lengthCm = lengthCm;
            this.mass = mass;
        }
    }
}