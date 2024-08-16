using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    NPCDialougeTrigger npcDialougeTrigger;

    private void Awake()
    {
        npcDialougeTrigger = GetComponentInChildren<NPCDialougeTrigger>();
    }

    private void Update()
    {
        if (dialogueManager.animator.GetBool("isOpen") == false && npcDialougeTrigger.wasInDialogue)
        {

        }
    }
}
