using UnityEngine;

namespace SA
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        //[HideInInspector] public PlayerManager player;

        //PlayerControls playerControls;

        [Header("INPUTS")]
        [SerializeField] Vector2 movementInput;
        [SerializeField] float moveAmount = 0; //ABS VALUE FOR ANIMATION
        [SerializeField] float verticalMovement;
        [SerializeField] float horizontalMovement;

        [SerializeField] bool interactInput;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {

        }

        private void Update()
        {
            
        }
    }
}