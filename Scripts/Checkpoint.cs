using System;
using System.Collections;
using System.Collections.Generic;
using Player_Scripts;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 _lastCheckpointPos;
    private PlayerController _playerController;
    private int playerLives;
    [SerializeField] private GameObject particles;


    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        FindObjectOfType<CheckPointController>().lastCheckPointPos = this.transform.position + new Vector3(0,2,0);
        gameObject.transform.Find("Moon Sigils").GetComponent<MeshRenderer>().material.color = Color.cyan;
        gameObject.transform.Find("MoonDial").GetComponent<MeshRenderer>().material.color = Color.cyan;
        gameObject.transform.Find("SummoningDisc").GetComponent<MeshRenderer>().material.color = Color.cyan;
        particles.SetActive(true);
        
    }
}
