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
        [SerializeField] bool loadGame;

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

            if (loadGame)
            {
                loadGame = false;
                LoadGame();
            }
        }

        public IEnumerator LoadWorldScene()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
            loadOperation.allowSceneActivation = false;

            PlayerUIManager.instance.EnablePlayerUI();

            GameObject player = Instantiate(PlayerInputManager.instance.playerPrefab, spawnPoint, Quaternion.identity);

            instance.player = player.GetComponent<PlayerManager>();
            PlayerInputManager.instance.player = player.GetComponent<PlayerManager>();
            PlayerCameraManager.instance.player = player.GetComponent<PlayerManager>();

            WorldSoundEffectsManager.instance.PlayMusic("Otopor", 0.5f, 0);

            instance.player.LoadGameDataToCharacter(ref currentCharacterData);
            SaveGame();

            yield return new WaitForSeconds(2);

            loadOperation.allowSceneActivation = true;
            PlayerUIManager.instance.FadeOut();

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