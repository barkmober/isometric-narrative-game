using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SA
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;

        [Header("Canvas Groups")]
        [SerializeField] float fadeDuration = 0.5f;
        public bool loadingScreenActive = false;
        public CanvasGroup playerUICanvasGroup;
        public CanvasGroup playerScreenCanvasGroup;
        public CanvasGroup loadingScreenCanvasGroup;

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

        private void Update()
        {
            loadingScreenActive = loadingScreenCanvasGroup.alpha > 0;
        }

        public void EnablePlayerUI()
        {
            playerUICanvasGroup.alpha = 1;
        }

        public void DisablePlayerUI()
        {
            playerUICanvasGroup.alpha = 0;
        }

        public void FadeIn()
        {
            StartCoroutine(FadeRoutine(1f));
        }

        public void FadeOut()
        {
            StartCoroutine(FadeRoutine(0f));
        }

        IEnumerator FadeRoutine(float targetAlpha)
        {
            float startAlpha = loadingScreenCanvasGroup.alpha;
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / fadeDuration;

                float smoothT = Mathf.SmoothStep(0f, 1f, t);

                loadingScreenCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, smoothT);

                yield return null;
            }

            loadingScreenCanvasGroup.alpha = targetAlpha;
        }
    }
}