using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health_Scripts;
using Player_Scripts;

public class FallDamage : MonoBehaviour
{

    public HealthSystem playerHealthSystem;
    public float startYPos;
    public float endYPos;
    public float damageThreshold = 10;
    public bool damageMe = false;
    public bool firstCall = true;
    private int damageValue;


    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindObjectOfType<CharacterController>().isGrounded)
        {
            if (gameObject.transform.position.y > startYPos)
            {
                firstCall = true;
            }
            if (firstCall)
            {
                startYPos = gameObject.transform.position.y;
                firstCall = false;
                damageMe = true;
            }
        }
        if (GameObject.FindObjectOfType<CharacterController>().isGrounded)
        {
            if (startYPos - endYPos > damageThreshold)
            {
                if (damageMe)
                {
                    damageValue = Mathf.RoundToInt (startYPos - endYPos - damageThreshold);
                    playerHealthSystem.GetComponent<HealthSystem>().Damage(damageValue);
                    damageMe = false;
                    firstCall = true;
                }
            }
        }
    }
}
