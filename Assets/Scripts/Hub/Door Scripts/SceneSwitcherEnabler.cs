using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcherEnabler : MonoBehaviour
{
    public List<GameObject> SceneSwitchers; 

    void Start()
    {
        string targetDoorID = SceneGameManager.Instance.SceneSwitcherToEnableID;

        if (!string.IsNullOrEmpty(targetDoorID))
        {
            foreach (GameObject sceneSwitcherObject in SceneSwitchers)
            {
                SceneSwitcherV2 sceneSwitcher = sceneSwitcherObject.GetComponent<SceneSwitcherV2>();

                if (sceneSwitcher != null && sceneSwitcher.DoorID == targetDoorID)
                {
                    sceneSwitcher.enabled = true;
                    Debug.Log($"Enabled SceneSwitcherV2 with DoorID: {targetDoorID}");
                    break;
                }
            }
        }
    }
}
