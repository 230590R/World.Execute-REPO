using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack/Attack")]
public class AttackAction : IAction {
  public string atkName;
  
  public override void Act(StateMachine controller) {
    Attack(controller);
  }

  private void Attack(StateMachine controller) {



  }



  public override void Enter(StateMachine controller) {
    var atkController = controller.GetAttackController(atkName);
    if (atkController == null) return;
    Vector2 dir = (controller.AimedTarget.position - atkController.transform.position).normalized;
    atkController.Attack(dir);
  }
  public override void Exit(StateMachine controller) {

  }
}
