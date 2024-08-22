using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGameManager : MonoBehaviour
{
    public static SceneGameManager Instance { get; private set; }

    public string SceneSwitcherToEnableID;
    public string SceneSwitcherToDisableID;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSceneSwitcherToEnable(string doorID)
    {
        SceneSwitcherToEnableID = doorID;
    }

    public void SetSceneSwitcherToDisable(string doorID)
    {
        SceneSwitcherToDisableID = doorID;
    }

    void OnApplicationQuit()
    {

        PlayerPrefs.DeleteKey("LastUsedDoorID");
        PlayerPrefs.Save();
        Debug.Log("LastUsedDoorID has been reset.");
    }
}
