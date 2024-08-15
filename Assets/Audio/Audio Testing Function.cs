using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestingFunction : MonoBehaviour
{
    public string categoryName;
    public int audioIndex;
    private AudioHandlerV2 audioHandler;

    private void Start()
    {
        audioHandler = FindObjectOfType<AudioHandlerV2>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            audioHandler.PlaySFX("Testing", 0, transform);
            
        }
    }
}
