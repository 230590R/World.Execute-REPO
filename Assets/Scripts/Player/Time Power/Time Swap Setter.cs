using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSwapSetter : MonoBehaviour
{
    public string Scene1;  // First scene name
    public string Scene2;  // Second scene name
    private TimeSwapV2 timeSwapV2;

    void Start()
    {
        TimeSwapManager.Instance.currentScene = SceneManager.GetActiveScene().name;
        timeSwapV2 = FindAnyObjectByType<TimeSwapV2>();
        timeSwapV2.Scene1 = Scene1;
        timeSwapV2.Scene2 = Scene2;
        timeSwapV2.ReAddPlayer();
    }

}
