using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SA
{
    public class WorldEvent : MonoBehaviour
    {
        public WorldEventSO worldEvent;
        public int eventID;

        public GameObject disableOnStart;

        protected virtual void Start()
        {
            WorldEventManager.instance.AddEventToList(this);
            eventID = worldEvent.eventID;
        }

        public virtual void FireEvent(PlayerManager player)
        {
            worldEvent.AddEventToList(player);   

            WorldSaveGameManager.instance.SaveGame();
        }

        public virtual void FireEventOnLoad(PlayerManager player)
        {
            
        }
    }
}