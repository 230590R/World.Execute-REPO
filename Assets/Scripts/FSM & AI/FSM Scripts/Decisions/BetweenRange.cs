using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/PlayerBetweenRange")]
public class BetweenRange : IDecision
{
    public float minRange;
    public float maxRange;
    public override bool Decide(StateMachine controller)
    {
        float distance = Vector2.Distance(controller.Player.position, controller.transform.position);

        return (distance < maxRange && distance > minRange);

    }

}
