using UnityEngine;

namespace Lib.ObjectPool
{
    public class PoolableObject : MonoBehaviour
    {
        public ObjectPool Parent;

        public void OnDisable()
        {
            Parent.ReturnObjectToPool(this);
        }
    }
}