using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player Stats")]
public class PlayerStatSO : ScriptableObject {
  public float maxHealth;
  public float movementSpeed;
  public Vector2 dashSpeed;
  public Vector2 wallJumpSpeed;
  public float attackDamage;
  public float attackCooldown;
  public float rollCooldown;
  public float regen;
}

