using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float destroyTime;
    [HideInInspector] public float bulletDmg;

    [SerializeField] GameObject bulletSparkParticle;

    [SerializeField] string[] bulletLayersToHit;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool destroy = false;

        for (int i = 0; i < bulletLayersToHit.Length; i++)
        {
            if (LayerMask.NameToLayer(bulletLayersToHit[i]) == collision.gameObject.layer)
            {
                Destroy(gameObject);
                destroy = true;

                break;
            }
        }

        if (destroy == false) return;

        if (!collision.gameObject.CompareTag("Player")) return;

        HealthController healthController = collision.gameObject.GetComponent<HealthController>();

        if (healthController != null)
            healthController.TakeDamage(bulletDmg);
    }

    private void OnDestroy()
    {
        var bulletSpark = Instantiate(bulletSparkParticle, transform.position, Quaternion.identity);
        var bulletSparkModule = bulletSpark.GetComponent<ParticleSystem>().main;
        bulletSparkModule.stopAction = ParticleSystemStopAction.Destroy;
    }
}
