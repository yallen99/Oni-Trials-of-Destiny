using System.Collections;
using System.Collections.Generic;
using Health_Scripts;
using Player_Scripts;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int playerDamage;
    [SerializeField] private int enemyDamage;
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        var player = other.GetComponent<PlayerController>();
        if (enemy)
        {
            enemy.GetComponent<HealthSystem>().Damage(playerDamage);
        }

        if (player)
        {
            player.GetComponent<HealthSystem>().Damage(enemyDamage);
        }
    }
}
