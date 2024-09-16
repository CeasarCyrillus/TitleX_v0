using System;
using System.Threading.Tasks;
using Events;
using Lib;
using Lib.ObjectPool;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons.Gun.Parts.data;
using Weapons.Gun.Parts.Data;
using Random = UnityEngine.Random;

namespace Managers
{
    public class BulletHitManager: MonoBehaviour
    {
        [SerializeField] private PoolableObject ricochetEffect;
        [SerializeField] private PoolableObject bloodEffect;
        private ObjectPool ricochetEffectPool;
        private ObjectPool bloodEffectPool;
        public static BulletHitManager Instance { private set; get; }
        private void Awake()
        {
            Instance = this;
            ricochetEffectPool = ObjectPool.CreateInstance(ricochetEffect, 10);
            bloodEffectPool = ObjectPool.CreateInstance(bloodEffect, 10);
        }
        
        public GunShotData OnGunShotHit(GunShotData gunShotData, RaycastHit hit)
        {
            if (hit.collider.CompareTag("NoBulletHit"))
            {
                return gunShotData;
            }
            
            var ricochetVelocity = BulletHitUtil.GetRicochetVelocity(gunShotData.velocity, hit.normal);
            if (hit.collider.gameObject.TryGetComponent<HealthController>(out var healthController))
            {
                if (gunShotData.hits.Contains(healthController) || !healthController.IsAlive())
                    return gunShotData;
                gunShotData.hits.Add(healthController);
                
                
                healthController.OnGunshotHit(hit.point, gunShotData, out var penetrationRate);
                
                if (penetrationRate >= 0.5)
                {
                    PlayHitEffects(bloodEffectPool, gunShotData.velocity.normalized, hit);
                    gunShotData.velocity *= 0.6f;
                }
                else
                {
                    PlayHitEffects(ricochetEffectPool, gunShotData.velocity.normalized, hit);
                    gunShotData.velocity = ricochetVelocity;
                }
            }
            else
            {
                PlayHitEffects(ricochetEffectPool, gunShotData.velocity.normalized, hit);
                gunShotData.velocity = ricochetVelocity;
            }

            return gunShotData;
        }

        private void PlayHitEffects(ObjectPool effectPool, Vector3 gunShotDirection, RaycastHit hit)
        {
            var particles = (HitEffect)effectPool.GetObject();
            particles.Init(hit.transform, hit.point);
            particles.transform.forward = -gunShotDirection;
            //particles.transform.position = hit.point;
        }
    }
}