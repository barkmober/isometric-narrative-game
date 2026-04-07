using UnityEngine;

namespace SA
{
    public class PlayerSoundFXManager : CharacterSoundFXManager
    {
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        protected virtual void Start()
        {
            base.Start();
        }

        protected virtual void Update()
        {
            base.Update();
        }
    }
}