using System;
using Lib;
using Weapons.Gun.Parts.Data;

namespace Weapons
{
    public static class GunBuilder
    {
        public static int CalculateMuzzleSpeed(float barrelLengthCm, AmmoData ammo)
        {
            var efficiencyFactor = ammo.energyEfficiency;
            var powderMass = UnitConverter.GrainsToKilograms(ammo.powderGrains);
            var barrelLength = UnitConverter.CentimeterToMeters(barrelLengthCm);
            var bulletMass = UnitConverter.GrainsToKilograms(ammo.bulletGrains);

            // Calculate total energy released by the powder
            var totalEnergy = powderMass * ammo.energyDensity;

            // Calculate effective energy considering efficiency
            var effectiveEnergy = efficiencyFactor * totalEnergy;

            // Calculate force on the bullet
            var netForce = effectiveEnergy / barrelLength;
            var friction = 0.35 * netForce * barrelLength;
            var force = netForce - friction;
    
            // Calculate acceleration
            var acceleration = force / bulletMass;

            // Calculate muzzle velocity
            var velocitySquared = 2 * acceleration * barrelLength;
            var muzzleVelocity = Math.Sqrt(velocitySquared);

            return Convert.ToInt32(muzzleVelocity);
        }
    }
}