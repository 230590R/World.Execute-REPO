using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpeechBubbleController : MonoBehaviour
{
    public SpeechBubbleSO speechBubbleSO;
    [HideInInspector] public GameObject speechBubble;

    private void Awake()
    {
        speechBubble = transform.GetChild(0).gameObject;
    }
}
