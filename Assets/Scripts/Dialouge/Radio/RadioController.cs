using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : NPCSpeechBubbleController
{
    [SerializeField] SpeechBubbleSO[] speeches;

    int currentSpeechToPlay = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        speechBubbleSO = speeches[currentSpeechToPlay];
        speechBubble.SetActive(true);
    }
}

