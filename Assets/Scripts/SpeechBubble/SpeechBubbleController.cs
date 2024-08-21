using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleController : MonoBehaviour
{
    TMPro.TMP_Text m_TextMeshPro;
    NPCSpeechBubbleController NPC;

    [SerializeField] int newLineLimit = 20;
    [SerializeField] float letterDelay = 0.05f;

    private void Awake()
    {
        m_TextMeshPro = GetComponentInChildren<TMPro.TMP_Text>();
        NPC = transform.parent.GetComponent<NPCSpeechBubbleController>();
    }

    private void OnEnable()
    {
        StartCoroutine(DisplaySpeechBubble());
    }

    IEnumerator DisplaySpeechBubble()
    {
        for (int i = 0; i < NPC.speechBubbleSO.speechBubbleText.Length; i++)
        {
            string formattedText = FormatText(NPC.speechBubbleSO.speechBubbleText[i]);
            yield return StartCoroutine(DisplayTextWithDelay(formattedText));

            if (NPC.speechBubbleSO.speechBubbleTimer[i] > 0)
                yield return new WaitForSeconds(NPC.speechBubbleSO.speechBubbleTimer[i]);
        }

        if (NPC.speechBubbleSO.speechBubbleTimer[NPC.speechBubbleSO.speechBubbleText.Length - 1] > 0)
            gameObject.SetActive(false);
    }

    string FormatText(string text)
    {
        var formattedText = new System.Text.StringBuilder();
        char[] characters = text.ToCharArray();

        for (int i = 0; i < characters.Length; i++)
        {
            if (i > 0 && i % newLineLimit == 0)
            {
                formattedText.Append(characters[i]);

                if (characters[i] != ' ' || characters[i] != '.')
                    formattedText.Append("-");

                formattedText.Append('\n');
                continue;
            }
            formattedText.Append(characters[i]);
        }

        return formattedText.ToString();
    }

    IEnumerator DisplayTextWithDelay(string text)
    {
        m_TextMeshPro.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            m_TextMeshPro.text += text[i];
            yield return new WaitForSeconds(letterDelay);
        }
    }
}
