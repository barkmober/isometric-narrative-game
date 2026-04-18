using UnityEngine;

namespace SA
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("Movement Stats")]
        [SerializeField] protected float maxAerialSpeed = 2.5f;
        [SerializeField] protected float maxJumpingSpeed = 2.5f;
        [SerializeField] protected float maxWalkingSpeed = 2f;
        [SerializeField] protected float maxRunningSpeed = 3.75f;
        [SerializeField] protected float maxSprintingSpeed = 6.5f;
        [SerializeField] protected float rotationSpeed = 7f;
        [SerializeField] protected float accelerationFactor = 8f;
        [SerializeField] protected float decelerationFactor = 10f;
        [Space]
        public float currentSpeed;
        public float velocity;

        [Header("Gravity")]
        protected Vector3 yVelocity;
        protected bool fallingVelocityHasBeenSet = false;

        [SerializeField] protected float maxJumpHeight = 1;
        [SerializeField] protected float jumpMomentumMultiplier = 2.5f;
        [SerializeField] protected float gravityForce = -30f;
        [SerializeField] protected float groundedYVelocity = -20;
        [SerializeField] protected float fallStartYVelocity = -5;
        [SerializeField] protected float groundCheckSphereRadius = 0.25f;
        public float inAirTimer = 0;

        [Header("Parkour")]
        [SerializeField] Vector3 forwardRayOffset = new Vector3(0, 0.25f, 0);
        [SerializeField] float forwardRayLength = 0.8f;
        [SerializeField] float heightRayLength = 5;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            //GRAVITY
            HandleWallDetection();

            HandleGroundCheck();
            HandleGravity();
        }

        public float GetWalkingSpeed()
        {
            return maxWalkingSpeed;
        }

        //GRAVITY
        protected virtual void HandleWallDetection()
        {
            Vector3 origin = transform.position + character.characterController.center;

            character.hasWallInFront = Physics.Raycast(origin, transform.forward, .45f, WorldUtilityManager.instance.GetEnviroLayers(), QueryTriggerInteraction.Ignore);
        }

        protected virtual void HandleGroundCheck()
        {
            character.isGrounded = Physics.CheckSphere(transform.position, groundCheckSphereRadius, WorldUtilityManager.instance.GetEnviroLayers(), QueryTriggerInteraction.Ignore);
            character.animator.SetBool("isGrounded", character.isGrounded);
        }

        protected virtual void HandleGravity()
        {
            if (character.isGrounded)
            {
                if (yVelocity.y <= 0)
                {
                    inAirTimer = 0;
                    fallingVelocityHasBeenSet = false;
                    yVelocity.y = groundedYVelocity;
                }
            }
            else
            {
                if (!character.isJumping && !fallingVelocityHasBeenSet)
                {
                    fallingVelocityHasBeenSet = true;
                    yVelocity.y = fallStartYVelocity;
                }

                inAirTimer += Time.deltaTime;
                yVelocity.y += gravityForce * Time.deltaTime;
                character.animator.SetFloat("inAirTimer", inAirTimer);
            }

            if (character.isJumping)
            {
                yVelocity.y = 0;
                return;
            }

            if(!PlayerUIManager.instance.isLoading)
                character.characterController.Move(yVelocity * Time.deltaTime);
        }

        //OBSTACLE
        public ObstacleHitData ObstacleCheck()
        {
            var hitData = new ObstacleHitData();

            var forwardOrigin = transform.position + forwardRayOffset;
            hitData.hitForwardFound = Physics.Raycast(forwardOrigin, transform.forward, out hitData.forwardHit, forwardRayLength, WorldUtilityManager.instance.GetObstacleLayers());

            if (hitData.hitForwardFound)
            {
                var heightOrigin = hitData.forwardHit.point + Vector3.up * heightRayLength;
                hitData.hitHeightFound = Physics.Raycast(heightOrigin, Vector3.down, out hitData.heightHit, heightRayLength, WorldUtilityManager.instance.GetObstacleLayers());
            }

            return hitData;
        }
    }

    public struct ObstacleHitData
    {
        public bool hitForwardFound;
        public bool hitHeightFound;

        public RaycastHit forwardHit;
        public RaycastHit heightHit;
    }
}