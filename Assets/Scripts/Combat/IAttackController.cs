using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAttackController : MonoBehaviour {

  public abstract void ChargeUp(Vector2 direction);
  public abstract Collider2D Attack(Vector2 direction);
  
    
    public Animator m_Animator;
    public LayerMask layer;
    public float damage;
    public string atkTriggerName = "";

}
