using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantItemImageUI : MonoBehaviour
{
    Image image;
    RectTransform rectTransform;
    MerchantShop merchantShop;

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        merchantShop = GetComponentInParent<MerchantShop>();
    }

    private void Start()
    {
        ChangeItemImage();
    }

    public void ChangeItemImage()
    {
        ItemSO item = merchantShop.itemsToBuy[merchantShop.currentItemID];

        image.sprite = item.image;
        rectTransform.sizeDelta = new Vector2(item.imageWidth, item.imageHeight);
    }
}
