using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    [HideInInspector] public DialougeSO dialogueSO;

    public static event System.Action onDialogueEnd;


    void Start()
    {
        sentences = new Queue<string>(); 
    }

    public void StartDialogue(DialougeSO SO)
    {
        animator.SetBool("isOpen", true);

        this.dialogueSO = SO;

        nameText.text = SO.name;
        sentences.Clear();

        foreach (string sentence in SO.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayeNextSentence();
    }

    //public void StartDialogue (Dialogue dialogue)
    //{
    //    animator.SetBool("isOpen", true);

    //    nameText.text = dialogue.name;

    //    sentences.Clear();

    //    foreach (string sentence in dialogue.sentences)
    //    {
    //        sentences.Enqueue(sentence); 
    //    }

    //    DisplayeNextSentence();
    //}

    public void DisplayeNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            dialogueSO.dialougeDone = true;
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        onDialogueEnd?.Invoke();
    }
}
