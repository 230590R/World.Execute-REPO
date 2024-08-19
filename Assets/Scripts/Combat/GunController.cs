using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : IAttackController
{
    [SerializeField] float fireForce;
    [SerializeField] float bulletAngleOffset;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform target;

    public override void Attack(Vector2 direction)
    {
        Vector2 directionToTarget = target.transform.position - transform.position;
        directionToTarget = directionToTarget.normalized;

        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        Rigidbody2D rb = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, angle + bulletAngleOffset)).GetComponent<Rigidbody2D>();
        rb.AddForce(directionToTarget * fireForce, ForceMode2D.Impulse);

        if (atkTriggerName != "") m_Animator.SetTrigger(atkTriggerName);
        Invoke("AttackEnter", delay);
    }

    public override void ChargeUp(Vector2 direction)
    {
        return;
    }

    public override void Inactive()
    {
        return;
    }
}
