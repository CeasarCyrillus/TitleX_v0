using System;
using Lib;
using Managers;
using UnityEngine;
using Weapons.Gun.Parts.data;

namespace Weapons.Bullet.Controller
{
    public class BulletController: MonoBehaviour
    {
        private TrailRenderer trailRenderer;
        [SerializeField] private float minSpeed;
        [SerializeField] private float visualVelocityModifier = 5f;
        private readonly float airDensity = 1.225f;
        private readonly float dragCoefficient = 0.3f;
        
        private long destroyAt;
        private GunShotData gunShotData;

        private void Awake()
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }

        private Vector3 GetVisualVelocity()
        {
            return gunShotData.velocity / visualVelocityModifier;
        }

        private Vector3 CalculateDragVelocity()
        {
            var crossSectionalArea =
                Mathf.Pow(UnitConverter.MillimeterToMeter(gunShotData.ammoData.bulletDiameterMM) / 2, 2) * Mathf.PI;
            float dragForceMagnitude = 0.5f * airDensity * dragCoefficient * crossSectionalArea * gunShotData.velocity.magnitude * gunShotData.velocity.magnitude;
            
            Vector3 dragForce = -dragForceMagnitude * gunShotData.velocity.normalized;
            
            Vector3 dragAcceleration = dragForce / UnitConverter.GrainsToKilograms(gunShotData.ammoData.bulletGrains);
            return dragAcceleration * Time.deltaTime;
        }
        private void Update()
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            if (now > destroyAt || gunShotData.velocity.magnitude <= minSpeed)
            {
                gameObject.SetActive(false);
                return;
            }

            gunShotData.velocity.y += CalculateGravityVelocity();
            gunShotData.velocity += CalculateDragVelocity();
            
            var travelDistance = GetVisualVelocity() * Time.deltaTime;
            var newPosition = gunShotData.currentPosition + travelDistance;

            var ray = new Ray(gunShotData.currentPosition, gunShotData.velocity.normalized);
            if (Physics.Raycast(ray, out var hit, travelDistance.magnitude) && !hit.collider.isTrigger)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    gameObject.SetActive(false);
                    return;
                }

                newPosition = hit.point;
                gunShotData = BulletHitManager.Instance.OnGunShotHit(gunShotData, hit);
            }
            
            gunShotData.currentPosition = newPosition;
            transform.position = gunShotData.currentPosition;
        }

        private static float CalculateGravityVelocity()
        {
            return -9.82f * Time.deltaTime;
        }

        public void OnEnable()
        {
            trailRenderer.Clear();
            trailRenderer.enabled = false;
            destroyAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 2;
            gunShotData.velocity = Vector3.zero;
        }

        public void Shoot(GunShotData gunShot)
        {
            transform.position = gunShot.originPosition;
            gunShotData = gunShot;
            trailRenderer.enabled = true;
        }
    }
}