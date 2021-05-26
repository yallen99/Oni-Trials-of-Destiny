using System;
using System.Collections;
using CameraScripts;
using Player_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Triggers
{
    public class TriggerCutscene : MonoBehaviour
    {

        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera secCamera;
        [SerializeField] private Animator targetAnimator;
        [SerializeField] private  String triggerParamName;
        [SerializeField] private int timeToWait;
        private CameraAnimations _cameraAnimations;
        private bool collided;


        private void Start()
        {
            collided = false;
            _cameraAnimations = FindObjectOfType<CameraAnimations>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var playerCollider = other.GetComponent<PlayerController>();
            if (playerCollider && !collided)
            {
                StartCoroutine(CameraAnimation());
                
                collided = true;
            }
        }

        private IEnumerator CameraAnimation()
        {
            yield return new WaitForSeconds(2);
           _cameraAnimations.DisableMovement();
           _cameraAnimations.ChangeCameras(mainCamera, secCamera);
          
           if (CheckForAnimatorComponent())
           {
               _cameraAnimations.AnimateCamera(secCamera, true);
           }

           targetAnimator.SetTrigger(triggerParamName);
            yield return new WaitForSeconds(timeToWait);
            _cameraAnimations.ChangeCameras(secCamera, mainCamera);
            if (CheckForAnimatorComponent())
            {
                _cameraAnimations.AnimateCamera(secCamera, false);
            }
            _cameraAnimations.EnableMovement();
        }
        

        private bool CheckForAnimatorComponent() => secCamera.GetComponent<Animator>();
    }
}
