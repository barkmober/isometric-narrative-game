using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace SA
{
    public class BlinkLoadingIcon : MonoBehaviour
    {
        [SerializeField] Image fadeImage;
        private Coroutine fadeCoroutine;

        private void OnEnable()
        {
            FadeUIImage();
        }

        private void OnDisable()
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
        }

        public void FadeUIImage()
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeOnce());
        }

        private IEnumerator FadeOnce()
        {
            for (int blink = 0; blink < 2; blink++)
            {
                for (float i = 1f; i >= 0f; i -= Time.unscaledDeltaTime)
                {
                    fadeImage.color = new Color(1, 1, 1, i);
                    yield return null;
                }

                if (blink == 1)
                {
                    gameObject.SetActive(false);
                    yield break;
                }

                for (float i = 0f; i <= 1f; i += Time.unscaledDeltaTime)
                {
                    fadeImage.color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
        }
    }
}