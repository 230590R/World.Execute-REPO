using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //public Dialogue dialogue;

    [HideInInspector] public DialougeSO dialougeSO;

    //public void TriggerDialogue()
    //{
    //    FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    //}

    public void TriggerDialogue()
    {
        Debug.Log("tried to trigger dialogue");
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialougeSO);
    }
}
