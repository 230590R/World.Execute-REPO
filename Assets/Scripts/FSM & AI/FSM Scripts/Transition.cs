using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition {
  public IDecision decision;
  public State trueState;
  public State falseState;
}
