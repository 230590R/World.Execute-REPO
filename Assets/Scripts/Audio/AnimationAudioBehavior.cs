using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudioBehaviorV2 : StateMachineBehaviour
{
    public string categoryName;
    public int audioIndex;

    public bool StopSFX = false;
    public bool Loop = true;

    private AudioHandlerV2 audioHandler;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (audioHandler == null)
        {
            audioHandler = FindObjectOfType<AudioHandlerV2>();
        }

        if (StopSFX)
        {
            audioHandler.StopPlayingSFX(animator.gameObject.transform);
        }
        else if (audioHandler != null && !string.IsNullOrEmpty(categoryName))
        {
            audioHandler.PlaySFXIfNotPlaying(categoryName, audioIndex, animator.gameObject.transform,Loop);
        }
    }

    // Optional: OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Play a different sound or handle exit state logic if needed
    }

    // Optional: OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Handle state update logic if needed
    }
}
