using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SA
{
    public class OnTriggerEvent : MonoBehaviour
    {
        [SerializeField] List<WorldEvent> eventsToFire;
        Collider collider;

        private void Awake()
        {
            collider = GetComponent<Collider>();
            collider.enabled = false;
        }

        private void Start()
        {
            Invoke("EnableCollider", 5);
        }

        private void EnableCollider()
        {
            collider.enabled = true;
        }

        protected void OnTriggerEnter(Collider other)
        {
            PlayerManager player = other.gameObject.GetComponent<PlayerManager>();

            if(player != null )
            {
                foreach (var obj in eventsToFire)
                {
                    obj.FireEvent(player);    
                }

                collider.enabled = false;
            }
        }
    }
}