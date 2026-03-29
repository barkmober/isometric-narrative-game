using UnityEngine;

namespace SA
{
    public class PlayerCameraManager : MonoBehaviour
    {
        public static PlayerCameraManager instance;

        [Header("Camera Transforms")]
        public Transform cameraPivot;
        public Camera camera;

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
    }
}