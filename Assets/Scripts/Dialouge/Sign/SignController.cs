using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : NPCSpeechBubbleController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        speechBubble.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        speechBubble.SetActive(false);
    }
}
