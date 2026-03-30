using UnityEngine;

namespace SA
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;

        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        public void HandleAllMovement()
        {
            CalculateSpeed();

            HandleGroundedMovement();
            HandleRotation();
        }

        private void CalculateSpeed()
        {
            float maxSpeed = maxWalkingSpeed;

            if (!player.isGrounded)
            {
                maxSpeed = maxAerialSpeed;
            }
            else
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

            moveDirection = PlayerCameraManager.instance.camera.transform.forward * PlayerInputManager.instance.verticalMovement;
            moveDirection += PlayerCameraManager.instance.camera.transform.right * PlayerInputManager.instance.horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            player.characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
            player.playerAnimatorManager.SetMovementValues(PlayerInputManager.instance.moveAmount);
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
    }
}