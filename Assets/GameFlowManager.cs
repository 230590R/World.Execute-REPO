using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour {

  private LevelManager levelManager;
  private SceneGameManager gameManager;

  // Update is called once per frame
  void Update() {
    levelManager = LevelManager.Instance;
    gameManager = SceneGameManager.Instance;

    foreach (var scene in levelManager.scenes) {
      if (scene.sceneName == "Water" && scene.isComplete) {
        gameManager.SetSceneSwitcherToEnable("Energy");
        gameManager.SetSceneSwitcherToDisable("Water");
      }
      else if (scene.sceneName == "Energy" && scene.isComplete) {
        gameManager.SetSceneSwitcherToDisable("Energy");
      }
    }


  }
}
