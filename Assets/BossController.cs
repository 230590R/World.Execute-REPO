using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BossController : IEnemy {
  // Start is called before the first frame update
  private AudioHandlerV2 audioHandler;
  private string categoryName = "Enemy";
  private float previousHealth;

  [SerializeField] State enterCombatState;
  [SerializeField] NPCDialougeTrigger myDialogueTrigger;


  void Start() {
    Init();
    previousHealth = m_HealthController.health;
    //audioHandler = FindObjectOfType<AudioHandlerV2>();
  }

  // Update is called once per frame
  void Update() {



    if (m_HealthController.health <= 0) {
      m_Animator.SetBool("isAlive", false);
      m_StateMachine.enabled = false;
      return;
    }



    if (m_HealthController.health < previousHealth) {
      m_Animator.SetTrigger("hurt");
      previousHealth = m_HealthController.health;
    }



    currentState = m_StateMachine.currentState.stateName;


    m_Animator.SetBool("isRunning", false);
    if (currentState == "Chase") {
      m_Animator.SetBool("isRunning", true);
      m_StateMachine.movementMultiplier = stats.runMultiplier;
    }





    m_StateMachine.movementMultiplier = 1f;


    if (currentState == "Patrol") {
      m_Animator.SetBool("isWalking", true);
    }
    else {
      m_Animator.SetBool("isWalking", false);
    }


    if (m_HealthController.health <= 0) {
      m_Animator.SetBool("isAlive", false);
    }
  }


  public void StartCombat() {
    if (!myDialogueTrigger.wasInDialogue) return;
    m_StateMachine.currentState = enterCombatState;
    myDialogueTrigger.enabled = false;
  }
}
