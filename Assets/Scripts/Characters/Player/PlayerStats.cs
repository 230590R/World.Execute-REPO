using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
  public PlayerStatSO baseStats;
  public List<IDebuff> debuffs = new List<IDebuff>();

  [Header("Current Stats")]
  public float maxHealth;
  public float movementSpeed;
  public Vector2 dashSpeed;
  public Vector2 wallJumpSpeed;
  public float attackDamage;
  public float attackCooldown;
  public float rollCooldown;
  public float regen;


  private void Update() {
    maxHealth = baseStats.maxHealth;
    movementSpeed = baseStats.movementSpeed;
    dashSpeed = baseStats.dashSpeed;
    wallJumpSpeed = baseStats.wallJumpSpeed;
    attackDamage = baseStats.attackDamage;
    attackCooldown = baseStats.attackCooldown;
    rollCooldown = baseStats.rollCooldown;
    regen = baseStats.regen;

    for (int i = 0; i < debuffs.Count; i++) {
      debuffs[i].InflictDebuff(this);
    }

  }

  public void AddDebuff(IDebuff debuff) {
    bool alreadyHasDebuff = false;
    foreach (var d in debuffs) {
      if (d == debuff) alreadyHasDebuff = true;
    }
    if (alreadyHasDebuff) return;
    

    debuffs.Add(debuff);
  }

}
