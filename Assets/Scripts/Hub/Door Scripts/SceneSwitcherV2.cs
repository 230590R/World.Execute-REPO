using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherV2 : MonoBehaviour
{
    public string SceneName;
    public string PlayerTag = "Player";
    public KeyCode InputKey = KeyCode.E;
    public string DoorID; 

    private bool isPlayerInTrigger = false;

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(InputKey))
        {
            PlayerPrefs.SetString("LastUsedDoorID", DoorID);
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if (collision.CompareTag(PlayerTag))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag))
        {
            isPlayerInTrigger = false;
        }
    }
}
