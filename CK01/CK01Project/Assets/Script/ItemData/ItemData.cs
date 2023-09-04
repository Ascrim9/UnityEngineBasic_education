using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum ItemType
{
    None,
    Material,
    Equipment
}
public enum EffectItemType
{
    None,
    PowerUp,
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public EffectItemType instantEffectItemType = EffectItemType.None;
    public ItemType itemType;
    public string itemName;
    public Sprite icon;


    [Range(0, 100)]
    public float dropChance;

    protected StringBuilder stringBuilder = new StringBuilder();

    public virtual string GetDescription()
    {
        return "";
    }
}
