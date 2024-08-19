using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack/ChainAttack")]
public class ChainAttackAction : IAction {

  [Serializable] public class AttackChain {
    public string atkName;
    public float atkLeadupTime;
    public float parriableDuration;
  }
  public AttackChain[] ChainAttacks;

  public override void Act(StateMachine controller) {
    int index = controller.atkIndex;
    if (index < 0) return;
    if (index >= ChainAttacks.Length) { controller.atkIndex = -1; return; }

    var atkController = controller.GetAttackController(ChainAttacks[index].atkName);


    // get the duration elapsed before attack
    float atkTimestamp = 0;
    for (int i = 0; i <= index; i++) {
      atkTimestamp += ChainAttacks[index].atkLeadupTime;
    }
    for (int i = 0; i < index; i++) {
      atkTimestamp += atkController.delay;
    }
    float switchTimestamp = atkTimestamp + atkController.delay;

    //Debug.Log(string.Format("atk: {0}, switch: {1}", atkTimestamp, switchTimestamp));

    float parryTimestamp = switchTimestamp - ChainAttacks[index].parriableDuration;
    if (controller.elapsedTime > parryTimestamp) {
      ChargeUpAttack(controller, ChainAttacks[index].atkName);
    }

    //ChargeUpAttack(controller, ChainAttacks[index].atkName);
    // attack
    if (controller.atkBuffer) {
      Attack(controller, ChainAttacks[index].atkName);
      controller.atkBuffer = false;
      //Debug.Log(string.Format("atk: {0}, elapsed: {1}", atkTimestamp, controller.elapsedTime));
    }

    if (controller.elapsedTime > atkTimestamp && controller.atkSwitchBuffer) {
      controller.atkBuffer = true;
      controller.atkSwitchBuffer = false;
    }

    if (controller.elapsedTime > switchTimestamp) {
      controller.atkIndex++;
      controller.atkSwitchBuffer = true;
    }


  }

  private void Attack(StateMachine controller, string atkName) {
    var atkController = controller.GetAttackController(atkName);
    if (atkController == null) return;
    Vector2 dir = (controller.AimedTarget.position - atkController.transform.position).normalized;
    atkController.Attack(dir);
  }
  private void ChargeUpAttack(StateMachine controller, string atkName) {
    var atkController = controller.GetAttackController(atkName);
    if (atkController == null) return;
    Vector2 dir = (controller.AimedTarget.position - atkController.transform.position).normalized;
    atkController.ChargeUp(dir);
  }


  public override void Enter(StateMachine controller) {
    controller.atkBuffer = false;
    controller.atkIndex = 0;
    controller.atkSwitchBuffer = true;
  }



  public override void Exit(StateMachine controller) {

  }


}
