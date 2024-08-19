using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTimeSwapZone : MonoBehaviour
{
    private TimeSwapV2 timeSwapV2;

    private void Start()
    {
        // Find the TimeSwapV2 instance
        timeSwapV2 = FindObjectOfType<TimeSwapV2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeSwapV2.SetInTrigger(true);
            Debug.Log("Player entered NoTimeSwapZone");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeSwapV2.SetInTrigger(false);
            Debug.Log("Player exited NoTimeSwapZone");
        }
    }
}
