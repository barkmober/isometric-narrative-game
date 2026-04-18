using UnityEngine;

namespace SA
{
    public class CharacterManager : MonoBehaviour
    {
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Animator animator;

        [HideInInspector] public CharacterLocomotionManager characterLocomotionManager;
        [HideInInspector] public CharacterAnimatorManager characterAnimatorManager;
        [HideInInspector] public CharacterSoundFXManager characterSoundFXManager;

        [Header("Toggles")]
        public bool willJump = true;
        public bool willRun = true;
        public bool willSprint = true;

        [Header("FLAGS")]
        public bool isPerformingAction = false;
        public bool applyRootMotion = false;
        public bool hasWallInFront = false;

        public bool canMove = true;
        public bool canRun = true;
        public bool canJump = true;
        public bool canSprint = true;
        public bool canRotate = true;

        public bool isGrounded = false;
        public bool isMoving = false;
        public bool isJumping = false;
        public bool isWalking = false;
        public bool isRunning = false;
        public bool isSprinting = false;

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();

            characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
            characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {
            if (!willSprint)
                canSprint = false;

            if (!willRun)
                canRun = false;

            if (!willJump)
                canJump = false;
        }
    }
}