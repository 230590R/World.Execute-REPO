using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    NPCDialougeTrigger npcDialougeTrigger;
    public QuestObj quest;
    public QuestManager manager;

    private void Awake()
    {
        npcDialougeTrigger = GetComponentInChildren<NPCDialougeTrigger>();
    }

    private void Update()
    {
        if (dialogueManager.animator.GetBool("isOpen") == false && npcDialougeTrigger.wasInDialogue)
        {
            npcDialougeTrigger.wasInDialogue = false;
        }
    }

    public void OnDialogueExit()
    {
        manager.AddQuest(quest);
    }

}
