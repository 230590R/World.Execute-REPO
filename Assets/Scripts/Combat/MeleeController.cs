using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeController : IAttackController {
  private Vector2 _attackPoint;

  public float distance;
  public float radius;

  public float maxAttackCD;
  public float attackCD;

  public Collider ParryCollider;

  private bool parryableState;

  public Vector2 AttackPoint {
    get { return _attackPoint; }
    set { _attackPoint = value; }
  }

  public override void ChargeUp(Vector2 direction) {
    if (ParryCollider == null) return;
    // activate the parry collider
    parryableState = true;
  }

  public override Collider2D Attack(Vector2 direction) {
    Vector2 point = (Vector2)transform.position + (direction * distance);
    _attackPoint = point;
    return AttackEnter(damage, radius, point);
  }







  public Collider2D AttackEnter(float dmg, float r, Vector2 point) {
    Collider2D hit = Physics2D.OverlapCircle(point, r, LayerMask.GetMask(layerName));
    _attackPoint = point;
    if (hit == null) {
      return null;
    }

    //CinemachineShake.Instance.ShakeCamera(3.5f, 0.5f);
    HealthController health = hit.GetComponent<HealthController>();

    // calculate ray
    Vector2 dir = (hit.transform.position - transform.position).normalized;
    Ray2D hitRay = new Ray2D(transform.position, dir);


    health.TakeDamage(dmg, hitRay);
    attackCD = maxAttackCD;
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
    attackCD -= Time.deltaTime;
    if (attackCD < 0) attackCD = 0; 
  }

  private void OnDrawGizmos() {
    // Set the gizmos color
    Gizmos.color = Color.red;

    // Draw a gizmos at the attackPoint within given range
    Gizmos.DrawWireSphere(AttackPoint, radius);
  }


}
