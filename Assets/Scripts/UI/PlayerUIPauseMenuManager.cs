using UnityEngine;

namespace SA
{
    public class PlayerUIPauseMenuManager : MonoBehaviour
    {
        [Header("MENU")]
        public bool hasSettingsMenuOpen = false;
        [SerializeField] GameObject pauseMenu;
        [SerializeField] GameObject confirmQuitPopUp;
        [SerializeField] GameObject pauseOptions;
        [SerializeField] GameObject settingsMenu;

        //LOADING
        public void ResumeGame()
        {
            PlayerInputManager.instance.ResumeGame();
        }

        public void OpenConfirmReturnPopUp()
        {
            PlayOpenPopUpSFX();

            confirmQuitPopUp.SetActive(true);
            pauseOptions.SetActive(false);
        }

        public void CloseConfirmReturnPopUp()
        {
            PlayClosePopUpSFX();

            confirmQuitPopUp?.SetActive(false);
            pauseOptions?.SetActive(true);
        }
        
        public void QuitGameToMenu()
        {
            ResumeGame();

            confirmQuitPopUp?.SetActive(false);
            pauseOptions?.SetActive(true);

            hasSettingsMenuOpen = false;
            pauseOptions.SetActive(true);
            settingsMenu.SetActive(false);

            PlayerInputManager.instance.ReturnToMenu();
        }

        //SETTINGS
        public void OpenSettingsMenu()
        {
            hasSettingsMenuOpen = true;
            PlayOpenPopUpSFX();

            pauseOptions.SetActive(false);
            settingsMenu.SetActive(true);
        }

        public void CloseSettingsMenu()
        {
            hasSettingsMenuOpen = false;
            PlayClosePopUpSFX();

            pauseOptions.SetActive(true);
            settingsMenu.SetActive(false);
        }

        public void ActivatePauseMenu()
        {
            if(!pauseMenu.gameObject.activeSelf)
                PlayOpenPopUpSFX();

            pauseMenu.SetActive(true);
            pauseOptions.SetActive(true);
        }

        public void DeactivatePauseMenu()
        {
            if (pauseMenu.gameObject.activeSelf)
                PlayClosePopUpSFX();

            pauseMenu.SetActive(false);
            pauseOptions.SetActive(false);
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
            WorldSoundEffectsManager.instance.PlaySoundFXWithStacking(WorldSoundEffectsManager.instance.clickButtonUISFX);
        }
    }
}