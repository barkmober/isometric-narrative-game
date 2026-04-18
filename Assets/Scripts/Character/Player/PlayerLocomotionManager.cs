using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SA
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;

        private Vector3 moveDirection;
        private Vector3 jumpDirection;
        private Vector3 targetRotationDirection;

        [Header("Parkour")]
        [SerializeField] List<ParkourAction> parkourActions;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        protected override void Update()
        {
            base.Update();

            CalculateSpeed();

            if (player.isJumping)
                return;

            HandleRotation();
            HandleGroundedMovement();
            HandleAerialMovement();
        }

        private void CalculateSpeed()
        {
            float maxSpeed;

            if (!player.isGrounded)
            {
                maxSpeed = maxAerialSpeed;
            }
            else
            {
                if (player.canRun)
                {
                    if (player.canSprint == true)
                    {
                        if (player.isSprinting)
                        {
                            maxSpeed = maxSprintingSpeed;
                        }
                        else
                        {
                            maxSpeed = maxRunningSpeed;
                        }
                    }
                    else
                    {
                        maxSpeed = maxRunningSpeed;
                    }
                }
                else
                {
                    if (player.canSprint == true)
                    {
                        if (player.isSprinting)
                        {
                            maxSpeed = maxRunningSpeed;
                        }
                        else
                        {
                            maxSpeed = maxWalkingSpeed;
                        }
                    }
                    else
                    {
                        maxSpeed = maxWalkingSpeed;
                    }
                }
            }

            if (!player.isMoving && currentSpeed > 0)
            {
                currentSpeed -= decelerationFactor * Time.deltaTime;
            }
            else if (player.isMoving && currentSpeed < maxSpeed) 
            {
                currentSpeed += accelerationFactor * Time.deltaTime;
            }

            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        }

        private void HandleGroundedMovement()
        {
            if (!player.canMove)
                return;

            if (!player.isGrounded)
                return;

            moveDirection = PlayerCameraManager.instance.camera.transform.forward * PlayerInputManager.instance.verticalMovement;
            moveDirection += PlayerCameraManager.instance.camera.transform.right * PlayerInputManager.instance.horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            player.characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
            player.playerAnimatorManager.SetMovementValues(PlayerInputManager.instance.moveAmount);
        }

        private void HandleAerialMovement()
        {
            if (player.isPerformingAction)
                return;

            if (player.isGrounded)
                return;

            Vector3 freeFallDirection;

            freeFallDirection = PlayerCameraManager.instance.camera.transform.forward * PlayerInputManager.instance.verticalMovement;
            freeFallDirection += PlayerCameraManager.instance.camera.transform.right * PlayerInputManager.instance.horizontalMovement;
            freeFallDirection.y = 0;

            player.characterController.Move(freeFallDirection * maxAerialSpeed * Time.deltaTime);
        }

        private void HandleRotation()
        {
            if (!player.canRotate)
                return;

            targetRotationDirection = Vector3.zero;

            targetRotationDirection = PlayerCameraManager.instance.camera.transform.forward * PlayerInputManager.instance.verticalMovement;
            targetRotationDirection += PlayerCameraManager.instance.camera.transform.right * PlayerInputManager.instance.horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if (targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

        //PARKOUR
        public void AttemptToParkour()
        {
            var hitData = player.playerLocomotionManager.ObstacleCheck();

            if (player.isPerformingAction)
                return;

            if (player.isJumping)
                return;

            if (!player.isGrounded)
                return;

            if (hitData.hitForwardFound)
            {
                foreach (var action in parkourActions)
                {
                    if (action.CheckIfPossible(hitData, player.transform))
                    {
                        StartCoroutine(DoParkourAction(action));
                        break;
                    }
                }
            }
        }

        IEnumerator DoParkourAction(ParkourAction action)
        {
            var animState = player.animator.GetNextAnimatorStateInfo(1);

            player.playerAnimatorManager.PlayTargetActionAnimation(action.animName, true);
            player.isJumping = true;

            float timer = 0;

            while (timer <= animState.length)
            {
                timer += Time.deltaTime;

                if (action.rotateToObstacle)
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, action.TargetRotation, 30 * Time.deltaTime);

                if (action.targetMatching)
                    MatchTarget(action);

                yield return null;
            }

            yield return new WaitForSeconds(animState.length);

            //player.isJumping = false;

            yield return null;
        }

        void MatchTarget(ParkourAction action)
        {
            if (player.animator.isMatchingTarget)
                return;

            player.animator.MatchTarget(action.MatchPos, transform.rotation, action.matchBodyPart, new MatchTargetWeightMask(new Vector3(0, 1, 0), 0), action.matchStartTime, action.matchTargetTime);
        }
    }
}