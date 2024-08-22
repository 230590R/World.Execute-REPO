using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class DialogueCompletionTrigger : MonoBehaviour
{
    DialogueManager manager;
    [SerializeField] private DialougeSO dialogueSO;

    private void Awake()
    {
        manager = FindObjectOfType<DialogueManager>(); // Automatically finds and assigns the DialogueManager in the scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dialogueSO.dialougeDone) return;
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            manager.StartDialogue(dialogueSO);

            // Optionally, you can log a message or trigger some other event here
            Debug.Log("Dialogue marked as done!");
        }

        // Set the dialogueDone flag to true
        dialogueSO.dialougeDone = true;
        manager.EndDialogue();
    }
}
