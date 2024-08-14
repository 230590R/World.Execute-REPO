using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/FlyingMovement")]
public class FlyAction : IAction {
  public override void Act(StateMachine controller) {
    if (controller.usingWaypoints) FlyToWaypoint(controller);
    else FlyToTarget(controller);
  }

  private void FlyToWaypoint(StateMachine controller) {
    // apply
    if (controller.path == null) return;

    if (controller.nextPathWaypoint >= controller.path.vectorPath.Count) {
      controller.pathDone = true;
      return;
    }
    else controller.pathDone = false;

    int currentWaypoint = controller.nextPathWaypoint;
    Vector2 dir = (controller.path.vectorPath[currentWaypoint] - controller.transform.position).normalized;
    Vector2 force = dir * controller.stats.movementSpeed * Time.deltaTime;
    controller.m_Rigidbody2D.AddForce(force, ForceMode2D.Force);

    float distance = Vector2.Distance(controller.m_Rigidbody2D.position, controller.path.vectorPath[currentWaypoint]);
    if (distance < controller.pathWaypointThreshold) {
      controller.nextPathWaypoint++;
    }


    Debug.Log("targetting");
  }



  private void FlyToTarget(StateMachine controller) {
    // apply
    controller.pathDone = true;

    Vector2 dir = (controller.Target.position - controller.transform.position).normalized;
    Vector2 force = dir * controller.stats.movementSpeed * Time.deltaTime;
    controller.m_Rigidbody2D.AddForce(force, ForceMode2D.Force);

  }


  public override void Enter(StateMachine controller) {

  }

  public override void Exit(StateMachine controller) {

  }
}
