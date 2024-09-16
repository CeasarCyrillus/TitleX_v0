
using UnityEngine;

namespace Events
{
    public struct MouseWorldPositionEvent
    {
        public readonly Vector3 mouseWorldPosition;

        public MouseWorldPositionEvent(Vector3 mouseWorldPosition)
        {
            this.mouseWorldPosition = mouseWorldPosition;
        }
    }
}