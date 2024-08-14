using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack/Charge Up")]
public class ChargeUpAttack : IAction {
  public string atkName;

  public override void Act(StateMachine controller) {
    ChargeUp(controller);
  }

  private void ChargeUp(StateMachine controller) {
    var atkController = controller.GetAttackController(atkName);
    if (atkController == null) return;

    // get the direction towards the target
    Vector2 dir = (controller.AimedTarget.position - atkController.transform.position).normalized;
    atkController.ChargeUp(dir);
  }



  public override void Enter(StateMachine controller) {

  }
  public override void Exit(StateMachine controller) {

  }
}
