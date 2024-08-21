using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public QuestObj quest;
    public GameObject targetLocation;
    public QuestManager manager;

    private bool isQuestTriggered = false;



    public void TriggerQuest()
    {
        if (!isQuestTriggered)
        {
            QuestManager.instance.AddQuest(quest);
            isQuestTriggered = true;
        }
    }

    public void CheckDialogueCompletion()
    {
        DialogueManager dialogueManager = GetComponent<DialogueManager>();
        if (dialogueManager != null && dialogueManager.dialogueSO.dialougeDone)
        {
            TriggerQuest();
        }
    }




}
