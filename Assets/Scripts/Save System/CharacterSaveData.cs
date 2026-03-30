using UnityEngine;

namespace SA
{
    [System.Serializable]
    public class CharacterSaveData
    {
        [Header("Coordinates")]
        public float xPosition;
        public float yPosition;
        public float zPosition;
        [Space]
        public float xRotation;
        public float yRotation;
        public float zRotation;
        [Space]
        public float xCameraPosition;
        public float yCameraPosition;
        public float zCameraPosition;
    }
}