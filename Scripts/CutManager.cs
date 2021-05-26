
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutManager : MonoBehaviour
{

    [SerializeField] private GameObject next;
    [SerializeField] private GameObject loading;

    private bool spacePressed;
    
    private void Start()
    {
        this.gameObject.SetActive(true);
        spacePressed = false;
    }

    public void MoveToNext ()
    {
        next.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void MoveToGame()
    {
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("Level 1");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
                GameObject sounds = GameObject.FindWithTag("sound");
                sounds.SetActive(false);
                this.gameObject.SetActive(false);
                loading.SetActive(true);
                SceneManager.LoadScene("Level 1");
        }
    }
}
