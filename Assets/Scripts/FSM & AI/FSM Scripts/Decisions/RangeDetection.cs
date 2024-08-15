using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/PlayerRange")]
public class RangeDetection : IDecision
{
    public enum COMPARATOR {
        LESS,
        GREATER,
        EQUAL
    }
    public COMPARATOR comparator;

    public float detectDistance = -1;

    public override bool Decide(StateMachine controller)
    {
        float distThreshold = (detectDistance < 0) ? controller.stats.detectionRange : detectDistance;
        float distance = Vector2.Distance(controller.Player.position, controller.transform.position);
        switch (comparator)
        {
            case COMPARATOR.EQUAL:
                return (distance == distThreshold);
            case COMPARATOR.GREATER:
                return (distance >= distThreshold);
            default:
            case COMPARATOR.LESS:
                return (distance <= distThreshold);
        }
    }


}
