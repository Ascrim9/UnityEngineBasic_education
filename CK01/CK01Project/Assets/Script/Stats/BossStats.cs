using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossStats : CharacterStats
{
    private Boss boss;
    private ItemDrop myDropSystem;

    [SerializeField] private EntityHpBar hpBarPrefab;
    [SerializeField] private Transform hpBarPosition;

    private EntityHpBar enemyHpBar;

    protected override void Start()
    {
        base.Start();

        CreateHealthBar();

        boss = GetComponent<Boss>();
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

    public override void TakeDamage(int _dmg)
    {
        base.TakeDamage(_dmg);
    }
    protected override void Die()
    {
        myDropSystem.GenerateDrop();
        base.Die();
        boss.Die();

    }
}
