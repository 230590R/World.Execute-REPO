using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour {

  private LevelManager levelManager;
  private SceneGameManager gameManager;

  private PlayerController player;

  [SerializeField] IDebuff postWaterDebuff;
  [SerializeField] IDebuff postEnergyDebuff;

  public string Scene1;
  public string Scene2;
  private TimeSwapV2 timeSwapV2;


  private void Start() {
    player = GameObject.FindObjectsOfType<PlayerController>()[0];
    player.m_Stats.debuffs.Clear();
    Debug.Log("asdsadsa");
    timeSwapV2 = FindAnyObjectByType<TimeSwapV2>();
  }

  // Update is called once per frame
  void Update() {
    levelManager = LevelManager.Instance;
    gameManager = SceneGameManager.Instance;

    if (player == null) 
      player = GameObject.FindObjectsOfType<PlayerController>()[0];


    foreach (var scene in levelManager.scenes) {


      if (scene.sceneName == "Water" && scene.isComplete) {
        //gameManager.SetSceneSwitcherToEnable("Energy");
        //gameManager.SetSceneSwitcherToDisable("Water");
        player.m_Stats.AddDebuff(postWaterDebuff);
      }
      else if (scene.sceneName == "Energy" && scene.isComplete) {
        //gameManager.SetSceneSwitcherToDisable("Energy");
        player.m_Stats.AddDebuff(postEnergyDebuff);

        TimeSwapManager.Instance.currentScene = SceneManager.GetActiveScene().name;
        timeSwapV2.ReAddPlayer();
        timeSwapV2.Scene1 = Scene1;
        timeSwapV2.Scene2 = Scene2;

      }
    }


  }
}
