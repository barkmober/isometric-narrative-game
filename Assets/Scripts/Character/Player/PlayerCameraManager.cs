using UnityEngine;

namespace SA
{
    public class PlayerCameraManager : MonoBehaviour
    {
        public static PlayerCameraManager instance;

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