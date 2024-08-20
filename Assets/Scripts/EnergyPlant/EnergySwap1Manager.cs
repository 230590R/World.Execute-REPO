using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySwap1Manager : MonoBehaviour
{
    public GameObject objectToActivate;

    public KeyCode interactionKey = KeyCode.E;

    public GameObject interactableObject;

    public string playerTag = "Player";

    private Transform playerTransform;
    private bool isPlayerInRange = false;

    void Start()
    {

        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            playerTransform = player.transform;
        }

    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
  

                objectToActivate.SetActive(true);
            
        }
    }

 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == playerTransform.gameObject)
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == playerTransform.gameObject)
        {
            isPlayerInRange = false;
        }
    }
}
