using UnityEngine;

namespace SA
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("Audio Sources")]
        [SerializeField] AudioSource oneShotAudioSource;
        [SerializeField] AudioSource stackingAudioSource;

        [Header("Footstep SFX")]
        public bool overrideDefaultFootstepSFX = false;
        public AudioClip[] footStepsDirt;
        public AudioClip[] footStepsStone;
        public AudioClip[] footStepsGrass;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        public void PlaySoundFX(AudioClip clip, float volume = 1, bool randomizePitch = false, float randomPitch = 0.25f)
        {
            if (oneShotAudioSource.isPlaying)
                oneShotAudioSource.Stop();

            oneShotAudioSource.volume = volume;
            oneShotAudioSource.PlayOneShot(clip);

            if (randomizePitch)
            {
                oneShotAudioSource.pitch = Random.Range(-randomPitch, randomPitch);
            }
            else
            {
                oneShotAudioSource.pitch = 1;
            }
        }

        public void PlaySoundFXWithStacking(AudioClip clip, float volume = 1, bool randomizePitch = false, float randomPitch = 0.25f)
        {
            stackingAudioSource.volume = volume;
            stackingAudioSource.PlayOneShot(clip);

            if (randomizePitch)
            {
                stackingAudioSource.pitch = Random.Range(-randomPitch, randomPitch);
            }
            else
            {
                stackingAudioSource.pitch = 1;
            }
        }
    }
}