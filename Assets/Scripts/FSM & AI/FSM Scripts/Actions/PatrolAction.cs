using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : IAction {

  public override void Act(StateMachine controller) {
    Patrol(controller);
  }

  private void Patrol(StateMachine controller) {
    //controller.UpdatePath();
    if (controller.waypoints.Length <= 0) return;

    if (controller.wp < 0) controller.wp = controller.waypoints.Length - 1;
    if (controller.wp >= controller.waypoints.Length) controller.wp = 0;

    if (controller.pathDone) {
      controller.pathDone = false;
      controller.Target = controller.waypoints[controller.wp];
      controller.wp++;
    }
  }


  public override void Enter(StateMachine controller) {
        if (controller.wp < 0) controller.wp = controller.waypoints.Length - 1;
        if (controller.wp >= controller.waypoints.Length) controller.wp = 0;

        controller.pathDone = false;
        controller.Target = controller.waypoints[controller.wp];
    }

  public override void Exit(StateMachine controller) {

  }



}
