using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryController : MonoBehaviour {
  public Rigidbody2D m_RigidBody2D;
  public MovementController m_MovementController;
  public void Parry(float r, Vector2 point) {
    Collider2D hit = Physics2D.OverlapCircle(point, r, LayerMask.GetMask("Parry"));
    if (hit == null) {
      return;
    }

    Debug.Log("PARRY");
    int dir = (transform.position.x > hit.transform.position.x) ? 1 : -1;
    Vector2 knockBackforce = new Vector2(dir * 10, 7.5f);
    if (m_MovementController != null) {
      m_MovementController.KnockBack(knockBackforce);
    }
  }
}
