using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScripts : MonoBehaviour
{
    public float damagePerSecond = 5f;

    private void OnTriggerStay2D(Collider2D other)
    {
        HealthController healthController = other.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}
