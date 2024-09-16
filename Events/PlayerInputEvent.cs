using UnityEngine;

namespace Events
{
    public struct PlayerInputEvent
    {
        public readonly Vector3 LocomotionDirection;
        public readonly Vector3 MousePosition;
        public readonly Vector3 mouseWorldPosition;
        
        public readonly bool isInAimMode;
        public readonly bool IsSprintingKey;
        public readonly bool IsWalkingKey;
        public readonly bool IsLeftClick;
        public readonly float Scroll;
        public readonly bool interactKeyPressed;
        public readonly bool interactKeyDown;
        
        public PlayerInputEvent(
            Vector3 locomotionDirection, 
            bool isInAimMode,
            bool isSprintingKey,
            bool isWalkingKey,
            Vector3 mousePosition,
            bool isLeftClick,
            float scroll,
            Vector3 mouseWorldPosition,
            bool interactKeyPressed, 
            bool interactKeyDown)
        {
            LocomotionDirection = locomotionDirection;
            this.isInAimMode = isInAimMode;
            IsSprintingKey = isSprintingKey;
            IsWalkingKey = isWalkingKey;
            MousePosition = mousePosition;
            IsLeftClick = isLeftClick;
            Scroll = scroll;
            this.mouseWorldPosition = mouseWorldPosition;
            this.interactKeyPressed = interactKeyPressed;
            this.interactKeyDown = interactKeyDown;
        }
        
        public static PlayerInputEvent Default()
        {
            return new PlayerInputEvent(
                Vector3.zero,
                false,
                false,
                true,
                Vector3.zero,
                false,
                0f,
                Vector3.zero,
                false,
                false);
        }
    }
}