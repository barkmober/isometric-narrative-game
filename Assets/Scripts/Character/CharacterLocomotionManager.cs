using UnityEngine;

namespace SA
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("Movement Stats")]
        [SerializeField] protected float maxAerialSpeed = 1.5f;
        [SerializeField] protected float maxWalkingSpeed = 2.5f;
        [SerializeField] protected float maxRunningSpeed = 3.5f;
        [SerializeField] protected float rotationSpeed = 7f;
        [SerializeField] protected float accelerationFactor = 5f;
        [SerializeField] protected float decelerationFactor = 10f;
        [Space]
        public float currentSpeed;

        [Header("Ground Detection and Gravity")]
        public LayerMask groundLayer;

        [Space]

        private Vector3 yVelocity;
        private bool fallingVelocityHasBeenSet = false;
        
        [SerializeField] protected float gravityForce = -30f;
        [SerializeField] protected float clampedGravityForce = -45;
        [SerializeField] protected float groundedYVelocity = -20;
        [SerializeField] protected float fallStartYVelocity = -5;
        [SerializeField] float groundCheckSphereRadius = 0.25f;
        public float inAirTimer = 0;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            HandleWallDetection();

            HandleGroundCheck();
            HandleGravity();
        }

        protected virtual void HandleWallDetection()
        {
            Vector3 origin = transform.position + Vector3.up * 1f;

            character.hasWallInFront = Physics.Raycast(origin, transform.forward, out RaycastHit hit, 0.5f, groundLayer);
        }

        protected virtual void HandleGroundCheck()
        {
            character.isGrounded = Physics.CheckSphere(transform.position, groundCheckSphereRadius, groundLayer, QueryTriggerInteraction.Ignore);
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
                if (!fallingVelocityHasBeenSet)
                {
                    fallingVelocityHasBeenSet = true;
                    yVelocity.y = fallStartYVelocity;
                }

                inAirTimer += Time.deltaTime;
                character.animator.SetFloat("inAirTimer", inAirTimer);

                if (yVelocity.y > clampedGravityForce)
                {
                    yVelocity.y += gravityForce * Time.deltaTime;
                }
            }

            character.characterController.Move(yVelocity * Time.deltaTime);
        }
    }
}