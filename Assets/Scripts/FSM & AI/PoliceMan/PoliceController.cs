using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceController : IEnemy
{
    // Start is called before the first frame update
    private AudioHandlerV2 audioHandler;
    private string categoryName = "Enemy";
    void Start()
    {
        Init();
        //audioHandler = FindObjectOfType<AudioHandlerV2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_HealthController.health <= 0)
        {
            m_Animator.SetBool("isAlive", false);
            m_StateMachine.enabled = false;
            return;
        }

        currentState = m_StateMachine.currentState.stateName;

        m_StateMachine.movementMultiplier = 1f;
        if (currentState == "Patrol")
        {
            m_Animator.SetBool("isWalking", true);
            //audioHandler.PlaySFXIfNotPlaying(categoryName, 0, transform);
        }
        else
        {
            m_Animator.SetBool("isWalking", false);
        }

        if (currentState == "Chase")
        {
            m_Animator.SetBool("isRunning", true);
            m_StateMachine.movementMultiplier = stats.runMultiplier;
            //audioHandler.PlaySFXIfNotPlaying(categoryName, 1, transform);
        }
        else
        {
            m_Animator.SetBool("isRunning", false);
        }

        if(currentState == "Idle"){
            //audioHandler.StopPlayingSFX(transform);
        }

        if(m_HealthController.health <= 0)
        {
            m_Animator.SetBool("isAlive", false);
        }
    }
}
