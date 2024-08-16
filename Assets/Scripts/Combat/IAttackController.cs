using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAttackController : MonoBehaviour {

  public abstract void Inactive();
  public abstract void ChargeUp(Vector2 direction);
  public abstract void Attack(Vector2 direction);


  public Animator m_Animator;
  public LayerMask layer;
  public float damage;
  public string atkTriggerName = "";
  public float delay = 0;
  public Collider2D lastHit;
}
