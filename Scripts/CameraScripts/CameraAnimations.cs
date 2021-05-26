using UnityEngine;

namespace CameraScripts
{
    public class CameraAnimations : MonoBehaviour
    {
        private CharacterController _player;
        private CameraFollow _cameraMovement;

        
        // Start is called before the first frame update
        void Start()
        {
            _player = FindObjectOfType<CharacterController>();
            _cameraMovement = FindObjectOfType<CameraFollow>();
        }


        public void DisableMovement()
        {
            _cameraMovement.isAnimating = true;
            _player.enabled = false;
        }

        public void EnableMovement()
        {
            _cameraMovement.isAnimating = false;
            _player.enabled = true;
        }

        public void ChangeCameras(Camera mainCam, Camera secCam)
        {
            mainCam.GetComponent<Camera>().enabled = false;
            secCam.GetComponent<Camera>().enabled = true;
           
        }

        public void AnimateCamera(Camera cam, bool shouldBeAnimating)
        {
            cam.GetComponent<Animator>().enabled = shouldBeAnimating;
        }
        
    }
}
