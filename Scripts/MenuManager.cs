using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject buttons;
    [SerializeField] private new GameObject camera;

    private Animator _optionsAnimator;
    private Animator _buttonsAnimator;
    private Animator _cameraAnimator;

    private bool _intro;
    private bool _fullscreen;
    public bool settings;
    
    private static readonly int ToGame = Animator.StringToHash("goToGame");
    private static readonly int ShowMenu = Animator.StringToHash("showMenu");
    private static readonly int ReturnToMenu = Animator.StringToHash("returnToMenu");
    private static readonly int ButtonsIn = Animator.StringToHash("buttonsIn");
    private static readonly int ButtonsOut = Animator.StringToHash("buttonsOut");
    private static readonly int OptionsOut = Animator.StringToHash("optionsOut");
    private static readonly int OptionsIn = Animator.StringToHash("optionsIn");


    private void Start()
    {
      _optionsAnimator = options.GetComponent<Animator>();
      _buttonsAnimator = buttons.GetComponent<Animator>();
      _cameraAnimator = camera.GetComponent<Animator>();
      
      //init
      _cameraAnimator.SetBool(ToGame, false);
      _cameraAnimator.SetBool(ShowMenu, false);
      _cameraAnimator.SetBool(ReturnToMenu, true);

      _intro = true;
      settings = false;
    }
    

    public void StartCoroutineOpenOptions()
    {
        StartCoroutine(OpenOptions());
    }
    public void StartCoroutineCloseOptions()
    {
        StartCoroutine(CloseOptions());
    }
    public void StartCoroutineToGame()
    {
        StartCoroutine(GoToGame());
    }

    //coroutines
    private IEnumerator GoToGame()
    {
        _cameraAnimator.SetBool(ReturnToMenu, false);
        _cameraAnimator.SetBool(ShowMenu, false);
        _cameraAnimator.SetBool(ToGame, true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Cutscene");
        _cameraAnimator.SetBool(ToGame, false);
        
        
    }
    private IEnumerator OpenOptions()
    {
        _buttonsAnimator.SetBool(ButtonsIn, false);
        _buttonsAnimator.SetBool(ButtonsOut, true);
        yield return new WaitForSeconds(1);
        _buttonsAnimator.SetBool(OptionsOut, false);
        _optionsAnimator.SetBool(OptionsIn, true);
        settings = true;
    }
    private IEnumerator CloseOptions()
    {
       _optionsAnimator.SetBool(OptionsIn, false);
       _optionsAnimator.SetBool(OptionsOut, true);
   
       yield return new WaitForSeconds(1);
       _buttonsAnimator.SetBool(ButtonsOut, false);
       _buttonsAnimator.SetBool(ButtonsIn, true);
       _optionsAnimator.SetBool(OptionsOut, false);
       settings = false;
    }

    private void Update()
    {
        if (_intro)
        {
            if (Input.anyKeyDown)
            {
                _intro = false;
                _cameraAnimator.SetBool(ShowMenu, true);
            }
        }
    }


    /*public void OpenExit()
    {
        exit.SetActive(true);
    }
        public void CloseExit()
    {
        exit.SetActive(false);
    }*/
    
    public void QuitGame()
    {
        Application.Quit();
    }

    #region Options Menu
    public void Low()
    {
        if (_fullscreen == true)
        {
            Screen.SetResolution(480, 270, true);
        }
        else if (_fullscreen == false)
        {
            Screen.SetResolution(480, 270, false);
        }

    }
    public void Medium()
    {
        if (_fullscreen == true)
        {
            Screen.SetResolution(960, 540, true);
        }
        else if (_fullscreen == false)
        {
            Screen.SetResolution(960, 540, false);
        }

    }
    public void High()
    {
        if (_fullscreen == true)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if (_fullscreen == false)
        {
            Screen.SetResolution(1920, 1080, false);
        }

    }

    public void Fullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log(isFullscreen);
    }

    private void CheckFullScreen()
    {
        if (Screen.fullScreen == true)
        {
            _fullscreen = true;

        }

        else if (Screen.fullScreen == false)
        {
            _fullscreen = false;
        }

    }
    #endregion
}
