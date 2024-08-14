using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingFunction : MonoBehaviour
{
    public string DoorID; 
    public string PlayerTag = "Player"; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
        {
            SceneGameManager.Instance.SetSceneSwitcherToEnable(DoorID);
            Debug.Log($"DoorID {DoorID} set in GameManager.");
        }
    }
}
