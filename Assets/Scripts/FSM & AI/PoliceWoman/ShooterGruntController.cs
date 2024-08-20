using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterGruntController : IEnemy
{
    // Start is called before the first frame update

    bool reloadFlag = false;
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
            m_Animator.SetBool("isWalking", false);

        if (currentState == "Chase")
        {
            m_Animator.SetBool("isRunning", true);
            m_StateMachine.movementMultiplier = 1.5f;
        }
        else
            m_Animator.SetBool("isRunning", false);

        if (currentState == "Reload")
        {
            if (reloadFlag)
            {

                m_Animator.SetTrigger("reload");
                reloadFlag = false;
            }
        }
        else
        {
            reloadFlag = true;
        }
        
    }
}
