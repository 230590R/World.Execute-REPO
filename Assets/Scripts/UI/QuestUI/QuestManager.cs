using System.Collections.Generic;
using System;
using UnityEngine;
using static QuestManager;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public List<QuestObj> quests = new List<QuestObj>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddQuest(QuestObj quest)
    {
        if (!quests.Contains(quest))
        {
            quests.Add(quest);
            Debug.Log("Quest added: " + quest.questID);
            UpdateQuestUI();
            // Call the appropriate method based on questType
            MethodOfCompletion(quest);
        }
    }

    public void CompleteQuest(QuestObj quest)
    {
        if (quests.Contains(quest) && quest.isComplete)
        {
            quests.Remove(quest);
            Debug.Log("Quest completed and removed: " + quest.questID);
            UpdateQuestUI();
        }
    }

    public enum Methods
    {
        GOTOLOCATION,
        KILLENEMY,
        KILLENEMIES,
        TALKTO,
        INACTIVE
    }

    private Methods currentMethod = Methods.INACTIVE;


    private void MethodOfCompletion(QuestObj quest)
    {
        Methods method;
        if (Enum.TryParse(quest.questType, out method))
        {
            switch (method)
            {
                case Methods.GOTOLOCATION:
                    HandleGotoLocation(quest);
                    break;
                case Methods.KILLENEMY:
                    HandleKillEnemy(quest);
                    break;
                case Methods.KILLENEMIES:
                    HandleKillEnemies(quest);
                    break;
                case Methods.TALKTO:
                    HandleTalkTo(quest);
                    break;
                case Methods.INACTIVE:
                    // Handle inactive quests
                    break;
            }
        }
    }

    private void HandleGotoLocation(QuestObj quest)
    {
        // Logic for "GOTOLOCATION" quest type
    }

    private void HandleKillEnemy(QuestObj quest)
    {
        // Logic for "KILLENEMY" quest type
    }

    private void HandleKillEnemies(QuestObj quest)
    {
        // Logic for "KILLENEMIES" quest type
    }

    private void HandleTalkTo(QuestObj quest)
    {
        // Logic for "TALKTO" quest type
    }

    private void UpdateQuestUI()
    {
        // Update the quest UI
    }
}
