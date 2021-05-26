using Health_Scripts;
using Player_Scripts;
using UnityEngine;

namespace Triggers
{
    public class WaterTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerController>();
        

            if (player)
            {
                player.GetComponent<HealthSystem>().Damage(500);
            }
        }
    }
}
