using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinisher : MonoBehaviour
{
    public string Scene;

    private SceneGameManager sceneGameManager;

    public void Start()
    {
        sceneGameManager = FindAnyObjectByType<SceneGameManager>();
    }
    public void FinishLevel(string LockDoor)
    {

        LevelManager.Instance.CompleteLevel(Scene);
        sceneGameManager.SetSceneSwitcherToDisable(LockDoor);
    }
}
