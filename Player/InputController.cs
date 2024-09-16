using Events;
using Lib;
using UnityEngine;

namespace Player
{
    public class InputController: MonoBehaviour
    {
        private Camera playerCamera;
        private PlayerInputEvent previousInput;
        private PlayerInputEvent currentInput;

        private bool isInInventoryMode;
        private bool isInAimingMode;

        private void Start()
        {
            playerCamera = Camera.main;
        }

        void Update()
        {
            previousInput = currentInput;
            
            for (int i = 1; i <= 9; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    EventBus.Instance.Publish(new InputNumericKeysEvent(i));
                }
            }
            
            var scroll = Input.mouseScrollDelta.y;
            var locomotionDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).ToIso();
            var isSprintingKey = Input.GetKey(KeyCode.LeftShift);
            
            var isAimingInput = Input.GetMouseButtonDown(1);
            if (isAimingInput)
            {
                isInAimingMode = !isInAimingMode;
                EventBus.Instance.Publish(new IsInAimModeEvent(isInAimingMode));
            }

            var isInventoryButtonPressed = Input.GetKeyDown(KeyCode.I);
            if (isInventoryButtonPressed)
            {
                isInInventoryMode = !isInInventoryMode;
                EventBus.Instance.Publish(new InputToggleInventoryEvent(isInInventoryMode));
            }
            
            var interactKeyPressed = Input.GetKeyDown(KeyCode.E);
            if (interactKeyPressed)
            {
                EventBus.Instance.Publish(new InputInteractEvent());
            }
            var interactKeyDown = Input.GetKey(KeyCode.E);
            
            var mouseScreenPosition = Input.mousePosition;
            var ray = playerCamera.ScreenPointToRay(mouseScreenPosition);
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            var mouseWorldPosition = Vector3.zero;
            if (groundPlane.Raycast(ray, out var distance))
            {
                mouseWorldPosition = ray.GetPoint(distance);
                EventBus.Instance.Publish(new MouseWorldPositionEvent(mouseWorldPosition));
            }
            
            
            var isWalkingKey = Input.GetKeyDown(KeyCode.LeftAlt);
            var isLeftClick = Input.GetMouseButtonDown(0);
            if (isLeftClick)
            {
                EventBus.Instance.Publish(new InputClickEvent(mouseWorldPosition));
            }
            
            bool isWalkingInput;
            if (isWalkingKey)
            {
                isWalkingInput = !previousInput.IsWalkingKey;
            }
            else
            {
                isWalkingInput = previousInput.IsWalkingKey;
            }
            
            var playerInputEvent = new PlayerInputEvent(
                locomotionDirection,
                isInAimingMode,
                isSprintingKey,
                isWalkingInput,
                mouseScreenPosition,
                isLeftClick,
                scroll,
                mouseWorldPosition,
                interactKeyPressed,
                interactKeyDown);
            
            currentInput = playerInputEvent;
            EventBus.Instance.Publish(playerInputEvent);
        }
    }
}