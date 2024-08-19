using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    //[SerializeField] DialougeSO[] dialouges;

    //int currentDialougeIndex = 0;

    [HideInInspector] public DialougeSO dialougeSO;

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }
}
