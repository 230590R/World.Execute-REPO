using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpeechBubbleController : MonoBehaviour
{
    public SpeechBubbleSO speechBubbleSO;
    GameObject speechBubble;

    private void Awake()
    {
        speechBubble = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        speechBubble.SetActive(true);
    }
}
