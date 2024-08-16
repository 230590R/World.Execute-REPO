using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSwap : MonoBehaviour
{
    public string sceneToSwitchTo; 
    public KeyCode switchKey = KeyCode.F;
    public string PlayerTag = "Player";
    private TimeSwapManager stateManager;

    void Start()
    {
        stateManager = TimeSwapManager.Instance;


       stateManager.currentScene = SceneManager.GetActiveScene().name;
        
    }

    void Update()
    {
 
        if (Input.GetKeyDown(switchKey))
        {
   
            stateManager.savedPosition = transform.position;

            if (sceneToSwitchTo == stateManager.previousScene)
            {
   
                DeactivateScene(stateManager.currentScene);

                string temp = stateManager.currentScene;
                stateManager.currentScene = stateManager.previousScene;
                stateManager.previousScene = temp;

                ActivateScene(stateManager.currentScene);

                SceneManager.SetActiveScene(SceneManager.GetSceneByName(stateManager.currentScene));

                GameObject newScenePlayer = GameObject.FindGameObjectWithTag(PlayerTag);
                if (newScenePlayer != null)
                {
                    newScenePlayer.transform.position = stateManager.savedPosition;
                }
            }
            else
            {
    
                stateManager.previousScene = stateManager.currentScene;

                if (!SceneManager.GetSceneByName(sceneToSwitchTo).isLoaded)
                {
                    SceneManager.LoadScene(sceneToSwitchTo, LoadSceneMode.Additive);
                }

                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DeactivateScene(stateManager.previousScene);

        GameObject newScenePlayer = GameObject.FindGameObjectWithTag(PlayerTag);

        if (newScenePlayer != null)
        {
            newScenePlayer.transform.position = stateManager.savedPosition;
        }

        SceneManager.SetActiveScene(scene);

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void DeactivateScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.isLoaded)
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject obj in rootObjects)
            {
                obj.SetActive(false);
            }
        }
    }

    void ActivateScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.isLoaded)
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject obj in rootObjects)
            {
                obj.SetActive(true);
            }
        }
    }
}
