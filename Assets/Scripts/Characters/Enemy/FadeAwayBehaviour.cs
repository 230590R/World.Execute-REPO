using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAwayBehaviour : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElapsed = 0f;
    SpriteRenderer spriteRenderer;
    GameObject parentObjToRemove;
    Color startColor;
    public GameObject prefabToSpawn;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        parentObjToRemove = animator.gameObject.transform.parent.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;

        float newAlpha = 1 - (timeElapsed / fadeTime);

        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

        if (timeElapsed > fadeTime)
        {
            if (prefabToSpawn != null)
            {
                Instantiate(prefabToSpawn, parentObjToRemove.transform.position, parentObjToRemove.transform.rotation);
            }
            Destroy(parentObjToRemove);
        }
    }
}
