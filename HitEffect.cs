using Lib.ObjectPool;
using UnityEngine;

public class HitEffect: PoolableObject
{
    private Transform followPosition;
    private Vector3 delta;

    public void Init(Transform target, Vector3 hitPosition)
    {
        followPosition = target;
        delta = target.position - hitPosition;
    }

    private void Update()
    {
        transform.position = followPosition.position - delta;
    }
}