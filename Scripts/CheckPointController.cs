using System;
using System.Collections;
using System.Collections.Generic;
using Player_Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointController : MonoBehaviour
{
    private static CheckPointController instance;
    public Vector3 lastCheckPointPos;
    private PlayerController _playerController;

    private SceneLoader _sceneLoader;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }else{
            Destroy(gameObject);
        }
   
    }
    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _playerController = FindObjectOfType<PlayerController>();
    }

    public void Update()
    {
        if (_playerController.ShouldRespawn() )
        {
            if (_playerController.playerHealthSystem._lives == 0)
            {
                _sceneLoader.GameOver();
            }
            else
            {
                _playerController.gameObject.transform.position = lastCheckPointPos;
                _playerController.playerHealthSystem.Reset();
            }
        }
        
    }

}
