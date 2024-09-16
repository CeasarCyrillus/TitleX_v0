using System;
using UnityEngine;
using Weapons.Gun.Parts.Data;

[Serializable]
public readonly struct BulletHitInfo
{ 
    public readonly Vector3 resultingVelocity;
    public readonly float penetrationRate;
    public readonly bool isFullPenetration;
    public readonly float bulletSpeedConsumed;
    public readonly AmmoData ammo;
    public readonly Vector3 bulletImpactVelocity;
    public readonly Vector3 impactPoint;

    public BulletHitInfo(AmmoData ammo, Vector3 resultingVelocity, float bulletSpeedConsumed, float penetrationRate, bool isFullPenetration, Vector3 bulletImpactVelocity, Vector3 impactPoint)
    {
        this.resultingVelocity = resultingVelocity;
        this.penetrationRate = penetrationRate;
        this.isFullPenetration = isFullPenetration;
        this.bulletImpactVelocity = bulletImpactVelocity;
        this.impactPoint = impactPoint;
        this.bulletSpeedConsumed = bulletSpeedConsumed;
        this.ammo = ammo;
    }
}