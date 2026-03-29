using UnityEngine;

namespace SA
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;

        [Header("Canvas Groups")]
        public CanvasGroup playerUICanvasGroup;

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

        public void EnablePlayerUI()
        {
            playerUICanvasGroup.alpha = 1;
        }

        public void DisablePlayerUI()
        {
            playerUICanvasGroup.alpha = 0;
        }
    }
}