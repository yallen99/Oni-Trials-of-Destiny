using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    AudioSource[] audio;
    MenuManager menuManager;
    Slider slider;
    private float volume = 1f;
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>().GetComponent<MenuManager>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this);
        InitializeAudio();
      
    }
    private void InitializeAudio()
    {
        audio = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audio)
        {
            audioSource.GetComponent<AudioSource>().volume = volume;
        }
    }

    private void Update()
    {
        if (menuManager.settings)
        {
            slider = FindObjectOfType<Slider>();
            slider.GetComponent<Slider>().value = volume;
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      
        InitializeAudio();
    }


    public void SetVolume()
    {
        volume = slider.value;
        foreach (AudioSource audioSource in audio)
        {
            audioSource.GetComponent<AudioSource>().volume = slider.value;
        }
    }

}