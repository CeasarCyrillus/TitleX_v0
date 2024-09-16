using Weapons.Gun.Parts.data;
using Weapons.Gun.Parts.Data;

namespace Weapons.Gun.Parts
{
    public class Barrel
    {
        private readonly BarrelData barrelData;
        private readonly float timeInBarrel = 0.002f;

        public Barrel(BarrelData barrelData)
        {
            this.barrelData = barrelData;
        }

        public bool IsEmpty()
        {
            return true;
        }

        public float Shoot(AmmoData ammoData)
        {
            var muzzleSpeed = GunBuilder.CalculateMuzzleSpeed(barrelData.lengthCm, ammoData);
            return muzzleSpeed;
        }
    }
}