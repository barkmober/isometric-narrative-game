using UnityEngine;

namespace SA
{
    public class PlayerCameraManager : MonoBehaviour
    {
        public static PlayerCameraManager instance;

        [Header("Setup")]
        public PlayerManager player;
        public Camera mainCamera;

        [Header("Follow Settings")]
        [SerializeField] private float followSpeed = 2f;
        public bool followY = false;

        [Header("Render Texture Settings")]
        public float rtWidth = 640f;
        public float rtHeight = 360f;

        private float snapXZ;
        private float snapY;
        private Vector3 smoothedPos;

        private Vector3 velocity;
        private bool initialized = false;
        private float unitsPerPixel;

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

        private void LateUpdate()
        {
            if (player == null) return;

            if (!initialized)
            {
                smoothedPos = player.cameraFollowTarget.position;
                transform.position = smoothedPos;
                PlayerUIManager.instance.playerScreen.uvRect = new Rect(0, 0, 1, 1);
                initialized = true;
                return;
            }

            Vector3 targetPos = player.cameraFollowTarget.position;

            if (!followY)
                targetPos.y = smoothedPos.y;

            smoothedPos = Vector3.SmoothDamp(smoothedPos, targetPos, ref velocity, 1f / followSpeed);

            if (velocity.magnitude < 0.0001f)
                velocity = Vector3.zero;

            unitsPerPixel = (mainCamera.orthographicSize * 2f) / rtHeight;

            Matrix4x4 W2C = mainCamera.worldToCameraMatrix;
            Matrix4x4 C2W = mainCamera.cameraToWorldMatrix;

            Vector3 camSpace = W2C.MultiplyPoint3x4(smoothedPos);

            float snappedCamX = Mathf.Round(camSpace.x / unitsPerPixel) * unitsPerPixel;
            float snappedCamY = Mathf.Round(camSpace.y / unitsPerPixel) * unitsPerPixel;

            float subTexelX = camSpace.x - snappedCamX;
            float subTexelY = camSpace.y - snappedCamY;

            Vector3 snappedCamSpace = new Vector3(snappedCamX, snappedCamY, camSpace.z);
            Vector3 snappedWorld = C2W.MultiplyPoint3x4(snappedCamSpace);
            transform.position = snappedWorld;

            float subTexelU = (subTexelX / unitsPerPixel) / rtWidth;
            float subTexelV = (subTexelY / unitsPerPixel) / rtHeight;

            PlayerUIManager.instance.playerScreen.uvRect = new Rect(subTexelU, subTexelV, 1f, 1f);

            snapXZ = unitsPerPixel / Mathf.Cos(45f * Mathf.Deg2Rad);
            snapY = unitsPerPixel / Mathf.Sin(30f * Mathf.Deg2Rad);
        }
    }
}