using UnityEngine;

namespace SA
{
    public class WorldUtilityManager : MonoBehaviour
    {
        public static WorldUtilityManager instance;

        [Header("")]
        [SerializeField] LayerMask characterLayer;
        [SerializeField] LayerMask damageableCharacterLayer;
        [SerializeField] LayerMask environmentLayer;

        private void Awake()
        {
            if (instance == null )
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        public LayerMask GetEnviroLayers()
        {
            return environmentLayer;
        }
    }
}