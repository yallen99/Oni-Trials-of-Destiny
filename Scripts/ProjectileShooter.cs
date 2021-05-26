
using System;
using System.Collections;
using Player_Scripts;
using UnityEngine;


public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] GameObject playerProjectilePrefab;
    [SerializeField] private GameObject[] spheres;
    private bool canAttack;
    private PlayerController _player;

    private void Start()
    {
        _player = GetComponent<PlayerController>();
        canAttack = true;
    }

    void Update()
    {
        if (_player != null)
        {
            //player attacks
            if (Input.GetMouseButtonDown(0) && _player.poweredUp && canAttack)
            {
                canAttack = false;
                foreach (var sphere in spheres)
                {
                    GameObject projectile = Instantiate(playerProjectilePrefab);
                    StartCoroutine(HideSpheres());
                    projectile.transform.position = sphere.transform.position;
                    Destroy(projectile, 0.8f);
                }
            }
        }
    }

    //prevent spam attacks and animate dropping spheres
    private IEnumerator HideSpheres(){ 
        foreach (GameObject obj in spheres)
        {
        obj.SetActive(false);
        }
        yield return new WaitForSeconds(0.9f);
        foreach (GameObject obj in spheres)
        {
            obj.SetActive(true);
        }

        canAttack = true;
    }
    
    //for enemy animations
    public void Shoot()
    {
        StartCoroutine(EnemyIsShooting());
      
    }

    private IEnumerator EnemyIsShooting()
    {
        yield return new WaitForSeconds(1);
    }
}

