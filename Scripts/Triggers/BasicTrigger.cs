using System;
using Player_Scripts;
using UnityEngine;

namespace Triggers
{
    public class BasicTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject objectToSetActive;

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerController>();
            {
                if (player)
                {
                    objectToSetActive.SetActive(true);
                }
            }
        }
    }
}