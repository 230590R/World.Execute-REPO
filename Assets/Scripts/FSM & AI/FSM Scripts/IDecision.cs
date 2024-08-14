using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class IDecision : ScriptableObject {
  public abstract bool Decide(StateMachine controller);
}
