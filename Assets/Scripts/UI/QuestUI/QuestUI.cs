using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public TextMeshPro taskDescription;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateQuestUI();
    }

    public void UpdateQuestUI()
    {
        if (QuestManager.instance.quests.Count > 0)
        {
            string descriptions = "";

            foreach (var quest in QuestManager.instance.quests)
            {
                if (!quest.isComplete)
                {
                    descriptions += quest.description + "\n";
                }
            }
            taskDescription.text = descriptions;
            questStart();
        }
        else
        {
            taskDescription.text = "";
            questFinish();
        }
    }

    private void questStart()
    {
        if (QuestManager.instance.quests.Count > 0)
        {
            animator.SetBool("isActive", true);
        }
    }

    private void questFinish()
    {
        if (QuestManager.instance.quests.Count == 0)
        {
            animator.SetBool("isActive", false);
        }
    }
}
