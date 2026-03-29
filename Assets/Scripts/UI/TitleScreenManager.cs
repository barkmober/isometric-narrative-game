using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SA
{
    public class TitleScreenManager : MonoBehaviour
    {
        [Header("Texts")]
        public TMP_Text versionText;

        [Header("Buttons")]
        public GameObject mainMenuButtons;
        public Button continueGameButton;
        public Button newGameButton;
        public Button optionsButton;
        public Button quitGameButton;

        private void Start()
        {
            versionText.text = PlayerInputManager.instance.currentVersion;
        }

        public void StartNewGame()
        {
            StartCoroutine(WorldSaveGameManager.instance.LoadNewGame());
        }
    }
}