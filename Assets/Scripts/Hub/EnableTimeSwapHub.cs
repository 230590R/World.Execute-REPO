using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableTimeSwapHub : MonoBehaviour
{

    private LevelManager levelManager;
    public string Scene1;
    public string Scene2;
    private TimeSwapV2 timeSwapV2;

    // Start is called before the first frame update
    void Start()
    {
        timeSwapV2 = FindAnyObjectByType<TimeSwapV2>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

            if (levelManager.IsLevelComplete("Energy"))
            {

                TimeSwapManager.Instance.currentScene = SceneManager.GetActiveScene().name;
                timeSwapV2.Scene1 = Scene1;
                timeSwapV2.Scene2 = Scene2;
                timeSwapV2.ReAddPlayer();

            }


        
    }
}
