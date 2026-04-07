using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace SA
{
    public class PlayerUILoadingScreenManager : MonoBehaviour
    {
        [SerializeField] GameObject loadingScreen;
        [SerializeField] CanvasGroup loadingScreenCanvasGroup;

        private Coroutine fadeLoadingScreenCoroutine;

        public void ActivateLoadingScreen()
        {
            if (loadingScreen.activeSelf)
                return;

            loadingScreenCanvasGroup.alpha = 1;
            loadingScreen.SetActive(true);
            PlayerUIManager.instance.isLoading = true;
        }

        public void DeactivateLoadingScreen(float delay = 1)
        {
            if (!loadingScreen.activeSelf)
                return;

            if (fadeLoadingScreenCoroutine != null)
                return;

            fadeLoadingScreenCoroutine = StartCoroutine(FadeLoadingScreen(1, delay));
        }

        private IEnumerator FadeLoadingScreen(float duration, float delay)
        {
            loadingScreen.SetActive(true);

            if (duration > 0)
            {
                while (delay > 0)
                {
                    delay -= Time.deltaTime;
                    yield return null;
                }

                loadingScreenCanvasGroup.alpha = 1;
                float elapsedTime = 0;

                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    loadingScreenCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / duration);

                    if(loadingScreenCanvasGroup.alpha > 0.5f)
                        PlayerUIManager.instance.isLoading = false;

                    yield return null;
                }
            }

            loadingScreenCanvasGroup.alpha = 0;
            loadingScreen.SetActive(false);
            fadeLoadingScreenCoroutine = null;
            PlayerUIManager.instance.isLoading = false;

            yield return null;
        }
    }
}