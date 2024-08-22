using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour {

  private LevelManager levelManager;
  private SceneGameManager gameManager;

  private PlayerController player;

  [SerializeField] IDebuff postWaterDebuff;
  [SerializeField] IDebuff postEnergyDebuff;


  private void Start() {
    SceneGameManager.Instance.SetSceneSwitcherToDisable("Energy");
    player = GameObject.FindObjectsOfType<PlayerController>()[0];
    player.m_Stats.debuffs.Clear();
  }

  // Update is called once per frame
  void Update() {
    levelManager = LevelManager.Instance;
    gameManager = SceneGameManager.Instance;

    if (player == null) 
      player = GameObject.FindObjectsOfType<PlayerController>()[0];


    foreach (var scene in levelManager.scenes) {


      if (scene.sceneName == "Water" && scene.isComplete) {
        gameManager.SetSceneSwitcherToEnable("Energy");
        gameManager.SetSceneSwitcherToDisable("Water");
        player.m_Stats.AddDebuff(postWaterDebuff);
      }
      else if (scene.sceneName == "Energy" && scene.isComplete) {
        gameManager.SetSceneSwitcherToDisable("Energy");
        player.m_Stats.AddDebuff(postEnergyDebuff);
      }
    }


  }
}
