using System.Collections.Generic;
using System;
using UnityEngine;
using static QuestManager;
using TMPro;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;


    public List<QuestObj> quests = new List<QuestObj>();
    public TextMeshProUGUI taskDescription;
    public Animator animator;


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
            quest.isComplete = false;
            quests.Add(quest);
            Debug.Log("Quest added: " + quest.questID);
            UpdateQuestUI();

        }
    }

    public void CompleteQuest(QuestObj quest)
    {
        if (quests.Contains(quest) && quest.isComplete)
        {
            //quests.Remove(quest);
            Debug.Log("Quest completed and removed: " + quest.questID);
            UpdateQuestUI();
        }
    }



    public void UpdateQuestUI()
    {
        if (QuestManager.instance.quests.Count > 0)
        {
            string descriptions = "";

            for (int i = 0; i < QuestManager.instance.quests.Count; i++)
            {
                if (!QuestManager.instance.quests[i].isComplete)
                {
                    descriptions += QuestManager.instance.quests[i].description + "\n";
                }
            }


            taskDescription.text = descriptions;
            animator.SetBool("isActive", true);
            Debug.Log("Anim Start");
        }
        else
        {
            taskDescription.text = "";
            animator.SetBool("isActive", false);
            Debug.Log("Anim finish");
        }
    }

  
}
