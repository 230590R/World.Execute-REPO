using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
  public static BGMManager Instance;

  private void Awake() {
    if (Instance != null)
      Destroy(gameObject);
    else
      Instance = this;
    DontDestroyOnLoad(gameObject);
  }

  private void Start() {
    SetBGM();
  }

  public void SetBGM() {
    Debug.Log("Nigger");

    string sceneName = SceneManager.GetActiveScene().name;
    switch (sceneName) {
      case "Menu":
        AudioHandlerV2.Instance.PlayBGM("BGM", 3);
        break;
      case "HubScene":
      case "HubSceneFuture":
        AudioHandlerV2.Instance.PlayBGM("BGM", 2);
        break;
      case "Tutorial level":
        break;
      case "Energy Intro":
      case "Energy Swap1":
      case "Energy Swap2":
        break;
    }
  }
}
