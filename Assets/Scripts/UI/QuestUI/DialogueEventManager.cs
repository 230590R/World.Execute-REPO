using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventManager : MonoBehaviour
{

    [SerializeField] NPCController[] NPCs;

    private void OnEnable()
    {
        for (int i = 0; i < NPCs.Length; i++)
        {
            DialogueManager.onDialogueEnd += NPCs[i].OnDialogueExit;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < NPCs.Length; i++)
        {
            DialogueManager.onDialogueEnd -= NPCs[i].OnDialogueExit;
        }
    }
}
