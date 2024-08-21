using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxController : MonoBehaviour {
  private float _startPos;
  private GameObject cam;
  public float parallaxEffect;
  public float _length;

  private void Start() {
    _startPos = transform.position.x;
    cam = Camera.main.gameObject;
  }

  private void FixedUpdate() {
    float distance = cam.transform.position.x * parallaxEffect;
    float movement = cam.transform.position.x * (1-parallaxEffect);

    transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);

    if (movement > _startPos + _length) {
      _startPos += _length;
    }
    else if (movement < _startPos - _length) {
      _startPos -= _length;
    }
  }
}


