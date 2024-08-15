using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemDropInterface
{
    void OnPickUpDropItemEffect(InventoryManager inventoryManager);

    void HoverOnGround();
}
