using UnityEngine;

namespace SA
{
    public class WorldSoundEffectsManager : MonoBehaviour
    {
        public static WorldSoundEffectsManager instance;

        AudioSource audioSource;

        [Header("UI SFX")]
        public AudioClip popUpOpenUISFX;
        public AudioClip popUpCloseUISFX;
        public AudioClip selectButtonUISFX;
        public AudioClip clickButtonUISFX;

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

            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySoundFX(AudioClip clip, float volume = 1, bool randomizePitch = false, float randomPitch = 0.25f)
        {
            audioSource.volume = volume;
            audioSource.PlayOneShot(clip);

            if(randomizePitch)
            {
                audioSource.pitch = Random.Range(-randomPitch, randomPitch);
            }
            else
            {
                audioSource.pitch = 1;
            }
        }
    }
}