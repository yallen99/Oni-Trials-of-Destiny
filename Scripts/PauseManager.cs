
using CameraScripts;
using Player_Scripts;
using TMPro;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private CharacterController player;
    [SerializeField] private GameObject pausePanel;
    private SceneLoader _sceneLoader;
    
    void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        player = FindObjectOfType<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            player.enabled = false;
            player.gameObject.GetComponent<Animator>().enabled = false;
            FindObjectOfType<CameraFollow>().GetComponent<CameraFollow>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        

    }
    
    public void Resume()
    {
        pausePanel.SetActive(false);
        player.enabled = true;
        player.gameObject.GetComponent<Animator>().enabled = true;
        FindObjectOfType<CameraFollow>().GetComponent<CameraFollow>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Menu()
    {
        _sceneLoader.LoadMenu();       
    }
    
   
}
