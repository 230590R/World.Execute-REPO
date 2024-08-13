using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {

  public float health;

  [Serializable]
  public class HealthLayer {
    public Slider slider;
    [HideInInspector] public float displayedHealth;
    public float interpolationSpeed = 1;
  }

  public List<HealthLayer> layers = new List<HealthLayer>();  

  private void Update() {
    for (int i = 0; i < layers.Count; i++) {
      layers[i].displayedHealth += (health - layers[i].displayedHealth) * layers[i].interpolationSpeed * Time.deltaTime;
      layers[i].slider.value = layers[i].displayedHealth;
    }
  }

  public void Init(float hp, float maxHp) {
    health = hp;
    SetMaxHealth(maxHp);
  }

  public void SetMaxHealth(float maxHealth) {
    for (int i = 0; i < layers.Count; i++) {
      layers[i].slider.maxValue = maxHealth;
    }
  }
}
