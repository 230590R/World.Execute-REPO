using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeController : IAttackController {

  private Vector2 _attackPoint;

  public float distance;
  public float radius;

  public Collider2D ParryCollider;
  public ParryController m_ParryController;
  public SpriteRenderer ParryIndicator;

  private bool parryableState;


  public Vector2 AttackPoint {
    get { return _attackPoint; }
    set { _attackPoint = value; }
  }


  public override void Inactive() {
    parryableState = false;
  }

  public override void ChargeUp(Vector2 direction) {
    parryableState = true;
  }

  public override void Attack(Vector2 direction) {
    Vector2 point = (Vector2)transform.position + (direction * distance);
    _attackPoint = point;
    if (atkTriggerName != "") m_Animator.SetTrigger(atkTriggerName);
    Invoke("AttackEnter", delay);
  }





  public Collider2D AttackEnter() {
    var hit = AttackEnter(damage, radius, _attackPoint);
    if (hit != null) {
      //MeleeHit?.Invoke();
      //CineController.Instance.ShakeCamera(2, 1);
      //TimeController.Instance.SlowTime(0.01f, 0.1f);
    }
    return hit;
  }

  public Collider2D AttackEnter(float dmg, float r, Vector2 point) {
    Parry(r, point);
    Collider2D hit = Physics2D.OverlapCircle(point, r, layer);
    _attackPoint = point;
    if (hit == null) {
      return null;
    }

    //CinemachineShake.Instance.ShakeCamera(3.5f, 0.5f);
    HealthController health = hit.GetComponent<HealthController>();

    if (health == null) return null;

    // calculate ray
    Vector2 dir = (hit.transform.position - transform.position).normalized;
    Ray2D hitRay = new Ray2D(transform.position, dir);


    health.TakeDamage(dmg, hitRay);
    return hit;
  }


  // Start is called before the first frame update
  void Start() {
    if (_attackPoint == null) {
      _attackPoint = transform.position;
    }

  }

  // Update is called once per frame
  void Update() {
    if (ParryCollider != null) {
      ParryCollider.enabled = parryableState;
      ParryIndicator.enabled = parryableState;
    }
  }



  private void OnDrawGizmos() {
    // Set the gizmos color
    Gizmos.color = Color.red;

    // Draw a gizmos at the attackPoint within given range
    Gizmos.DrawWireSphere(AttackPoint, radius);
  }

  private void Parry(float r, Vector2 point) {
    //Collider2D hit = Physics2D.OverlapCircle(point, r, LayerMask.GetMask("Parry"));
    //if (hit == null) {
    //  return;
    //}

    //Debug.Log("PARRY");
    if (m_ParryController != null) {
      m_ParryController.Parry(r, point);
    }
  }

}
