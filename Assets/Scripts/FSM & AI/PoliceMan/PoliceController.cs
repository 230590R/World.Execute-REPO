using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceController : IEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        Init();

    }

    // Update is called once per frame
    void Update()
    {
        currentState = m_StateMachine.currentState.stateName;

        m_StateMachine.movementMultiplier = 1f;
        if (currentState == "Patrol")
        {
            m_Animator.SetBool("isWalking", true);
        }
        else
        {
            m_Animator.SetBool("isWalking", false);
        }

        if (currentState == "Chase")
        {
            m_Animator.SetBool("isRunning", true);
            m_StateMachine.movementMultiplier = stats.runMultiplier;
        }
        else
        {
            m_Animator.SetBool("isRunning", false);
        }
    }
}
