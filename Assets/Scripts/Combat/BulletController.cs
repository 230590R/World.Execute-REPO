using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float destroyTime;
    [SerializeField] float bulletDmg;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Destroy(collision.gameObject);

        HealthController healthController = collision.gameObject.GetComponent<HealthController>();

        if (healthController != null)
            healthController.TakeDamage(bulletDmg);
    }
}
