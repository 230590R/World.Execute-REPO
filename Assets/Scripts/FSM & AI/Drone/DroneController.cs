using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : IEnemy
{
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        currentState = m_StateMachine.currentState.stateName;


    }
}
