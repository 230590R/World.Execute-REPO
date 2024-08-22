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
    [SerializeField] float fireForce = 1.0f;

  GameObject player;

    [SerializeField] HealthController healthController;   


  private void Awake()
    {
        image = GetComponent<Image>();
        inventorySlots = inventory.GetComponentsInChildren<InventorySlot>();

    FindPlayerReference();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && equippedItem != null)
        {
            switch (equippedItem.name)
            {
                case "Medkit":
                    healthController.Heal(25);
                    break;
                case "Frag Grenade":
                    Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    Vector2 direction = (mouseToWorld - player.transform.position).normalized;
                    GameObject grenade = Instantiate(grenadePrefab, firePoint.position, Quaternion.identity);
                    Rigidbody2D grenadeRigidBody = grenade.GetComponent<Rigidbody2D>();
                    grenadeRigidBody.AddForce(direction * fireForce, ForceMode2D.Impulse);
                    break;
            }

            Destroy(inventorySlots[selectedSlotIndex].transform.GetChild(0).gameObject);
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

  public void FindPlayerReference() {
    player = GameObject.FindGameObjectWithTag("Player");

    healthController = player.GetComponent<HealthController>();
    firePoint = player.transform.GetChild(3).GetChild(0).GetChild(0);
  }
}
