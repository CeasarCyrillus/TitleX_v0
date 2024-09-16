using System;
using UnityEngine;
using Weapons.Gun.Parts.data;
using Weapons.Gun.Parts.Data;

namespace Lib
{
    public class BulletHitUtil: MonoBehaviour
    {
        private static readonly float bounceCoefficient = 0.001f;
        [SerializeField] private float globalPenetrationModifier = 0.01f;
        
        // TODO: bullet modifiers
        
        private static BulletHitUtil _instance;
        public static BulletHitUtil Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }
        
        public static Vector3 GetRicochetVelocity(Vector3 velocity, Vector3 hitNormal)
        {
            var initialSpeed = velocity.magnitude;
        
            // Calculate the angle of incidence
            float angleOfIncidence = Vector3.Angle(-velocity, hitNormal);
        
            // Convert angle to radians for trigonometric calculations
            float theta = angleOfIncidence * Mathf.Deg2Rad;

            // Calculate the components of the initial velocity
            float vParallel = initialSpeed * Mathf.Sin(theta);
            float vPerpendicular = initialSpeed * Mathf.Cos(theta);

            // Calculate the components of the velocity after impact
            float vParallelAfter = vParallel;
            float vPerpendicularAfter = bounceCoefficient * vPerpendicular;

            // Calculate the new speed after impact
            float newSpeed = Mathf.Sqrt(vParallelAfter * vParallelAfter + vPerpendicularAfter * vPerpendicularAfter);

            // Calculate the new velocity vector after impact
            Vector3 newVelocity = Vector3.Reflect(velocity.normalized, hitNormal) * newSpeed;
            return newVelocity;
        }
    }
}