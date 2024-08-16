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

    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    private void OnDestroy()
    {
        
    }
}
