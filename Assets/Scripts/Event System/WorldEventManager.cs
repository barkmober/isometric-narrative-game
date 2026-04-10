using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace SA
{
    public class WorldEventManager : MonoBehaviour
    {
        public static WorldEventManager instance;

        [Header("Events")]
        [SerializeField] List<WorldEvent> worldEvents;

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
        }

        public void AddEventToList(WorldEvent _event)
        {
            if (worldEvents.Contains(_event))
                return;

            worldEvents.Add(_event);
        }

        public WorldEvent FindEventByID(int id)
        {
            foreach (var obj in worldEvents)
            {
                if (obj.eventID == id)
                {
                    return obj;
                }
            }

            return null;
        }

        public void FireEventByIDLoad(int id)
        {
            foreach (var obj in worldEvents)
            {
                if (obj.eventID == id)
                {
                    obj.FireEventOnLoad(WorldSaveGameManager.instance.player);
                    Debug.Log("WORLD EVENT RAN");
                }
            }
        }
    }
}