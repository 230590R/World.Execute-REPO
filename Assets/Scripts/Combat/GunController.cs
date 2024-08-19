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

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, angle + bulletAngleOffset));

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.AddForce(directionToTarget * fireForce, ForceMode2D.Impulse);

        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
            bulletController.bulletDmg = damage;

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
