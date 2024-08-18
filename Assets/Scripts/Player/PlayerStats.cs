using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
  public IDebuff[] debuffs;

  [Header("Default Stats")]
  public float base_maxHealth;
  public float base_movementSpeed;
  public Vector2 base_dashSpeed;
  public Vector2 base_wallJumpSpeed;
  public float base_attackDamage;
  public float base_attackCooldown;
  public float base_rollCooldown;
  public float base_regen;

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
    maxHealth = base_maxHealth;
    movementSpeed = base_movementSpeed;
    dashSpeed = base_dashSpeed;
    wallJumpSpeed = base_wallJumpSpeed;
    attackDamage = base_attackDamage;
    attackCooldown = base_attackCooldown;
    rollCooldown = base_rollCooldown;
    regen = base_regen;

    for (int i = 0; i < debuffs.Length; i++) {
      debuffs[i].InflictDebuff(this);
    }

  }
}
