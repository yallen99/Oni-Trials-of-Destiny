using UnityEngine;

namespace CameraScripts
{
    public class CameraFollow : MonoBehaviour
    {

        public float cameraMoveSpeed = 120.0f;
        public GameObject cameraFollowObj;
        private float clampAngle = 80.0f;
        private float inputSensitivity = 150.0f;
        private float mouseX;
        private float mouseY;
        private float finalInputX;
        private float finalInputZ;
        private float _rotY;
        private float _rotX;
        [HideInInspector]public bool isAnimating;


        void Start()
        {
            Vector3 rot = transform.localRotation.eulerAngles;
            _rotY = rot.y;
            _rotX = rot.x;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        void Update()
        {
            if (!isAnimating)
            {
                mouseX = Input.GetAxis("Mouse X");
                mouseY = Input.GetAxis("Mouse Y");
                finalInputX = mouseX;
                finalInputZ = mouseY;

                _rotY += finalInputX * inputSensitivity * Time.deltaTime;
                _rotX += finalInputZ * inputSensitivity * Time.deltaTime;

                _rotX = Mathf.Clamp(_rotX, -clampAngle, clampAngle);

                Quaternion localRotation = Quaternion.Euler(_rotX, _rotY, 0.0f);
                transform.rotation = localRotation;
            }
        }


        private void LateUpdate()
        {
            if (!isAnimating)
            {
                CameraUpdater();
            }
        }


        void CameraUpdater()
        {
            //sets target object to follow
            Transform target = cameraFollowObj.transform;

            //move towards game object that is target
            float step = cameraMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}
