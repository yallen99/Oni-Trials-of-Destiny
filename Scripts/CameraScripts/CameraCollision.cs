﻿using UnityEngine;

namespace CameraScripts
{
    public class CameraCollision : MonoBehaviour
    {

        public float minDistance = 1.0f;
        public float maxDistance = 4.0f;
        public float smooth = 10.0f;
        Vector3 _dollyDir;
        public Vector3 dollyDirAdjusted;
        public float distance;


        void Awake()
        {
            var localPosition = transform.localPosition;
            _dollyDir = localPosition.normalized;
            distance = localPosition.magnitude;
        }

    
        void Update()
        {
            Vector3 desiredCameraPos = transform.parent.TransformPoint(_dollyDir * maxDistance);
            RaycastHit hit;

            if (Physics.Linecast (transform.parent.position, desiredCameraPos, out hit))
            {
                distance = Mathf.Clamp ((hit.distance * 0.9f), minDistance, maxDistance);
            }
            else
            {
                distance = maxDistance;
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, _dollyDir * distance, Time.deltaTime * smooth);
        }
    }
}
