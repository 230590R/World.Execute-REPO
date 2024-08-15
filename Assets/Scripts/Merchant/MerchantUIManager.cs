using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantUIManager : MonoBehaviour
{
    [SerializeField] MerchantItemImageUI merchantItemImageUI;
    [SerializeField] MerchantItemTextUI[] merchantItemTextUI;

    private void OnEnable()
    {
        MerchantShop.onChangingItemID += merchantItemImageUI.ChangeItemImage;

        for (int i = 0; i < merchantItemTextUI.Length; i++)
        {
            MerchantShop.onChangingItemID += merchantItemTextUI[i].ChangeItemText;
        }
    }

    private void OnDisable()
    {
        MerchantShop.onChangingItemID -= merchantItemImageUI.ChangeItemImage;

        for (int i = 0; i < merchantItemTextUI.Length; i++)
        {
            MerchantShop.onChangingItemID -= merchantItemTextUI[i].ChangeItemText;
        }
    }
}
