using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [System.Serializable]

    public class SerializableWorldEvent : ISerializationCallbackReceiver
    {
        [SerializeField] public int eventID;

        public WorldEventSO GetEvent()
        {
            WorldEventSO _event = WorldObjectDatabase.instance.GetEventFromSerializedData(this);
            return _event;
        }

        public void OnAfterSerialize()
        {

        }

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {

        }
    }
}