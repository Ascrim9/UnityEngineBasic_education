using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;


    private void OnValidate()
    {
        SetupVisible();
    }

    private void SetupVisible()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item Object - " + itemData.itemName;
    }

    public void SetUpItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        SetupVisible();
    }

    public void PickUpItem()
    {
        if (Inventory.Instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
            return;
        if (itemData.instantEffectItemType != EffectItemType.None)
            UseInstantEffectItem();

        Inventory.Instance.AddItem(itemData);
        Debug.Log("Picked Up Item" + itemData.itemName);
        Destroy(gameObject);
    }

    private void UseInstantEffectItem()
    {
        switch (itemData.instantEffectItemType)
        {
            case EffectItemType.PowerUp:
                Inventory.Instance.GetEquipment(EquipmentType.Weapon).attackLevel += 1;
                if (Inventory.Instance.GetEquipment(EquipmentType.Weapon).attackLevel >= 4)
                {
                    Inventory.Instance.GetEquipment(EquipmentType.Weapon).attackLevel = 4;
                    PlayerManager.Instance.score += 4000;
                }

                break;
        }
    }
}
