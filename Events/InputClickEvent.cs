using UnityEngine;

namespace Events
{
    public struct InputClickEvent
    {
        public readonly Vector3 clickPosition;

        public InputClickEvent(Vector3 clickPosition)
        {
            this.clickPosition = clickPosition;
        }
    }
}