using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCamera : MonoBehaviour {
  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    Vector3 position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
    transform.position = position;
    transform.rotation = Camera.main.transform.rotation;

  }
}
