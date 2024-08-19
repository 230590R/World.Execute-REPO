using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  GameManager Instance;

  private void Awake() {
    if (Instance == null) {
      Instance = this;
    }
    else {
      Destroy(this);
    }
  }

  private void Start() {
    DontDestroyOnLoad(this.gameObject);
  }


}
