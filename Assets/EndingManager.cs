using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
  [SerializeField] BossController boss;
  [SerializeField] Animator endingCutscene;

  bool flag = true;

  private void Update() {
    if (boss.m_StateMachine.m_HealthController.health <= 0) {
      if (flag) {
        endingCutscene.SetTrigger("end");
        flag = false;
      }
    }
  }
}
