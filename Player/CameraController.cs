using System.Threading.Tasks;
using Events;
using UnityEngine;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float zoomSpeed = 100f;
        [SerializeField] private Transform cameraTargetPosition;
        
        private Camera playerCamera;
        private float targetZoom;
        private float originalZoom;
        private readonly float minZoom = 1.5f;
        private readonly float maxZoom = 10;
        private Quaternion cameraTargetRotation;

        private void Start()
        {
            cameraTargetRotation = Quaternion.Euler(30, 45, 0);
            playerCamera = Camera.main;
            EventBus.Instance.Subscribe<PlayerInputEvent>(OnPlayerInput);
        }

        private Task OnPlayerInput(PlayerInputEvent inputEvent)
        {
            if (inputEvent.Scroll != 0)
            {
                targetZoom -= inputEvent.Scroll * 0.5f;
            }
            
            return Task.CompletedTask;
        }
        
        void Update()
        {
            gameObject.transform.position = Vector3.Slerp(gameObject.transform.position, cameraTargetPosition.position, Time.deltaTime * 10f);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, cameraTargetRotation, Time.deltaTime * 10f);

            if (targetZoom != 0)
            {
                targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
                playerCamera.orthographicSize = Mathf.SmoothDamp(playerCamera.orthographicSize, targetZoom, ref zoomSpeed, 0.1f, float.PositiveInfinity, Time.deltaTime);
            }
        }
    }
}
