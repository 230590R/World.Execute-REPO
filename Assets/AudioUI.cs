using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUI : MonoBehaviour {

  public Button button;
  public GameObject audioUICanvas;

  void Start() {
    button.onClick.AddListener(ButtonListener);

  }

  public void ButtonListener() {
    AudioHandlerV2.Instance.GetComponent<AudioManager>().ToggleAudioSetting(audioUICanvas);
  }

}
