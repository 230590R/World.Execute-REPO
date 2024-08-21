using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenadeController : MonoBehaviour
{
    [SerializeField] float explosionTime;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionDamage;

    [SerializeField] GameObject explosionParticle;

    private void Start()
    {
        Destroy(gameObject, explosionTime);
    }

    private void OnDestroy()
    {
        var explosion = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        var explosionModule = explosion.GetComponent<ParticleSystem>().main;
        explosionModule.stopAction = ParticleSystemStopAction.Destroy;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            HealthController healthController = colliders[i].gameObject.GetComponent<HealthController>();
            if (healthController == null) return;

            healthController.TakeDamage(explosionDamage);
        }
    }
}
