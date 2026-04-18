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
        public Texture2D cursorIcon;
        public bool gamePaused = false;

        [Header("INPUTS")]
        [SerializeField] Vector2 movementInput;
        public float moveAmount = 0; //ABS VALUE FOR ANIMATION
        public float verticalMovement;
        public float horizontalMovement;

        [SerializeField] bool interact_Input;
        [SerializeField] bool escape_Input;

        [SerializeField] bool jump_Input;
        [SerializeField] bool sprint_Input;

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
        }

        private void OnApplicationFocus(bool focus)
        {
            if(focus)
            {
                playerControls.Enable();
            }
            else
            {
                playerControls.Disable();
            }
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;

                PlayerCameraManager.instance.camera.GetComponent<AudioListener>().enabled = false;
                
                if(player != null)
                    player.gameObject.GetComponent<AudioListener>().enabled = true;
            }
            else
            {
                instance.enabled = false;

                PlayerCameraManager.instance.camera.GetComponent<AudioListener>().enabled = true;

                if (player != null)
                    player.gameObject.GetComponent<AudioListener>().enabled = false;
            }

            PlayerUIManager.instance.playerUILoadingScreenManager.DeactivateLoadingScreen(2.5f);
            ResumeGame();
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

                playerControls.Actions.Jump.performed += i => jump_Input = true;
                playerControls.Actions.Interact.performed += i => interact_Input = true;
                playerControls.Actions.Escape.performed += i => escape_Input = true;                

                playerControls.Movement.Sprint.performed += i => sprint_Input = true;
                playerControls.Movement.Sprint.canceled += i => sprint_Input = false;
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
            SceneManager.activeSceneChanged += OnSceneChange;

            instance.enabled = false;
        }

        private void Update()
        {
            HandleInputs();
        }

        private void HandleInputs()
        {
            HandlePauseInput();

            HandleMovementInput();
            HandleSprintInput();

            HandleInteractionInput();
            HandleJumpingInput();
        }

        private void HandlePauseInput()
        {
            if (escape_Input)
            {
                escape_Input = false;

                if (SceneManager.GetActiveScene().buildIndex != WorldSaveGameManager.instance.GetWorldSceneIndex())
                    return;

                if (PlayerUIManager.instance.isLoading)
                    return;

                if (gamePaused)
                {
                    if (PlayerUIManager.instance.playerUIPauseMenuManager.hasSettingsMenuOpen)
                    {
                        PlayerUIManager.instance.playerUIPauseMenuManager.CloseSettingsMenu();
                    }
                    else
                    {
                        ResumeGame();
                    }
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void PauseGame()
        {
            gamePaused = true;
            Time.timeScale = 0;

            PlayerUIManager.instance.playerUIPauseMenuManager.ActivatePauseMenu();
        }

        public void ResumeGame()
        {
            gamePaused = false;
            Time.timeScale = 1;

            PlayerUIManager.instance.playerUIPauseMenuManager.DeactivatePauseMenu();
        }

        public void ReturnToMenu()
        {
            StartCoroutine(WorldSaveGameManager.instance.LoadMenuScene());
        }

        private void HandleMovementInput()
        {
            if (gamePaused)
                return;

            verticalMovement = movementInput.y;
            horizontalMovement = movementInput.x;

            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalMovement) + Mathf.Abs(horizontalMovement));

            if (PlayerUIManager.instance.isLoading)
                return;

            if (moveAmount <= 0.5f && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5f && moveAmount <= 0)
            {
                moveAmount = 1;
            }

            if (moveAmount > 0.1 && player.canMove)
            {
                player.isMoving = true;
            }
            else
            {
                player.isMoving = false;
                player.isRunning = false;
            }

            if (player.isMoving)
            {
                if (player.canRun)
                {
                    player.isRunning = true;
                    player.isWalking = false;
                }
                else
                {
                    player.isWalking = true;
                    player.isRunning = false;
                }
            }
        }

        private void HandleSprintInput()
        {
            if (gamePaused)
                return;

            if (player.isPerformingAction)
            {
                player.isSprinting = false;
                return;
            }
                
            if (sprint_Input && player.isMoving && player.isGrounded)
            {
                if (player.canMove && !player.isPerformingAction && !player.hasWallInFront)
                {
                    if (PlayerUIManager.instance.isLoading)
                        return;

                    player.isSprinting = true;
                }
                else
                {
                    player.isSprinting = false;
                }
            }
            else
            {
                player.isSprinting = false;
            }
        }

        private void HandleInteractionInput()
        {
            if (interact_Input)
            {
                interact_Input = false;

                if (gamePaused)
                    return;

                if (PlayerUIManager.instance.isLoading)
                    return;
            }
        }

        private void HandleJumpingInput()
        {
            if (jump_Input)
            {
                jump_Input = false;

                if (gamePaused)
                    return;

                if (PlayerUIManager.instance.isLoading)
                    return;

                if (!player.canJump)
                    return;

                player.playerLocomotionManager.AttemptToParkour();
            }
        }
    }
}