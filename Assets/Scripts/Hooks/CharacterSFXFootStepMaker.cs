using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class CharacterFootstepSFXMaker : MonoBehaviour
    {
        CharacterManager character;

        AudioSource audioSource;
        GameObject steppedOnObject;

        private bool hasTouchedGround = false;
        private bool hasPlayedFootstepSFX = false;
        Vector3 posi;

        [Header("Footstep Settings")]
        [SerializeField] float distanceToGround = 0.05f;
        [SerializeField] float footStepResetTime = 0.5f;     
        float timer;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            character = GetComponentInParent<CharacterManager>();
        }

        private void FixedUpdate()
        {
            CheckForFootsteps();

            if (timer > 0)
            {
                timer -= Time.fixedDeltaTime;
            }
        }

        private void CheckForFootsteps()
        {
            if (character == null)
                return;

            if (character.isPerformingAction)
                return;

            if (character.isMoving == false)
                return;

            if (character.characterLocomotionManager.currentSpeed < 3)
                return;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, character.transform.TransformDirection(Vector3.down), out hit, distanceToGround, WorldUtilityManager.instance.GetEnviroLayers()))
            {
                hasTouchedGround = true;
                posi = hit.point;

                if (!hasPlayedFootstepSFX)
                    steppedOnObject = hit.transform.gameObject;
            }
            else
            {
                hasTouchedGround = false;
                hasPlayedFootstepSFX = false;
                steppedOnObject = null;
                posi = Vector3.zero;
            }

            if (hasTouchedGround && !hasPlayedFootstepSFX && timer <= 0)
            {
                hasPlayedFootstepSFX = true;

                PlayFootstepSFX();
                PlayFootstepVFX(posi);
            }
        }

        private void PlayFootstepSFX()
        {
            character.characterSoundFXManager.PlaySoundFX(WorldSoundEffectsManager.instance.ChooseRandomFootstepSoundFX(steppedOnObject, character));
            timer = footStepResetTime;
        }

        private void PlayFootstepVFX(Vector3 pos)
        {
            GameObject dust = null;
            dust = Instantiate(WorldCharacterEffectsManager.instance.dustFootstepVFX, pos, Quaternion.identity);
            dust.transform.parent = null;

            Destroy(dust, 2);
        }
    }
}