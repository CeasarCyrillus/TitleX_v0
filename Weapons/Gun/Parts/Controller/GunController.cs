using System.Threading.Tasks;
using Events;
using JetBrains.Annotations;
using Lib;
using Lib.ObjectPool;
using UnityEngine;
using Weapons.Bullet;
using Weapons.Bullet.Controller;
using Weapons.Gun.Parts.data;

namespace Weapons.Gun.Parts.Controller
{
    public class GunController: MonoBehaviour
    {
        private GunParts parts;
        [CanBeNull] private Magazine magazine;
        
        [SerializeField] private GameObject muzzle;
        [SerializeField] private PoolableObject bulletPrefab;
        private ObjectPool bulletObjectPool;

        public void Shoot()
        {
            if (parts == null)
                return;
            
            var canShoot = parts.trigger.Press() && parts.barrel.IsEmpty();
            if (!canShoot)
            {
                return;
            }

            var maybeAmmoData = magazine?.GetBullet();
            if (!maybeAmmoData.HasValue)
            {
                return;
            }
            
            var ammoData = maybeAmmoData.Value;
            var muzzleSpeed = parts.barrel.Shoot(ammoData);
            var muzzleVelocity = muzzleSpeed * muzzle.transform.forward;
            
            var gunShotData = new GunShotData(muzzleVelocity, muzzle.transform.position, ammoData);
            
            var bullet = bulletObjectPool.GetObject();
            bullet.GetComponent<BulletController>().Shoot(gunShotData);
            
            /* recoil
             * var bulletMass = UnitConverter.GrainsToKilograms(ammoData.bulletGrains);
               var force = -((muzzleVelocity * bulletMass) / Time.fixedDeltaTime);
             */
        }

        public void Reload(Magazine newMagazine)
        {
            magazine = newMagazine;
        }

        private void Awake()
        {
            bulletObjectPool = ObjectPool.CreateInstance(bulletPrefab, 10);
        }

        public void SetParts(GunParts newParts)
        {
            parts = newParts;
            magazine = null;
        }
    }
}