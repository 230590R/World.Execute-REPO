using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : NPCSpeechBubbleController
{
    [SerializeField] SpeechBubbleSO[] speeches;

    int currentSpeechToPlay = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = LevelManager.Instance.scenes.Count - 1; i >= 0; i--) 
        {
            if (LevelManager.Instance.scenes[i].isComplete)
            {
                currentSpeechToPlay = i+1;
                break;
            }
        }

        speechBubbleSO = speeches[currentSpeechToPlay];
        speechBubble.SetActive(true);
    }
}

