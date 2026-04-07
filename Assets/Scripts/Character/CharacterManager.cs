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

        [Header("FLAGS")]
        public bool isPerformingAction = false;
        public bool applyRootMotion = false;
        public bool hasWallInFront = false;

        public bool canMove = true;
        public bool canRotate = true;

        public bool isGrounded = false;
        public bool isMoving = false;
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
    }
}