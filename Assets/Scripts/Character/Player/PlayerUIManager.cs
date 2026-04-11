using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace SA
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;

        [HideInInspector] public PlayerUILoadingScreenManager playerUILoadingScreenManager;
        [HideInInspector] public PlayerUIPauseMenuManager playerUIPauseMenuManager;

        [Header("Flags")]
        public bool isLoading;

        [Header("Canvas Groups")]
        public CanvasGroup playerUICanvasGroup;
        public CanvasGroup playerScreenCanvasGroup;

        [Header("Icons")]
        public Image saveIcon;

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

            playerUILoadingScreenManager = GetComponentInChildren<PlayerUILoadingScreenManager>();
            playerUIPauseMenuManager = GetComponentInChildren<PlayerUIPauseMenuManager>();
        }

        public void ActivateSavingIcon()
        {
            saveIcon.gameObject.SetActive(true);
        }

        public void EnablePlayerScreen()
        {
            playerScreenCanvasGroup.alpha = 1;
        }

        public void DisablePlayerScreen()
        {
            playerScreenCanvasGroup.alpha = 0;
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