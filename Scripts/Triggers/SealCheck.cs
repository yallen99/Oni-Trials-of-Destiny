using System;
using Player_Scripts;
using UnityEngine;

namespace Triggers
{
    public class SealCheck : MonoBehaviour
    {
        [SerializeField] private GameObject doors;

        private void Start()
        {
            doors.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerInventory>();
            {
                if (player.HasSeal())
                {
                    doors.SetActive(true);
                }
            }
        }
    }
}
