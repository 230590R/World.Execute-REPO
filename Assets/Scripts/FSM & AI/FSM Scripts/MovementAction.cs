using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Movement")]
public class MovementAction : IAction {

  public enum TYPE {
    FLYING,
    WALKING
  }
  public TYPE type; 

  public override void Act(StateMachine controller) {
    switch (type) {
      case TYPE.FLYING:
        //if (controller.usingWaypoints) FlyToWaypoint(controller);
        //else FlyToTarget(controller);\

        FlyToWaypoint(controller);
        break;

      default:
      case TYPE.WALKING:
        WalkToTarget(controller);
        break;
    }


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
    Vector2 force = dir * controller.stats.movementSpeed;
    controller.m_Rigidbody2D.AddForce(force, ForceMode2D.Force);

    float distance = Vector2.Distance(controller.m_Rigidbody2D.position, controller.path.vectorPath[currentWaypoint]);
    if (distance < controller.pathWaypointThreshold) {
      controller.nextPathWaypoint++;
    }

    Debug.Log("TEST");
  }

  private void FlyToTarget(StateMachine controller) {
    // apply
    controller.pathDone = true;

    Vector2 dir = (controller.Target.position - controller.transform.position).normalized;
    Vector2 force = dir * controller.stats.movementSpeed;
    controller.m_Rigidbody2D.AddForce(force, ForceMode2D.Force);

  }


  private void WalkToTarget(StateMachine controller) {
    // apply
    if (controller.path == null) return;

    float distance = controller.Target.position.x - controller.transform.position.x;

    if (Mathf.Abs(distance) < controller.pathWaypointThreshold) {
      controller.pathDone = true;
      return;
    }
    else controller.pathDone = false;


    float xVel = (distance > 0 ? 1f : -1f) * controller.stats.movementSpeed;
    controller.m_Rigidbody2D.velocity = new Vector2(xVel, controller.m_Rigidbody2D.velocity.y);
  }




  public override void Enter(StateMachine controller) {

  }

  public override void Exit(StateMachine controller) {

  }
}