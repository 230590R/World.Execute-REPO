using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialougeTrigger : MonoBehaviour
{
    GameObject parent;
    [SerializeField] DialougeController controller;

    DialogueTrigger dialogueTrigger;

    [HideInInspector] public bool wasInDialogue = false;

    private void Awake()
    {
        dialogueTrigger = transform.parent.GetComponent<DialogueTrigger>();
        parent = transform.parent.gameObject;
    }

    private void Update()
    {
        if (controller.dialougeTriggers.Contains(parent) && Input.GetKeyDown(KeyCode.E))
        {
            dialogueTrigger.TriggerDialogue();
            wasInDialogue = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        controller.dialougeTriggers.Add(parent);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        controller.dialougeTriggers.Remove(parent);
    }
}
