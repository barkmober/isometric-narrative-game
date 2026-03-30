using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SA
{
    public class WorldSoundEffectsManager : MonoBehaviour
    {
        public static WorldSoundEffectsManager instance;

        [Header("AUDIO SOURCES")]
        [SerializeField] AudioSource oneShotAudioSource;
        [SerializeField] AudioSource musicAudioSource;

        [Header("UI SFX")]
        public AudioClip popUpOpenUISFX;
        public AudioClip popUpCloseUISFX;
        public AudioClip selectButtonUISFX;
        public AudioClip clickButtonUISFX;

        [Header("MUSIC TRACKS")]
        public MusicTrack[] tracks;

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

        public void PlaySoundFX(AudioClip clip, float volume = 1, bool randomizePitch = false, float randomPitch = 0.25f)
        {
            oneShotAudioSource.volume = volume;
            oneShotAudioSource.PlayOneShot(clip);

            if(randomizePitch)
            {
                oneShotAudioSource.pitch = Random.Range(-randomPitch, randomPitch);
            }
            else
            {
                oneShotAudioSource.pitch = 1;
            }
        }

        public AudioClip GetMusicTrackFromName(string clipName)
        {
            foreach (var track in tracks)
            {
                if (track.trackName == clipName)
                {
                    return track.musicClip;
                }
            }

            return null;
        }

        public void PlayMusic(string trackName, float fadeDuration = 0.5f, float volume = 1)
        {
            StartCoroutine(AnimateMusicCrossfade(GetMusicTrackFromName(trackName), fadeDuration, volume));
        }

        IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f, float volume = 1)
        {
            float percent = 0;

            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeDuration;
                musicAudioSource.volume = Mathf.Lerp(volume, 0, percent);
                yield return null;
            }

            if(nextTrack != null)
            {
                musicAudioSource.clip = nextTrack;
                musicAudioSource.Play();
            }
            else
            {
                yield return null;
            }

            percent = 0;

            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeDuration;
                musicAudioSource.volume = Mathf.Lerp(0, volume, percent);
                yield return null;
            }
        }
    }

    [System.Serializable]
    public class MusicTrack
    {
        public string trackName;
        public AudioClip musicClip;
    }
}