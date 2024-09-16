using System.Collections.Generic;
using UnityEngine;
using Weapons.Gun.Parts.Data;

namespace Weapons.Gun.Parts.data
{
    public struct GunShotData
    {
        public Vector3 velocity;
        public Vector3 currentPosition;
        public HashSet<HealthController> hits;
        public readonly Vector3 originPosition;
        public readonly AmmoData ammoData;

        public GunShotData(Vector3 velocity, Vector3 originPosition, AmmoData ammoData)
        {
            this.velocity = velocity;
            this.originPosition = originPosition;
            this.ammoData = ammoData;
            currentPosition = this.originPosition;
            hits = new HashSet<HealthController>();
        }
    }
}