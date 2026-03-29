using UnityEngine;

namespace SA
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            playerLocomotionManager.HandleAllMovement();
        }
    }
}