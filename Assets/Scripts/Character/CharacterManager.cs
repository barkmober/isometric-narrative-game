using UnityEngine;

namespace SA
{
    public class CharacterManager : MonoBehaviour
    {
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Animator animator;

        [Header("FLAGS")]
        public bool isPerformingAction = false;
        public bool applyRootMotion = false;
        public bool canMove = true;
        public bool canRotate = true;
        public bool isMoving = false;

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }
    }
}