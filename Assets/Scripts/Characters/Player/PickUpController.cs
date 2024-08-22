using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PickUpItem(collision);
    }

    void PickUpItem(Collision2D collision)
    {
        ItemDropInterface item = collision.gameObject.GetComponent<ItemDropInterface>();
        if (item != null)
        {
            item.OnPickUpDropItemEffect(inventoryManager);
        }
    }
}
