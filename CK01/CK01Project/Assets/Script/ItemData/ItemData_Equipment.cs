using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}


[CreateAssetMenu(fileName = "New Item Data", menuName ="Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;


    [Header("Item Attack Level")]
    public GameObject[] bulletPrefabs;
    public int attackLevel = 1;

    [Header("Unique Effect")]
    public float itemCooldown;
    public ItemEffect[] itemEffects;
    [TextArea]
    public string itemEffectDescription;

    [Header("Major stats")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    [Header("Offensive stats;")]
    public int damage;
    public int criticalChance;
    public int criticalPower;

    [Header("Defensive stats")]
    public int maxHp;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Magic stats")]
    public int fireDamage;
    public int iceDamage;


    [Header("Craft Requirements")]
    public List<InventoryItem> craftingMaterials;

    private int descriptionLength;


    public void Effect(Transform _enemyPos)
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect(_enemyPos);
        }
    }

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.dmg.AddModifier(damage);
        playerStats.criticalChance.AddModifier(criticalChance);
        playerStats.criticalPower.AddModifier(criticalPower);


        playerStats.health.AddModifier(maxHp);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);

        playerStats.fireDmg.AddModifier(fireDamage);
        playerStats.iceDmg.AddModifier(iceDamage);

        playerStats.UpdateHealthUI(playerStats.curHp, playerStats.maxHp);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();


        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);
                             
        playerStats.dmg.RemoveModifier(damage);
        playerStats.criticalChance.RemoveModifier(criticalChance);
        playerStats.criticalPower.RemoveModifier(criticalPower);
              
        playerStats.health.RemoveModifier(maxHp);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);
       
        playerStats.fireDmg.RemoveModifier(fireDamage);
        playerStats.iceDmg.RemoveModifier(iceDamage);
    }

    public override string GetDescription()
    {
        stringBuilder.Length = 0;
        descriptionLength = 0;

        AddItemDescription(strength, "Strength");
        AddItemDescription(agility, "Agility");
        AddItemDescription(intelligence, "Intelligence");
        AddItemDescription(vitality, "Vitality");

        AddItemDescription(damage, "Damage");
        AddItemDescription(criticalChance, "Critical Chance");
        AddItemDescription(criticalPower, "Critical Power");

        AddItemDescription(maxHp, "Health");
        AddItemDescription(evasion, "Evasion");
        AddItemDescription(armor, "Armor");
        AddItemDescription(magicResistance, "Magic Resist.");

        AddItemDescription(fireDamage, "Fire damage");
        AddItemDescription(iceDamage, "Ice damage");


        //if(descriptionLength < 5)
        //{
        //    for (int i = 0; i < 5 - descriptionLength; i++)
        //    {
        //        stringBuilder.AppendLine();
        //        stringBuilder.Append("");
        //    }
        //}

        if(itemEffectDescription.Length > 0)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append(itemEffectDescription);
        }

        return stringBuilder.ToString();
    }

    private void AddItemDescription(int _value, string _name)
    {
        if(_value != 0)
        {
            if (stringBuilder.Length > 0)
                stringBuilder.AppendLine();

            if (_value > 0)
                stringBuilder.Append("+ " + _value + " " + _name);


            descriptionLength++;
        }
    }
}
