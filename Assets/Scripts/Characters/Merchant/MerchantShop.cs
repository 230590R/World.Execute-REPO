using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShop : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemSO[] itemsToBuy;

    [HideInInspector] public int currentItemID = 0;

    private InventorySlot[] inventorySlots;
    [SerializeField] GameObject inventory;

    [HideInInspector] public bool inMerchantMenu = false;

    public static event System.Action onChangingItemID;

    private void Start()
    {
    inventoryManager = InventoryManager.Instance;
    inventory = inventoryManager.inventory;
    inventorySlots = inventory.GetComponentsInChildren<InventorySlot>();
    }

    public void OpenShop()
    {
        gameObject.SetActive(true);
        inMerchantMenu = true;
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
        inMerchantMenu = false;
    }

    public void IncreaseItemID(int amount)
    {
        currentItemID += amount;

        if (currentItemID >= itemsToBuy.Length - 1)
            currentItemID = itemsToBuy.Length - 1;

        onChangingItemID?.Invoke();
    }

    public void DecreaseItemID(int amount)
    {
        currentItemID -= amount;

        if (currentItemID <= 0)
            currentItemID = 0;

        onChangingItemID?.Invoke();
    }

    public void BuyMerchantItem()
    {
        ItemSO item = itemsToBuy[currentItemID];
        int amountOfCash = 0;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item.name == "Cash")
            {
                amountOfCash += itemInSlot.count;
            }
        }

        if (amountOfCash < item.cost) return;

        bool result = inventoryManager.AddItem(item);

        if (result)
        {
            int amountToDeduct = item.cost;

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.item.name == "Cash")
                {
                    itemInSlot.count -= amountToDeduct;

                    if (itemInSlot.count > 0)
                    {
                        itemInSlot.RefreshCount();
                        return;
                    }
                    else if (itemInSlot.count == 0)
                    {
                        Destroy(itemInSlot.gameObject);
                        return;
                    }
                    else if (itemInSlot.count < 0)
                    {
                        amountToDeduct = Mathf.Abs(itemInSlot.count);
                        Destroy(itemInSlot.gameObject);
                    }
                }
            }
        }
    }
}
