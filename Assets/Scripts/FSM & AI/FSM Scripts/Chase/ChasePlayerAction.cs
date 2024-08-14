using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase Player")]
public class ChasePlayerScript : IAction {


  public override void Act(StateMachine controller) {



    ChasePlayer(controller);
  }

  private void ChasePlayer(StateMachine controller) {
    //controller.usingWaypoints = false;
    controller.Target = controller.Player;
  }
  public override void Enter(StateMachine controller) {

  }
  public override void Exit(StateMachine controller) {

  }

  private void UpdatePlayerPath(StateMachine controller) {

  }

}
