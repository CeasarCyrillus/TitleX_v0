using Lib;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons.Gun.Parts.data;

namespace Player
{
    public class PlayerHealthController: HealthController
    {
        [SerializeField] private GameObject deadPlayerPrefab;

        protected override void  OnDeath(Vector3 impactPoint, GunShotData gunShotData)
        {
            var deadPlayer = Instantiate(deadPlayerPrefab);
            deadPlayer.transform.position = transform.position;
            deadPlayer.transform.rotation = transform.rotation;
            var rigidBody = deadPlayer.GetComponent<Rigidbody>();
            var force = gunShotData.velocity * UnitConverter.GrainsToKilograms(gunShotData.ammoData.bulletGrains);
            rigidBody.AddForceAtPosition(force * 2, impactPoint, ForceMode.Impulse);
            gameObject.SetActive(false);
        }
    }
}