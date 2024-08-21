using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest")]
public class QuestObj : ScriptableObject
{
    public string questID;
    public string description;
    public bool isComplete;
    public string targetID; // Unique identifier for NPC or Enemy
    public string questType; // Should match enum values in QuestManager

    public void CompleteQuest()
    {
        isComplete = true;
        Debug.Log("Quest completed: " + questID);
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        QuestManager.instance.CompleteQuest(this);
    }
}
