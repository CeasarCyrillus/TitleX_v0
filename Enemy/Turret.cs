using System;
using UnityEngine;
using Weapons.Gun.Parts;
using Weapons.Gun.Parts.Controller;
using Weapons.Gun.Parts.data;
using Weapons.Gun.Parts.Data;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class Turret: MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float RPM;
        [SerializeField] private Transform player;
        [SerializeField] private Transform muzzle;
        [SerializeField] private float maxRange;
        [SerializeField] private float leadingShotRadius;
        private GunController gunController;
        private bool isShooting;
        
        private float timeBetweenShots;
        private float nextShotTime;
        private Vector3 playerLastKnownPosition;


        private void Start()
        {
            timeBetweenShots = 60f / RPM; 
            nextShotTime = 0f;
        }

        private void Awake()
        {
            gunController = GetComponent<GunController>();
            
            var barrelData = new BarrelData(200f, 11.4f);
            var barrel = new Barrel(barrelData);
            
            var triggerData = new TriggerData();
            var trigger = new Trigger(triggerData);
            
            
            var gunParts = new GunParts(trigger, barrel);
            gunController.SetParts(gunParts);
            
            var magazineData = new MagazineData(450, 20);
            var magazine = new Magazine(magazineData);
            var glockAmmo = new AmmoData(115, 5, 9.03f, 9.9f);
            magazine.Load(glockAmmo, 20);
            gunController.Reload(magazine);
        }

        private void Update()
        {
            // TODO: vision raycast to player to control target rotation
            // TODO: stop and Fire x shots, then rotate again 
            if (isShooting && Time.time >= nextShotTime)
            {
                nextShotTime = Time.time + timeBetweenShots;
                gunController.Shoot();
            }
            
            var direction = player.position - muzzle.position;
            if(Physics.Raycast(muzzle.position, direction, maxRange, LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore))
            {
                playerLastKnownPosition = player.position;
            }

            var playerLastKnownDirection = playerLastKnownPosition - transform.position; 
            var rotation = Quaternion.LookRotation(playerLastKnownDirection, Vector3.up).eulerAngles;
            rotation.x = 0f;
            rotation.z = 0f;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotation), rotationSpeed * Time.deltaTime);
            
            // TODO: instead calculate the angle betwen the turret rotation and the line of sight raycast, and start shooting if it is within a few angles
            if (Physics.SphereCastAll(muzzle.position, leadingShotRadius, transform.forward, maxRange, LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore).Length > 0)
            {
                isShooting = true;
            }
            else
            {
                isShooting = false;
            };
        }
    }
}