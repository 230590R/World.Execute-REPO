using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashItemDropController : MonoBehaviour, ItemDropInterface
{
    [SerializeField] ItemSO item;

    [SerializeField] float timeSpeed;
    [SerializeField] float speed;
    Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void HoverOnGround()
    {
        var time = (1 + Mathf.Sin(Time.time * timeSpeed)) / 2f;

        rigidBody.velocity = Vector2.Lerp(new Vector2(rigidBody.velocity.x, -speed), new Vector2(rigidBody.velocity.x, speed), time);
    }

    private void Update()
    {
        HoverOnGround();
    }

    public void OnPickUpDropItemEffect(InventoryManager inventoryManager)
    {
        inventoryManager.AddItem(item);

        Destroy(gameObject);
    }
}
