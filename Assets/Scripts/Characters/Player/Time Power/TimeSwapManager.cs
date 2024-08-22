using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSwapManager : MonoBehaviour
{
    public static TimeSwapManager Instance;

    private LevelManager levelManager;
    public string currentScene;
    public string previousScene;
    public Vector3 savedPosition;
    public string hubSceneName = "SceneHub";
    private TimeSwapV2 timeSwapV2;

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

    private void Start()
    {
        timeSwapV2 = GetComponent<TimeSwapV2>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == hubSceneName && !levelManager.IsLevelComplete("Energy"))
        {
            savedPosition = Vector3.zero;
            currentScene = previousScene = timeSwapV2.Scene1 = timeSwapV2.Scene2 = "";
            timeSwapV2.player = null;
        }
    }
}
