using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeController : MonoBehaviour {
  private Vector2 _attackPoint;
  private Vector2 _targetDir;
  public float damage;

  public string layerName;

  public float distance;
  public float radius;

  public float maxAttackCD;
  public float attackCD;

  public Vector2 AttackPoint {
    get { return _attackPoint; }
    set { _attackPoint = value; }
  }
  public Vector2 TargetDir {
    get { return _targetDir; }
    set { _targetDir = value; }
  }

  public Collider2D AttackEnter() {
    Debug.Log("hitattempt");
    Collider2D hit = CheckHit();

    if (hit == null) {
      return null;
    }

    //CinemachineShake.Instance.ShakeCamera(3.5f, 0.5f);
    HealthController health = hit.GetComponent<HealthController>();

    // calculate ray
    Vector2 dir = (hit.transform.position - transform.position).normalized;
    Ray2D hitRay = new Ray2D(transform.position, dir);
    

    health.TakeDamage(damage, hitRay);
    attackCD = maxAttackCD;
    return hit;
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


  public void AttackExit() {
  }

  // Start is called before the first frame update
  void Start() {
    if (_attackPoint == null) {
      _attackPoint = transform.position;
    }

  }

  // Update is called once per frame
  void Update() {
    _attackPoint = new Vector2(transform.position.x, transform.position.y) + _targetDir.normalized * distance;
    attackCD -= Time.deltaTime;
    if (attackCD < 0) attackCD = 0; 
  }

  private void OnDrawGizmos() {
    // Set the gizmos color
    Gizmos.color = Color.red;

    // Draw a gizmos at the attackPoint within given range
    Gizmos.DrawWireSphere(AttackPoint, radius);
  }

  public Collider2D CheckHit() {
    return Physics2D.OverlapCircle(_attackPoint, radius, LayerMask.GetMask(layerName));
  }

  public void UpdateEquipment(float dmg, float spd) {
    damage = dmg;
  }
}
