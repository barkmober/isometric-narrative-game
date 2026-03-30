using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SA
{
    public class TitleScreenManager : MonoBehaviour
    {
        SaveFileDataWriter saveFileDataWriter;

        [Header("Texts")]
        public TMP_Text versionText;

        [Header("Buttons")]
        public GameObject mainMenuButtons;
        public Button continueGameButton;
        public Button newGameButton;
        public Button optionsButton;
        public Button quitGameButton;

        [Header("Panels")]
        public GameObject overrideSavePanel;
        public GameObject quitConfirmationPanel;

        private void Start()
        {
            versionText.text = PlayerInputManager.instance.currentVersion;

            LoadSaveSlot();
        }

        public void PlayOpenPopUpSFX()
        {
            WorldSoundEffectsManager.instance.PlaySoundFX(WorldSoundEffectsManager.instance.popUpOpenUISFX);
        }

        public void PlayClosePopUpSFX()
        {
            WorldSoundEffectsManager.instance.PlaySoundFX(WorldSoundEffectsManager.instance.popUpCloseUISFX);
        }

        public void PlaySelectButtonSFX()
        {
            WorldSoundEffectsManager.instance.PlaySoundFX(WorldSoundEffectsManager.instance.selectButtonUISFX);
        }

        public void PlayClickButtonSFX()
        {
            WorldSoundEffectsManager.instance.PlaySoundFX(WorldSoundEffectsManager.instance.clickButtonUISFX);
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