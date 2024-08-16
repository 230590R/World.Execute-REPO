using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/PathDone")]
public class PathDone : IDecision
{
    public override bool Decide(StateMachine controller)
    {
        return controller.pathDone;
    }
}
