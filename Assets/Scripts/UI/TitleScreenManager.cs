using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

namespace SA
{
    public class TitleScreenManager : MonoBehaviour
    {
        SaveFileDataWriter saveFileDataWriter;

        [Header("DEBUG")]
        public bool deleteSettings = false;

        [Header("Texts")]
        public TMP_Text versionText;

        [Header("Buttons")]
        public GameObject mainMenuButtons;
        public Button continueGameButton;
        public Button newGameButton;
        public Button optionsButton;
        public Button quitGameButton;

        [Header("Slider")]
        public Slider masterVolumeSlider;
        public Slider musicVolumeSlider;
        public Slider sfxVolumeSlider;

        [Header("Panels")]
        public GameObject overrideSavePanel;
        public GameObject quitConfirmationPanel;
        public GameObject settingsPanel;

        [Header("Audio")]
        public AudioMixer audioMixer;

        private void Start()
        {
            versionText.text = PlayerInputManager.instance.currentVersion;

            LoadSaveSlot();          

            //INITIALIZE PLAYER SETTINGS IF NOT SET BEFORE (RAN ONLY FIRST TIME PLAYING)
            if (!PlayerPrefs.HasKey("IsInitialized"))
            {
                PlayerPrefs.SetFloat("MasterVolume", .85f);
                PlayerPrefs.SetFloat("MusicVolume", .65f);
                PlayerPrefs.SetFloat("SFXVolume", .8f);

                PlayerPrefs.SetInt("IsInitialized", 1);
                PlayerPrefs.Save();
            }

            LoadVolume();

            //MENU MUSIC
            WorldSoundEffectsManager.instance.PlayMusic("Otopor", 0.25f, 1);
        }

        private void Update()
        {
            if (deleteSettings)
            {
                deleteSettings = false;
                PlayerPrefs.DeleteAll();
            }
        }

        public void PlayOpenPopUpSFX()
        {
            WorldSoundEffectsManager.instance.PlaySoundFXWithStacking(WorldSoundEffectsManager.instance.popUpOpenUISFX);
        }

        public void PlayClosePopUpSFX()
        {
            WorldSoundEffectsManager.instance.PlaySoundFXWithStacking(WorldSoundEffectsManager.instance.popUpCloseUISFX);
        }

        public void PlaySelectButtonSFX()
        {
            WorldSoundEffectsManager.instance.PlaySoundFX(WorldSoundEffectsManager.instance.selectButtonUISFX);
        }

        public void PlayClickButtonSFX()
        {
            WorldSoundEffectsManager.instance.PlaySoundFX(WorldSoundEffectsManager.instance.clickButtonUISFX);
        }

        public void UpdateMusicVolume(float volume)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        }

        public void UpdateSFXVolume(float volume)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        }

        public void UpdateMasterVolume(float volume)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }

        public void SaveVolume()
        {
            PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
            PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);

            PlayerPrefs.Save();
        }

        public void LoadVolume()
        {
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }

        public void OpenSettingsMenu()
        {
            mainMenuButtons.SetActive(false);
            settingsPanel.SetActive(true);

            PlayOpenPopUpSFX();
        }

        public void NewGameButtonEvent()
        {
            if (WorldSaveGameManager.instance.hasSaveFile)
            {
                overrideSavePanel.SetActive(true);
                mainMenuButtons.SetActive(false);

                PlayOpenPopUpSFX();
            }
            else
            {
                StartNewGame();
            }
        }

        public void StartNewGame()
        {
            WorldSaveGameManager.instance.CreateNewGame();
            StartCoroutine(WorldSaveGameManager.instance.LoadWorldScene());
        }

        public void LoadGame()
        {
            WorldSaveGameManager.instance.LoadGame();
        }

        public void LoadSaveSlot()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

            saveFileDataWriter.saveFileName = "characterSave01";

            if (saveFileDataWriter.CheckToSeeIfFileExists())
            {
                continueGameButton.gameObject.SetActive(true);
                WorldSaveGameManager.instance.hasSaveFile = true;
            }
            else
            {
                continueGameButton.gameObject.SetActive(false);
                WorldSaveGameManager.instance.hasSaveFile = false;
            }
        }

        public void AttemptToQuitGame()
        {
            quitConfirmationPanel.SetActive(true);
            mainMenuButtons.SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
            //UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}