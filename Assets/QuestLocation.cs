using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocation : MonoBehaviour
{
    public QuestObj[] questObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        
        for (int i = 0; i < questObj.Length; i++)
        {
            questObj[i].CompleteQuest();
        }
    }


}
