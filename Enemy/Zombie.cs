using System;
using Lib;
using UnityEngine;
using Weapons.Gun.Parts.data;

namespace Enemy
{
    public class Zombie: HealthController
    {
        private GameObject player;

        private void OnEnable()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        protected override void OnDeath(Vector3 impactPoint, GunShotData gunShotData)
        {
            rb.constraints = RigidbodyConstraints.None;
            base.OnDeath(impactPoint, gunShotData);
            Invoke("Remove", 5f);
        }

        private void Remove()
        {
            gameObject.SetActive(false);
        }

        public void Update()
        {
            if (!IsAlive()) return;
            var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            
            var yRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, yRotation, Time.deltaTime * 5f);
            transform.position += transform.forward * Time.deltaTime * 2.5f;
        }

        public void Respawn(Vector3 position)
        {
            healthPoints = 10f;
            
            transform.position = position;
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}