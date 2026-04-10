using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SA
{
    public class EnableAndDisableGameobjectsEvent : WorldEvent
    {
        [Header("GameObjects")]
        [SerializeField] List<GameObject> objectsToEnable;
        [SerializeField] List<GameObject> objectsToDisable;

        protected override void Start()
        {
            base.Start();
        }

        public override void FireEvent(PlayerManager player)
        {
            base.FireEvent(player);

            foreach(var obj in objectsToEnable)
            {
                obj?.SetActive(true);
            }

            foreach (var obj in objectsToDisable)
            {
                obj?.SetActive(false);
            }
        }

        public override void FireEventOnLoad(PlayerManager player)
        {
            base.FireEventOnLoad(player);

            foreach (var obj in objectsToEnable)
            {
                obj.SetActive(true);
            }

            foreach (var obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
        }
    }
}