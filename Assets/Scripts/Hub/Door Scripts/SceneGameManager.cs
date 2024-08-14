using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGameManager : MonoBehaviour
{
    public static SceneGameManager Instance { get; private set; }

    public string SceneSwitcherToEnableID;

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

    void OnApplicationQuit()
    {

        PlayerPrefs.DeleteKey("LastUsedDoorID");
        PlayerPrefs.Save();
        Debug.Log("LastUsedDoorID has been reset.");
    }
}
