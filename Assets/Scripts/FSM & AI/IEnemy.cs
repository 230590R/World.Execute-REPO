using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemy : MonoBehaviour {
  public StateMachine m_StateMachine;


  // private variables
  protected string currentState;

    // inherit
    protected Stats stats;
    protected HealthController m_HealthController;
    protected Animator m_Animator;
    protected SpriteRenderer m_SpriteRenderer;
    protected Dictionary<string, IAttackController> m_Attacks;
    protected Transform Player;
    protected Transform Target;

  protected void Init() {
    stats = m_StateMachine.stats;
    m_HealthController = m_StateMachine.m_HealthController;
    m_Attacks = m_StateMachine.m_Attacks;
    Player = m_StateMachine.Player;
    Target = m_StateMachine.Target;
    m_Animator = m_StateMachine.m_Animator;
    m_SpriteRenderer = m_StateMachine.m_SpriteRenderer;
        m_HealthController.SetMaxHealth(stats.maxHp);
        m_HealthController.health = stats.maxHp;
  }

}
