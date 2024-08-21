using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public struct SceneStatus
    {
        public string sceneName;
        public bool isComplete;
    }
    public static LevelManager Instance { get; private set; }

    public List<SceneStatus> scenes = new List<SceneStatus>();

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

    public void CompleteLevel(string sceneName)
    {
        for (int i = 0; i < scenes.Count; i++)
        {
            if (scenes[i].sceneName == sceneName)
            {
                SceneStatus updatedStatus = scenes[i];
                updatedStatus.isComplete = true;
                scenes[i] = updatedStatus; 
                Debug.Log("Complete " + sceneName);
                return;
            }
        }
    }

    public bool IsLevelComplete(string sceneName)
    {
        foreach (var scene in scenes)
        {
            if (scene.sceneName == sceneName)
            {
                return scene.isComplete;
            }
        }
        return false;
    }

}
