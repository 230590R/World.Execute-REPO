using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject {
  public string stateName;
  public IAction[] actions;
  public Transition[] transitions;




  public void UpdateState(StateMachine controller) {
    DoActions(controller);
    CheckTransitions(controller);
  }

  private void DoActions(StateMachine controller) {
    for (int i = 0; i < actions.Length; i++) {
      actions[i].Act(controller);
    }
  }

  private void CheckTransitions(StateMachine controller) {
    for (int i = 0; i < transitions.Length; i++) {
      bool decisionSucceeded = transitions[i].decision.Decide(controller);

      if (decisionSucceeded) {
        controller.currentState.Exit(controller);
        controller.TransitionToState(transitions[i].trueState);
        transitions[i].trueState.Enter(controller);
      }
      else {
        controller.TransitionToState(transitions[i].falseState);
      }

    }
  }



  public void Enter(StateMachine controller) {
    for (int i = 0; i < actions.Length; i++) {
      actions[i].Enter(controller);
    }
  }

  public void Exit(StateMachine controller) {
    for (int i = 0; i < actions.Length; i++) {
      actions[i].Exit(controller);
    }
  }

}
