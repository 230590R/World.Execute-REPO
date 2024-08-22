using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
  public static BGMManager Instance;
  public bool bossOverride = false;

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
    string sceneName = SceneManager.GetActiveScene().name;
    switch (sceneName) {
      case "Menu":
        AudioHandlerV2.Instance.PlayBGM("BGM", 3);
        break;
      case "HubScene":
      case "HubSceneFuture":
        if (!bossOverride) AudioHandlerV2.Instance.PlayBGM("BGM", 2);
        break;
      case "Tutorial level":
        AudioHandlerV2.Instance.PlayBGM("BGM", 4);
        break;
      case "Energy Intro":
      case "Energy Swap1":
      case "Energy Swap2":
        AudioHandlerV2.Instance.PlayBGM("BGM", 1);
        break;
      case "Water Level":
        AudioHandlerV2.Instance.PlayBGM("BGM", 5);
        break;
    }
  }
}
