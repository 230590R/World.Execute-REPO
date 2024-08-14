using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAction : ScriptableObject {
  public abstract void Act(StateMachine controller);
  public abstract void Enter(StateMachine controller);
  public abstract void Exit(StateMachine controller);

}
