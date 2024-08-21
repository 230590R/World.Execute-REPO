using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicatorController : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject Target;

    [SerializeField] float timeSpeed;
    [SerializeField] float speed;
    Rigidbody2D rigidBody;

    [SerializeField] float distanceAboveEnemy = 1.125f;

    Renderer renderer;

    [SerializeField] float indicatorDistanceCheck;
    [SerializeField] float transitionTimeMultiplier = 1.0f;
    [SerializeField] float distanceAwayFromPlayerArrow = 1.5f;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        rigidBody = GetComponentInChildren<Rigidbody2D>();
    }

    public void Hover()
    {
        var time = (1 + Mathf.Sin(Time.time * timeSpeed)) / 2f;

        rigidBody.velocity = Vector2.Lerp(new Vector2(rigidBody.velocity.x, -speed), new Vector2(rigidBody.velocity.x, speed), time);
    }

    private void Update()
    {

        Vector3 targetPos = transform.position + (Vector3.up * distanceAboveEnemy);

        if (renderer.isVisible == false)
        {
            Vector3 direction = (-Target.transform.position + transform.position).normalized;

            if (Vector2.Distance(transform.position, Target.transform.position) <= indicatorDistanceCheck)
            {
                if (indicator.activeSelf == false)
                {
                    indicator.SetActive(true);
                }

                Vector3 clampedPos = Target.transform.position + (direction * distanceAwayFromPlayerArrow);
                indicator.transform.position += (clampedPos - indicator.transform.position) * Time.deltaTime * 25f;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                indicator.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle + 90.0f + 180.0f);
            }
            else
            {
                indicator.SetActive(false);
            }
        }
        else
        {
            indicator.transform.position += (targetPos - indicator.transform.position) * Time.deltaTime * transitionTimeMultiplier;
            
            indicator.transform.localRotation = Quaternion.Lerp(indicator.transform.localRotation, Quaternion.Euler(0.0f, 0.0f, 180.0f), Time.deltaTime * transitionTimeMultiplier);
        }

        if (Vector2.Distance(indicator.transform.position, targetPos) <= 0.5f)
        {
            Hover();
        }
    }
}
