using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace SA
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;

        public PlayerManager player;

        [Header("WORLD")]
        [SerializeField] int worldSceneIndex = 1;
        [SerializeField] Vector3 spawnPoint;

        [Header("DEBUG")]
        [SerializeField] bool saveGame;
        [SerializeField] bool deleteGame;

        [Header("SAVE DATA WRITER")]
        private SaveFileDataWriter saveFileDataWriter;

        [Header("GAME SAVING")]
        public CharacterSaveData currentCharacterData;
        public bool hasSaveFile = false;
        public CharacterSaveData characterSlot;
        public string fileName;

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

        private void Start()
        {
            LoadCharacterProfile();
        }

        private void Update()
        {
            if (saveGame)
            {
                saveGame = false;
                SaveGame();
            }

            if (deleteGame)
            {
                deleteGame = false;
                DeleteGame();
            }
        }

        public IEnumerator LoadWorldScene()
        {
            PlayerUIManager.instance.playerUILoadingScreenManager.ActivateLoadingScreen();
            PlayerUIManager.instance.isLoading = true;

            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
            loadOperation.allowSceneActivation = false;

            PlayerUIManager.instance.EnablePlayerUI();
            PlayerUIManager.instance.EnablePlayerScreen();

            GameObject player = Instantiate(PlayerInputManager.instance.playerPrefab, spawnPoint, Quaternion.identity);

            instance.player = player.GetComponent<PlayerManager>();
            PlayerInputManager.instance.player = player.GetComponent<PlayerManager>();
            PlayerCameraManager.instance.player = player.GetComponent<PlayerManager>();
            instance.player.gameObject.GetComponent<AudioListener>().enabled = false;

            WorldSoundEffectsManager.instance.StopMusic();

            loadOperation.allowSceneActivation = true;

            instance.player.LoadGameDataToCharacter(ref currentCharacterData);

            yield return new WaitForSeconds(1);

            SaveGame();
            PlayerInputManager.instance.ResumeGame();

            yield return null;
        }

        public IEnumerator LoadMenuScene()
        {
            SaveGame();

            PlayerUIManager.instance.playerUILoadingScreenManager.ActivateLoadingScreen();
            PlayerUIManager.instance.isLoading = true;

            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(0);
            loadOperation.allowSceneActivation = false;

            instance.player.gameObject.GetComponent<AudioListener>().enabled = false;
            PlayerCameraManager.instance.mainCamera.GetComponent<AudioListener>().enabled = true;
            Destroy(player.gameObject);

            yield return new WaitForSeconds(2);
            
            loadOperation.allowSceneActivation = true;

            PlayerUIManager.instance.DisablePlayerScreen();

            yield return null;
        }

        public void CreateNewGame()
        {
            fileName = "characterSave01";

            currentCharacterData = new CharacterSaveData();
        }

        public void SaveGame()
        {
            fileName = "characterSave01";

            saveFileDataWriter = new SaveFileDataWriter();

            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = fileName;

            player.SaveGameDataToCharacter(ref currentCharacterData);

            if(AutoSaveManager.instance != null)
                AutoSaveManager.instance.ResetSaveTick();

            PlayerUIManager.instance.ActivateSavingIcon();

            saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData);
        }

        public void LoadGame()
        {
            fileName = "characterSave01";

            saveFileDataWriter = new SaveFileDataWriter();

            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = fileName;
            currentCharacterData = saveFileDataWriter.LoadSaveFile();

            StartCoroutine(LoadWorldScene());
        }

        public void DeleteGame()
        {
            fileName = "characterSave01";

            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = fileName;
            saveFileDataWriter.DeleteSaveFile();
        }

        public void LoadCharacterProfile()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

            saveFileDataWriter.saveFileName = "characterSave01";
            characterSlot = saveFileDataWriter.LoadSaveFile();
        }

        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }
}