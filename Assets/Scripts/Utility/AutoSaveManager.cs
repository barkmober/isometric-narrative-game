using UnityEngine;

namespace SA
{
    public class AutoSaveManager : MonoBehaviour
    {
        public static AutoSaveManager instance;

        [Header("Auto-Save")]
        public float saveTick = 15;
        public float saveTimer = 0;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (saveTimer < saveTick)
            {
                saveTimer += Time.deltaTime;
            }
            else
            {
                WorldSaveGameManager.instance.SaveGame();
            }
        }

        public void ResetSaveTick()
        {
            saveTimer = 0;
        }
    }
}