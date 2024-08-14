using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> SceneSwitchers;

    void Start()
    {

        string lastUsedDoorID = PlayerPrefs.GetString("LastUsedDoorID", "");

        if (!string.IsNullOrEmpty(lastUsedDoorID))
        {
            foreach (GameObject spawnPoint in SceneSwitchers)
            {
                SceneSwitcherV2 sceneSwitcher = spawnPoint.GetComponent<SceneSwitcherV2>();
                if (sceneSwitcher != null && sceneSwitcher.DoorID == lastUsedDoorID)
                {
                    Player.transform.position = spawnPoint.transform.position;
                    break;
                }
            }
        }
        else
        {
            Debug.Log("no doorID found");
        }
    }
}
