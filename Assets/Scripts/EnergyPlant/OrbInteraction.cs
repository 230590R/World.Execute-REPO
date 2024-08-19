using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrbInteraction : MonoBehaviour
{
    public GameObject objectToActivate;

    public KeyCode interactionKey = KeyCode.E;

    public string playerTag = "Player";

    private bool playerInRange = false;

    public string Scene1;  
    public string Scene2;  
    private TimeSwapV2 timeSwapV2;

    private void Start()
    {
        timeSwapV2 = FindAnyObjectByType<TimeSwapV2>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
 
          objectToActivate.SetActive(true);
          TimeSwapManager.Instance.currentScene = SceneManager.GetActiveScene().name;
          timeSwapV2.Scene1 = Scene1;
          timeSwapV2.Scene2 = Scene2;
          timeSwapV2.ReAddPlayer();

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
        }
    }
}
