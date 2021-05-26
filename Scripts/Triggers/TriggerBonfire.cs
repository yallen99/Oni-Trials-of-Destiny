using Health_Scripts;
using Player_Scripts;
using UnityEngine;

namespace Triggers
{
    public class TriggerBonfire : MonoBehaviour
    {
        [SerializeField] private GameObject fire;

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerController>();
            var playerHealth = other.GetComponent<HealthSystem>();
            if (player)
            {
                fire.SetActive(true);
                playerHealth.Heal(30);
            }
        }
    }
}
