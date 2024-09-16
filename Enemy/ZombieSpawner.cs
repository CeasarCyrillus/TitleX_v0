using System;
using System.Collections;
using Lib.ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class ZombieSpawner: MonoBehaviour
    {
        [SerializeField] private PoolableObject zombiePrefab;
        [SerializeField] private float spawnRange;
        private ObjectPool zombiePool;
        private GameObject player;
        
        private void Start()
        {
            zombiePool = ObjectPool.CreateInstance(zombiePrefab, 10);
            player = GameObject.FindGameObjectWithTag("Player");
            InvokeRepeating(nameof(SpawnZombie), 0f, 2f);
        }

        private void SpawnZombie()
        {
            var angle = Random.Range(0f, Mathf.PI * 2);
            var x = Mathf.Cos(angle) * spawnRange;
            var z = Mathf.Sin(angle) * spawnRange;
            
            var position = player.transform.position + new Vector3(x, 0, z);
            position.y = 1f;
            
            var zombie = zombiePool.GetObject();
            var zombieController = zombie.GetComponent<Zombie>();
            zombieController.Respawn(position);
        }
    }
}