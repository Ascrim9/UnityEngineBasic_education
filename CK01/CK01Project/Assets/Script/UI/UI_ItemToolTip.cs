using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;


    public void ShowToolTip(ItemData_Equipment _item)
    {
        if (_item == null) return;

        itemNameText.text = _item.itemName;
        itemTypeText.text = _item.equipmentType.ToString();
        itemDescription.text = _item.GetDescription();


        if (itemNameText.text.Length > 14)
            itemNameText.fontSize = itemNameText.fontSize * 0.7f;
        else
            itemNameText.fontSize = 32;

        gameObject.SetActive(true);
    }

    public void HideToopTip()
    {
        gameObject.SetActive(false);
    }
}
