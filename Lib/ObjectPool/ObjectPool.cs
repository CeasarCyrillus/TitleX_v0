using System.Collections.Generic;
using UnityEngine;

namespace Lib.ObjectPool
{
    public class ObjectPool
    {
        private GameObject parent;
        private readonly PoolableObject prefab;
        private readonly int size;
        private readonly List<PoolableObject> availableObjectsPool;
        private static readonly Dictionary<PoolableObject, ObjectPool> ObjectPools = new();

        private ObjectPool(PoolableObject prefab, int size)
        {
            this.prefab = prefab;
            this.size = size;
            availableObjectsPool = new List<PoolableObject>(size);
        }

        public static ObjectPool CreateInstance(PoolableObject prefab, int size)
        {
            ObjectPool pool = null;

            if (ObjectPools.TryGetValue(prefab, out var objectPool))
            {
                pool = objectPool;
            }
            else
            {
                pool = new ObjectPool(prefab, size)
                {
                    parent = new GameObject(prefab + " Pool")
                };

                pool.CreateObjects();

                ObjectPools.Add(prefab, pool);
            }


            return pool;
        }

        private void CreateObjects()
        {
            for (int i = 0; i < size; i++)
            {
                CreateObject();
            }
        }

        private void CreateObject()
        {
            PoolableObject poolableObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent.transform);
            poolableObject.Parent = this;
            poolableObject.gameObject.SetActive(false);
        }

        public PoolableObject GetObject()
        {
            if (availableObjectsPool.Count == 0) // auto expand pool size if out of objects
            {
                CreateObject();
            }

            PoolableObject instance = availableObjectsPool[0];

            availableObjectsPool.RemoveAt(0);

            instance.gameObject.SetActive(true);

            return instance;
        }

        public void ReturnObjectToPool(PoolableObject Object)
        {
            availableObjectsPool.Add(Object);
        }
    }
}