using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SA
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;

        [Header("Transforms")]
        public Transform cameraFollowTarget;

        [Header("Events")]
        public List<WorldEventSO> allFiredEvents;

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

            //EVENTS
            currentCharacterData.eventsFired.Clear();
            for (int i = 0; i < allFiredEvents.Count; i++)
            {    
                currentCharacterData.eventsFired.Add(allFiredEvents[i].eventID);
            }
        }

        public void LoadGameDataToCharacter(ref CharacterSaveData currentCharacterData)
        {
            Vector3 playerPos = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
            transform.position = playerPos;

            Vector3 playerRot = new Vector3(currentCharacterData.xRotation, currentCharacterData.yRotation, currentCharacterData.zRotation);
            transform.eulerAngles = playerRot;

            Vector3 cameraPos = new Vector3(currentCharacterData.xCameraPosition, currentCharacterData.yCameraPosition, currentCharacterData.zCameraPosition);
            PlayerCameraManager.instance.transform.position = cameraPos;

            StartCoroutine(LoadEvents(currentCharacterData));
        }

        IEnumerator LoadEvents(CharacterSaveData currentCharacterData)
        {
            yield return new WaitForSeconds(1);
            //EVENTS
            for (int i = 0; i < currentCharacterData.eventsFired.Count; i++)
            {
                WorldEventSO _event = WorldObjectDatabase.instance.GetWorldEventByID(currentCharacterData.eventsFired[i]);
                _event.AddEventToList(this);

                WorldEventManager.instance.FireEventByIDLoad(_event.eventID);
            }

            yield return null;
        }
    }
}