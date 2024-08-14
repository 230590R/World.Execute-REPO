using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAttackController : MonoBehaviour {

  public abstract Collider2D Attack(Vector2 direction);
  public string layerName;
  public float damage;


}
