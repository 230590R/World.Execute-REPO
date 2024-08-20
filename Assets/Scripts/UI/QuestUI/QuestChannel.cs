using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class QuestChannel : MonoBehaviour
{
    // These Actions store methods that can be triggered when a quest is completed or activated.
    public Action<QuestManager> QuestCompleteEvent;
    public Action<QuestManager> QuestActivatedEvent;

    // Call this method when a quest is completed to notify anything listening to the event.
    public void CompleteQuest(QuestManager completedQuest)
    {
        QuestCompleteEvent?.Invoke(completedQuest); // Safely invoke the event if there are any subscribers.
    }

    // Call this method when a quest is assigned to notify anything listening to the event.
    public void AssignQuest(QuestManager questToAssign)
    {
        QuestActivatedEvent?.Invoke(questToAssign); // Safely invoke the event if there are any subscribers.
    }
}
