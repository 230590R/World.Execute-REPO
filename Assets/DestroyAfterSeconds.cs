using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {
  public void StartDestroy(float duration) {
    Invoke("DestroySelf", duration);
  }

  private void DestroySelf() {
    Destroy(gameObject);
  }
}
