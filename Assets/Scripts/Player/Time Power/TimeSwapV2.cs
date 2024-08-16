using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSwapV2 : MonoBehaviour
{
    public string Scene1;  
    public string Scene2;  
    public KeyCode switchKey = KeyCode.F;
    public string PlayerTag = "Player";
    private TimeSwapManager stateManager;
    public List<string> objectNamesToKeepActive; 
    public GameObject player;

    void Start()
    {
        stateManager = TimeSwapManager.Instance;

        player = GameObject.FindGameObjectWithTag(PlayerTag);
        if (player != null)
        {
            objectNamesToKeepActive.Add(player.name);
        }

        stateManager.currentScene = SceneManager.GetActiveScene().name;
    }

    public void ReAddPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(PlayerTag);
        }
    }

    public void TimeSwap()
    {
        if (string.IsNullOrEmpty(Scene1) && string.IsNullOrEmpty(Scene2)) return;

        stateManager.savedPosition = player.transform.position;

        string sceneToSwitchTo = (stateManager.currentScene == Scene1) ? Scene2 : Scene1;

        if (sceneToSwitchTo == stateManager.previousScene)
        {
            DeactivateScene(stateManager.currentScene);

            string temp = stateManager.currentScene;
            stateManager.currentScene = stateManager.previousScene;
            stateManager.previousScene = temp;

            ActivateScene(stateManager.currentScene);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(stateManager.currentScene));

            player.transform.position = stateManager.savedPosition;
        }
        else
        {
            stateManager.previousScene = stateManager.currentScene;
            stateManager.currentScene = Scene2;
            if (!SceneManager.GetSceneByName(sceneToSwitchTo).isLoaded)
            {
                SceneManager.LoadScene(sceneToSwitchTo, LoadSceneMode.Additive);
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    //void Update()
    //{
    //    if (string.IsNullOrEmpty(Scene1) && string.IsNullOrEmpty(Scene2)) return;

    //    if (Input.GetKeyDown(switchKey))
    //    {
    //        stateManager.savedPosition = player.transform.position;

    //        string sceneToSwitchTo = (stateManager.currentScene == Scene1) ? Scene2 : Scene1;

    //        if (sceneToSwitchTo == stateManager.previousScene)
    //        {
    //            DeactivateScene(stateManager.currentScene);

    //            string temp = stateManager.currentScene;
    //            stateManager.currentScene = stateManager.previousScene;
    //            stateManager.previousScene = temp;

    //            ActivateScene(stateManager.currentScene);

    //            SceneManager.SetActiveScene(SceneManager.GetSceneByName(stateManager.currentScene));

    //            player.transform.position = stateManager.savedPosition;
    //        }
    //        else
    //        {
    //            stateManager.previousScene = stateManager.currentScene;
    //            stateManager.currentScene = Scene2;
    //            if (!SceneManager.GetSceneByName(sceneToSwitchTo).isLoaded)
    //            {
    //                SceneManager.LoadScene(sceneToSwitchTo, LoadSceneMode.Additive);
    //            }

    //            SceneManager.sceneLoaded += OnSceneLoaded;
    //        }
    //    }
    //}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DeactivateScene(stateManager.previousScene);

        player.transform.position = stateManager.savedPosition;

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
                // Check if the object's name is not in the list of names to keep active
                if (!objectNamesToKeepActive.Contains(obj.name))
                {
                    obj.SetActive(false);
                }
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
