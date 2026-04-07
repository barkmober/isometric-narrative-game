using UnityEngine;

namespace SA
{
    public class PlayerCameraManager : MonoBehaviour
    {
        public static PlayerCameraManager instance;

        [Header("Camera Transforms")]
        public Transform cameraPivot;
        public Camera camera;

        [Header("Camera Stats")]
        private Vector3 currentVelocity;

        [SerializeField] float smoothTime = .25f;

        [Header("Camera Target")]
        public PlayerManager player;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            
        }

        private void LateUpdate()
        {
            HandleCameraFollow();
        }

        private void HandleCameraFollow()
        {
            if (player == null)
                return;

            Vector3 targetPosition = player.cameraFollowTarget.position;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        }
    }
}