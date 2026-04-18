using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class WorldCharacterEffectsManager : MonoBehaviour
    {
        public static WorldCharacterEffectsManager instance;

        [Header("FX")]
        public GameObject jumpingDustVFX;
        public GameObject landingDustVFX;
        public GameObject dustFootstepVFX;

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
    }
}