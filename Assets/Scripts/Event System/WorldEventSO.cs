using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SA
{
    [CreateAssetMenu(menuName = "Data/Events/World Event")]
    public class WorldEventSO : ScriptableObject
    {
        public int eventID;

        public void AddEventToList(PlayerManager player)
        {
            player.allFiredEvents.Add(this);
        }
    }
}