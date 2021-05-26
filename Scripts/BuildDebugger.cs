using System;
using System.Collections;
using Player_Scripts;
using TMPro;
using UnityEngine;

public class BuildDebugger:MonoBehaviour
{
        [SerializeField] private TextMeshProUGUI mouse;
        [SerializeField] private TextMeshProUGUI grounded;
        [SerializeField] private TextMeshProUGUI lastPos;
        [SerializeField] private TextMeshProUGUI fps;
        private int frames;

        private void Start()
        {
                StartCoroutine(FPS());

        }

        private void Update()
        {
                mouse.text = "Mouse is " + Cursor.lockState.ToString();
                /*grounded.text = "Is Grounded = " +
                                FindObjectOfType<PlayerController>().GetComponent<CharacterController>().isGrounded;*/
                grounded.text = "Is Grounded  " + FindObjectOfType<PlayerController>().IsGrounded();
                lastPos.text = "last ckp pos = " + FindObjectOfType<CheckPointController>().lastCheckPointPos;
        }

       
        private IEnumerator FPS()
        {
                frames = (int) (1.0f / Time.deltaTime);
                fps.text = "FPS " + frames.ToString();
                yield return new WaitForSeconds(1);
                StartCoroutine(FPS());
        }
}