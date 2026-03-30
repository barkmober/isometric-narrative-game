using UnityEngine;

namespace SA
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            playerLocomotionManager.HandleAllMovement();
        }

        public void SaveGameDataToCharacter(ref CharacterSaveData currentCharacterData)
        {
            currentCharacterData.xPosition = transform.position.x;
            currentCharacterData.yPosition = transform.position.y;
            currentCharacterData.zPosition = transform.position.z;

            currentCharacterData.xRotation = transform.eulerAngles.x;
            currentCharacterData.yRotation = transform.eulerAngles.y;
            currentCharacterData.zRotation = transform.eulerAngles.z;

            currentCharacterData.xCameraPosition = PlayerCameraManager.instance.transform.position.x;
            currentCharacterData.yCameraPosition = PlayerCameraManager.instance.transform.position.y;
            currentCharacterData.zCameraPosition = PlayerCameraManager.instance.transform.position.z;
        }

        public void LoadGameDataToCharacter(ref CharacterSaveData currentCharacterData)
        {
            Vector3 playerPos = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
            transform.position = playerPos;

            Vector3 playerRot = new Vector3(currentCharacterData.xRotation, currentCharacterData.yRotation, currentCharacterData.zRotation);
            transform.eulerAngles = playerRot;

            Vector3 cameraPos = new Vector3(currentCharacterData.xCameraPosition, currentCharacterData.yCameraPosition, currentCharacterData.zCameraPosition);
            PlayerCameraManager.instance.transform.position = cameraPos;
        }
    }
}