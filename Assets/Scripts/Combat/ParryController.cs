using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryController : MonoBehaviour {
  public Rigidbody2D m_RigidBody2D;
  public MovementController m_MovementController;
  public float parried = 0;
  public void Parry(float r, Vector2 point) {
    Collider2D hit = Physics2D.OverlapCircle(point, r, LayerMask.GetMask("Parry"));
    if (hit == null) {
      return;
    }
    
    int dir = (transform.position.x > hit.transform.position.x) ? 1 : -1;
    Vector2 knockBackforce = new Vector2(dir * 10, 7.5f);
    if (m_MovementController != null) {
      m_MovementController.KnockBack(knockBackforce);
    }

    CineController.Instance.ShakeCamera(5, 1);
    CineController.Instance.ZoomCamera(1.5f, 1);
    TimeController.Instance.SlowTime(0.0000000001f, 0.8f);
    PostProcessController.Instance.SetChromatic(10);
    AudioHandlerV2.Instance.PlaySFXAtPosition("Player", 0, transform.position);
    parried = 1f;
  }

  private void Update() {
    parried = Mathf.Max(0, parried - Time.deltaTime);
  }
}
