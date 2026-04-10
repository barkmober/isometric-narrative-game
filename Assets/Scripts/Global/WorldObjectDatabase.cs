using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SA
{
    public class WorldObjectDatabase : MonoBehaviour
    {
        public static WorldObjectDatabase instance;

        [Header("EVENTS")]
        public List<WorldEventSO> allWorldEvents;

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

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            AssignIDsToEvents();
        }

        private void AssignIDsToEvents()
        {
            for (int i = 0; i < allWorldEvents.Count; i++)
            {
                allWorldEvents[i].eventID = i + 1;
            }
        }

        public WorldEventSO GetWorldEventByID(int ID)
        {
            return allWorldEvents.FirstOrDefault(_event => _event.eventID == ID);
        }

        public WorldEventSO GetEventFromSerializedData(SerializableWorldEvent _event)
        {
            WorldEventSO worldEvent = null;

            if (GetWorldEventByID(_event.eventID))
                worldEvent = Instantiate(GetWorldEventByID(_event.eventID));

            return worldEvent;
        }
    }
}