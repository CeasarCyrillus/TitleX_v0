using Armour;
using Lib;
using UnityEngine;
using Weapons.Gun.Parts.data;
using Weapons.Gun.Parts.Data;

public class HealthController: MonoBehaviour
{
    [SerializeField] public float healthPoints;
    [SerializeField] private ArmourData armourData;
    [SerializeField] protected Rigidbody rb;
    [SerializeField]

    private float HitArmour(float penetrationValue)
    {
        var armourValue = ArmourUtil.Instance.GetArmourValue(armourData);
        var penetrationRate = penetrationValue / (penetrationValue + armourValue);
        return penetrationRate;
    }

    private void AddBulletImpactForce(Vector3 impactPoint, GunShotData gunShotData)
    {
        if (!rb) return;
        
        var totalForce =
            (UnitConverter.GrainsToKilograms(gunShotData.ammoData.bulletGrains) *
             gunShotData.velocity) / Time.fixedDeltaTime;
        rb.AddForceAtPosition(totalForce, impactPoint);
    }

    protected virtual void OnDeath(Vector3 impactPoint, GunShotData gunShotData)
    {
        AddBulletImpactForce(impactPoint, gunShotData);
    }

    public void OnGunshotHit(Vector3 impactPoint, GunShotData gunShotData, out float penetrationRate)
    {
        var bulletMass = UnitConverter.GrainsToKilograms(gunShotData.ammoData.bulletGrains);
        
        var penetrationValue = gunShotData.velocity.magnitude * bulletMass;
        penetrationRate = HitArmour(penetrationValue);
        
        if (penetrationRate >= 0.5)
        {
            TakeSharpDamage(penetrationRate * gunShotData.velocity.magnitude, gunShotData.ammoData);
        }
        else
        {
            TakeBluntDamage(gunShotData.velocity.magnitude, gunShotData.ammoData);
        }
        
        if (!IsAlive())
        {
            OnDeath(impactPoint, gunShotData);
            penetrationRate = 1f;
            AddBulletImpactForce(impactPoint, gunShotData);
            return;
        }
        
        AddBulletImpactForce(impactPoint, gunShotData);
    }

    private void TakeSharpDamage(float bulletSpeed, AmmoData ammoData)
    {
        var bulletSizeDamage = UnitConverter.MillimeterToMeter(ammoData.bulletDiameterMM);
        var damage = bulletSpeed * bulletSizeDamage;
        healthPoints = Mathf.Max(0, healthPoints - damage);
    }

    private void TakeBluntDamage(float consumedBulletSpeed, AmmoData ammoData)
    {
        var bulletMassDamage = UnitConverter.GrainsToKilograms(ammoData.bulletGrains);
        var damage = (consumedBulletSpeed * bulletMassDamage) / 5f;
        healthPoints = Mathf.Max(0, healthPoints - damage);
    }

    public bool IsAlive() => healthPoints > 0f;
}