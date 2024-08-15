using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MerchantItemTextUI : MonoBehaviour
{
    public enum TEXT_TYPE
    {
        NAME,
        DESCRIPTION,
        COST
    }

    TMP_Text m_TextMeshPro;
    MerchantShop merchantShop;

    [SerializeField] TEXT_TYPE textType = TEXT_TYPE.NAME;

    private void Awake()
    {
        m_TextMeshPro = GetComponent<TMP_Text>();
        merchantShop = GetComponentInParent<MerchantShop>();
    }

    private void Start()
    {
        ChangeItemText();
    }

    public void ChangeItemText()
    {
        ItemSO item = merchantShop.itemsToBuy[merchantShop.currentItemID];

        switch (textType)
        {
            case TEXT_TYPE.NAME:
                m_TextMeshPro.text = item.name;
                break;
            case TEXT_TYPE.DESCRIPTION:
                m_TextMeshPro.text = item.description;
                break;
            case TEXT_TYPE.COST:
                m_TextMeshPro.text = item.cost.ToString();
                break;
        }
    }
}
