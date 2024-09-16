
namespace Weapons.Gun.Parts.Data
{
    public readonly struct AmmoData
    {
        public readonly int bulletGrains;
        public readonly int powderGrains;
        public readonly float bulletDiameterMM;
        public readonly float casingDiameterMM;
        public readonly int energyDensity;
        public readonly float energyEfficiency;

        public AmmoData(int bulletGrains, int powderGrains, float bulletDiameterMm, float casingDiameterMm)
        {
            this.bulletGrains = bulletGrains;
            this.powderGrains = powderGrains;
            this.bulletDiameterMM = bulletDiameterMm;
            this.casingDiameterMM = casingDiameterMm;
            energyDensity = 2_000_000;
            float energyEfficiencyOffset = powderGrains / 100f;
            energyEfficiency = 1 - energyEfficiencyOffset;
        }
    }
}