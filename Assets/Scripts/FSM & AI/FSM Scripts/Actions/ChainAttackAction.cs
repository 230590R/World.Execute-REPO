using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack/ChainAttack")]
public class ChainAttackAction : IAction {

  [Serializable] public class AttackChain {
    public string atkName;
    public float atkDuration;

  }
  public AttackChain[] ChainAttacks;

  public override void Act(StateMachine controller) {
    int index = controller.atkIndex;
    if (index < 0) return;
    if (index >= ChainAttacks.Length) return;

    // attack
    if (controller.atkBuffer) {
      Attack(controller, ChainAttacks[index].atkName);
      controller.atkBuffer = false;
    }

    // get the duration elapsed before attack
    float timestamp = 0;
    for (int i = 0; i < index; i++) {
      timestamp += ChainAttacks[index].atkDuration;
    }

    if (controller.elapsedTime > timestamp) {
      controller.atkBuffer = true;
      controller.atkIndex++;
    }


  }

  private void Attack(StateMachine controller, string atkName) {
    var atkController = controller.GetAttackController(atkName);
    if (atkController == null) return;
    Vector2 dir = (controller.AimedTarget.position - atkController.transform.position).normalized;
    atkController.Attack(dir);
  }



  public override void Enter(StateMachine controller) {
    controller.atkBuffer = true;
    controller.atkIndex = 0;
  }



  public override void Exit(StateMachine controller) {

  }


}
