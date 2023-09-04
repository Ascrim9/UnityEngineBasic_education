using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop myDropSystem;

    [SerializeField] private EntityHpBar hpBarPrefab;
    [SerializeField] private Transform hpBarPosition;

    [Header("Trauma Points")]
    public float trauma;

    [Header("Level details")]
    public int Enemy_level;

    [Range(0f, 1f)]
    public float percentageModifier = .4f;


    private EntityHpBar enemyHpBar;

    protected override void Start()
    {
        ApplyLevelModifiers();

        base.Start();

        CreateHealthBar();

        enemy = GetComponent<Enemy>();
        myDropSystem = GetComponent<ItemDrop>();

    }

    private void CreateHealthBar()
    {
        enemyHpBar = Instantiate(hpBarPrefab, hpBarPosition);
        UpdateHealthUI(curHp, maxHp);
    }
    public override void UpdateHealthUI(float curhp, float maxhp)
    {
        enemyHpBar.ModifyHealth(curhp, maxhp);
    }

    public void ApplyLevelModifiers()
    {
        Modify(strength);
        Modify(agility);
        Modify(intelligence);
        Modify(vitality);

        Modify(dmg);
        Modify(criticalChance);
        Modify(criticalPower);

        Modify(health);
        Modify(armor);
        Modify(evasion);
        Modify(magicResistance);
        
        Modify(fireDmg);
        Modify(iceDmg);
    }
    private void Modify(Stat _stat)
    {
        for (int i = 1; i < Enemy_level; i++)
        {
            float modifier = _stat.GetValue() * percentageModifier;

            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }

    public override void TakeDamage(int _dmg)
    {
        base.TakeDamage(_dmg);
    }
    protected override void Die()
    {
        myDropSystem.GenerateDrop();
        base.Die();
        enemy.Die();

    }
}
