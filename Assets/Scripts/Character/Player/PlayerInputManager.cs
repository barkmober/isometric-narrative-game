using UnityEngine;
using UnityEngine.SceneManagement;

namespace SA
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        [HideInInspector] public PlayerManager player;

        PlayerControls playerControls;

        [Header("GAME INFORMATION")]
        public GameObject playerPrefab;
        public string currentVersion = "v.00";

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

            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += OnSceneChange;
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;
            }
            else
            {
                instance.enabled = false;
            }
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.Movement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            }

            playerControls.Enable();
        }

        private void OnDisable()
        {
            if (playerControls != null)
            {
                playerControls.Disable();
            }
        }

        private void Start()
        {
            instance.enabled = false;
        }

        private void Update()
        {
            HandleInputs();
        }

        private void HandleInputs()
        {
            HandleMovementInput();
            HandleInteractionInput();
        }

        private void HandleMovementInput()
        {

        }

        private void HandleInteractionInput()
        {
            if (interactInput)
            {
                interactInput = false;
            }
        }
    }
}