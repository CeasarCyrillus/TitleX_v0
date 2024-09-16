using System;
using Weapons.Gun.Parts.data;
using Weapons.Gun.Parts.Data;

namespace Weapons.Gun.Parts
{
    public class Magazine
    {
        private int currentAmmoAmount;
        private AmmoData currentAmmo;
        
        private readonly MagazineData magazineData;

        public Magazine(MagazineData magazineData)
        {
            this.magazineData = magazineData;
        }

        public AmmoData? GetBullet()
        {
            if (currentAmmoAmount > 1)
            {
                currentAmmoAmount -= 1;
                return currentAmmo;   
            }

            return null;
        }

        public void Load(AmmoData newAmmo, int amount)
        {
            currentAmmo = newAmmo;
            currentAmmoAmount = Math.Max(amount, magazineData.ammoCapacity);
        }
    }
}