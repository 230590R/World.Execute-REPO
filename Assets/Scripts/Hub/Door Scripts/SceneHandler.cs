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


        string EnableTargetDoorID = SceneGameManager.Instance.SceneSwitcherToEnableID;

        if (!string.IsNullOrEmpty(EnableTargetDoorID))
        {
            foreach (GameObject sceneSwitcherObject in SceneSwitchers)
            {
                SceneSwitcherV2 sceneSwitcher = sceneSwitcherObject.GetComponent<SceneSwitcherV2>();

                if (sceneSwitcher != null && sceneSwitcher.DoorID == EnableTargetDoorID)
                {
                    sceneSwitcher.enabled = true;
                    Debug.Log($"Enable SceneSwitcher: {EnableTargetDoorID}");
                    break;
                }
            }
        }

        string DisableTargetDoorID = SceneGameManager.Instance.SceneSwitcherToDisableID;

        if (!string.IsNullOrEmpty(DisableTargetDoorID))
        {
            foreach (GameObject sceneSwitcherObject in SceneSwitchers)
            {
                SceneSwitcherV2 sceneSwitcher = sceneSwitcherObject.GetComponent<SceneSwitcherV2>();

                if (sceneSwitcher != null && sceneSwitcher.DoorID == DisableTargetDoorID)
                {
                    sceneSwitcher.enabled = false;
                    Debug.Log($"Disabled SceneSwitcher: {DisableTargetDoorID}");
                    break;
                }
            }
        }

    }


}
