using UnityEngine;

namespace SA
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;

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

        public void SetMovementValues(float moveAmount)
        {
            if (character.canRun)
            {
                if (character.canSprint)
                {
                    if (character.isSprinting)
                    {
                        character.animator.SetFloat("MoveAmount", 2, 0.15f, Time.deltaTime);
                    }
                    else
                    {
                        character.animator.SetFloat("MoveAmount", 1, 0.15f, Time.deltaTime);
                    }
                }
                else
                {
                    character.animator.SetFloat("MoveAmount", 1, 0.15f, Time.deltaTime);
                }
            }
            else
            {
                if (character.canSprint)
                {
                    if (character.isSprinting)
                    {
                        character.animator.SetFloat("MoveAmount", 1, 0.15f, Time.deltaTime);
                    }
                    else
                    {
                        character.animator.SetFloat("MoveAmount", .5f, 0.15f, Time.deltaTime);
                    }
                }
                else
                {
                    character.animator.SetFloat("MoveAmount", 1, 0.15f, Time.deltaTime);
                }
            }

            if(character.isPerformingAction)
                character.animator.SetFloat("MoveAmount", 0, 0.15f, Time.deltaTime);

            if (character.hasWallInFront)
                character.animator.SetFloat("MoveAmount", .5f, 0.15f, Time.deltaTime);

            character.animator.SetBool("isMoving", character.isMoving);
        }

        public virtual void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false)
        {
            character.applyRootMotion = applyRootMotion;
            character.isPerformingAction = isPerformingAction;
            character.animator.CrossFade(targetAnimation, .1f);

            character.canMove = canMove;
            character.canRotate = canRotate;
        }

        protected virtual void OnAnimatorMove()
        {

        }

        public void PlayLandFX()
        {
            GameObject dust = null;
            dust = Instantiate(WorldCharacterEffectsManager.instance.landingDustVFX, transform.position, Quaternion.identity);
            dust.transform.parent = null;

            Destroy(dust, 3);

            character.characterSoundFXManager.PlaySoundFXWithStacking(WorldSoundEffectsManager.instance.landSFX);
        }
    }
}