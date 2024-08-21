using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipController : MonoBehaviour
{
    [HideInInspector] public int selectedSlotIndex = -1;

    [SerializeField] GameObject inventory;
    private InventorySlot[] inventorySlots;

    ItemSO equippedItem = null;

    Image image;
    [SerializeField] Sprite originalSprite;

    [SerializeField] GameObject grenadePrefab;
    [SerializeField] Transform firePoint;

    GameObject player;

    [SerializeField] HealthController healthController;

    private void Awake()
    {
        image = GetComponent<Image>();
        inventorySlots = inventory.GetComponentsInChildren<InventorySlot>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && equippedItem != null)
        {
            switch (equippedItem.name)
            {
                case "Medkit":
                    healthController.Heal(10);
                    break;
                case "Frag Grenade":
                    Vector2 direction = (Input.mousePosition - player.transform.position).normalized;
                    GameObject grenade = Instantiate(grenadePrefab, firePoint.position, Quaternion.identity);
                    Rigidbody2D grenadeRigidBody = grenade.GetComponent<Rigidbody2D>();
                    grenadeRigidBody.AddForce(direction * 10.0f, ForceMode2D.Impulse);
                    break;
            }

            Destroy(inventorySlots[selectedSlotIndex].gameObject);
            equippedItem = null;
            image.sprite = originalSprite;
        }
    }

    public void ChangeEquipImage(int slotIndex)
    {
        selectedSlotIndex = slotIndex;
        InventoryItem itemInSlot = inventorySlots[selectedSlotIndex].GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null && itemInSlot.item.name != "Cash")
        {
            equippedItem = itemInSlot.item;
            image.sprite = equippedItem.image;
        }
    }
}
