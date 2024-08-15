using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenadeController : MonoBehaviour
{
    [SerializeField] float explosionTime;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionDamage;

    [SerializeField] GameObject explosionParticle;

    float explosionTimer;

    private void Awake()
    {
        explosionTimer = explosionTime;
    }

    private void Update()
    {
        if (explosionTimer > 0.0f)
        {
            explosionTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);

            // check radius for enemy
            // deal damage to enemy in the radius

            // create particle system
        }
    }
}
