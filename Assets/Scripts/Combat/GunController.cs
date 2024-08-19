using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : IAttackController
{
    public float maxAttackCD;
    public float attackCD;

    [SerializeField] float fireForce;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform target;

    public override void Attack(Vector2 direction)
    {
        Vector2 directionToPlayer = target.transform.position - transform.position;
        directionToPlayer = directionToPlayer.normalized;

        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        Rigidbody2D rb = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, angle)).GetComponent<Rigidbody2D>();
        rb.AddForce(directionToPlayer * fireForce, ForceMode2D.Impulse);
    }

    public override void ChargeUp(Vector2 direction)
    {
      
    }

    public override void Inactive()
    {
        
    }
}
