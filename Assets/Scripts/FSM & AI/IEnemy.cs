using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemy : MonoBehaviour {
  public StateMachine m_StateMachine;


  // private variables
  private string currentState;

  // inherit
  private Stats stats;
  private HealthController m_HealthController;
  private Dictionary<string, IAttackController> m_Attacks;
  private Transform Player;
  private Transform Target;

  protected void Init() {
    stats = m_StateMachine.stats;
    m_HealthController = m_StateMachine.m_HealthController;
    m_Attacks = m_StateMachine.m_Attacks;
    Player = m_StateMachine.Player;
    Target = m_StateMachine.Target;
  }

}
