using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionEventHandler : MonoBehaviour
{

  private void OnEnable() {
    BGMManager.Instance.SetBGM();
    SceneTransition.OnTransition += BGMManager.Instance.SetBGM;
  }

  private void OnDisable() {

    SceneTransition.OnTransition -= BGMManager.Instance.SetBGM;
  }
}
