using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

  public HealthBarUI healthBarUI;

  public float health = 100;
  public float maxHealth = 100;


  private float hp1 = 0;
  private float hp2 = 0;

  private float speed1 = 10f;
  private float speed2 = 5f;


  public bool isPlayer = false;

  public Ray2D hitRay;

  // Start is called before the first frame update
  void Start() {
    healthBarUI.Init(health, maxHealth);
  }

  // Update is called once per frame
  void Update() {
    hp1 += (health - hp1) * speed1 * Time.deltaTime;
    hp2 += (health - hp2) * speed2 * Time.deltaTime;
    //healthBarUI.SetHealth(hp1, hp2);
    healthBarUI.health = health;
  }
  public void TakeDamage(float damage) {
    health -= damage;
  }

  public void TakeDamage(float damage, Ray2D ray) {
    TakeDamage(damage);
    hitRay = ray;
    if (isPlayer) {
      PostProcessController.Instance.SetVignette(0.5f);
      Debug.Log("sadasd");
    }
  }

  public void Heal(float healingPercentage) {
    health += (health * (healingPercentage / 100));
    if (health > maxHealth)
      health = maxHealth;
  }

  public void SetMaxHealth(float maxhp) {

    healthBarUI.SetMaxHealth(maxhp);
    maxHealth = maxhp;


  }
}
