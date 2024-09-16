using System.Threading.Tasks;
using Events;
using Lib;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float acceleration;
        [SerializeField] private float breakingSpeed;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float changeDirectionSpeed = 30f;
        [SerializeField] private float gravity = 9.82f;
        
        private CharacterController controller;
        
        private PlayerInputEvent playerInput = PlayerInputEvent.Default();
        
        private Vector3 currentVelocity;
        private bool isInBuildMode;
        
        
        private void Start()
        {
            controller = GetComponent<CharacterController>();
            currentVelocity = Vector3.zero;
            EventBus.Instance.Subscribe<PlayerInputEvent>(OnPlayerInput);
        }
        
        Task OnPlayerInput(PlayerInputEvent inputEvent)
        {
            playerInput = inputEvent;
            return Task.CompletedTask;
        }
        
        
        private void Update()
        {
            currentVelocity = CalculateVelocity();
            Rotate();
            controller.Move((currentVelocity + Vector3.down * gravity) * Time.deltaTime);   
        }
        
        private void Rotate()
        {
            Quaternion targetRotation;
            if (playerInput.isInAimMode)
            {
                Vector3 direction = playerInput.mouseWorldPosition - transform.position;
                targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                controller.transform.rotation = targetRotation;
            }
            else
            {
                var direction = playerInput.LocomotionDirection;
                if (direction != Vector3.zero)
                {
                    var relative = (transform.position + direction) - transform.position;
                    targetRotation = Quaternion.LookRotation(relative, Vector3.up);
                    controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, targetRotation, changeDirectionSpeed * Time.deltaTime);
                }
            }
        }
        
        private Vector3 CalculateVelocity()
        {
            var targetVelocity = GetTargetVelocity();
            if (targetVelocity.magnitude == 0f)
            {
                return Vector3.MoveTowards(currentVelocity, Vector3.zero, breakingSpeed * Time.deltaTime);
            }

            return Vector3.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        
        private Vector3 GetTargetVelocity()
        {
            var direction = playerInput.LocomotionDirection.normalized;
            var speed = playerInput.IsSprintingKey ? sprintSpeed : walkSpeed;
            if (playerInput.isInAimMode)
            {
                speed = walkSpeed - 2f;
            }
            return direction * speed;
        }
    }
}
