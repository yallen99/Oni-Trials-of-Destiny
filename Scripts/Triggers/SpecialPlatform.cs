using System;
using Player_Scripts;
using UnityEngine;

namespace Triggers
{
    

    public class SpecialPlatform : MonoBehaviour
    {
        Animator anim;
        [SerializeField] private string triggerName; 
        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger(triggerName);

            }
        }
    }
}
