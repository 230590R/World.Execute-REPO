using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwapManager : MonoBehaviour
{
    public static TimeSwapManager Instance;

    public string currentScene;
    public string previousScene;
    public Vector3 savedPosition;
    public string hubSceneName = "SceneHub";

    void Awake()
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

    private void Update()
    {
        if (currentScene == hubSceneName)
        {
            savedPosition = Vector3.zero;
            currentScene = previousScene = "";
        }
    }
}
