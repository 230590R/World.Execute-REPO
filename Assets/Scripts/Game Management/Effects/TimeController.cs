using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {
  public static TimeController Instance { get; private set; }

  [SerializeField] private float timescale = 1f;
  [SerializeField] private float slowDuration = 1f;
  [SerializeField] private float elapsed;

  private void Awake() {
    Instance = this; // singleton
  }
  public void SlowTime(float factor, float duration) {
    timescale = factor;
    slowDuration = duration;
    elapsed = duration;
  }

  private void Update() {
    // update elapsed time
    if (elapsed > 0) elapsed -= Time.unscaledDeltaTime;
    else elapsed = 0;

    Time.timeScale = Mathf.Lerp(1, timescale, (elapsed / slowDuration));
    Time.fixedDeltaTime = 0.02f * Mathf.Lerp(1, timescale, (elapsed / slowDuration));
  }



}
