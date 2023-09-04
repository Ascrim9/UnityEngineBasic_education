using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    [Header("Trauma")]
    public float curTrauma;
    public float maxTrauma;

    [Header("Config")]
    [SerializeField] private int maxLevel;
    [SerializeField] private int incremetalValue;

    public int Curlevel { get; private set; }

    [HideInInspector] public float curExp;
    [HideInInspector] public float curExpTemp;
    [HideInInspector] public float expToNextLevel;


    [Header("Stat Value UI")]
    [SerializeField] private Transform statSlotParent;

    private UI_StatSlot[] statSlot;

    protected override void Update()
    {
        maxTrauma = 200;
        maxHp = GetMaxHealthValue();
        UpdateHealthUI(curHp, GetMaxHealthValue());
        UpdateTraumaUI(curTrauma, maxTrauma);
    }
    private void Awake()
    {
        player = GetComponent<Player>();
        statSlot = statSlotParent.GetComponentsInChildren<UI_StatSlot>();
    }
    protected override void Start()
    {
        base.Start();
        Curlevel = level.GetValue();

        level.SetDefaultValue(1);
        expToNextLevel = exp.GetValue();

        ApplyLevelModifiers();
        UpdateExpUI();
        UpdateHealthUI(curHp, maxHp);
        UpdateTraumaUI(curTrauma, maxTrauma);
        UpdateStatSlot();
    }
    public void AddExperience(float ObtainedExp)
    {
        if (ObtainedExp > 0f)
        {
            float remainingExpToNextLevel = expToNextLevel - curExpTemp;

            if (ObtainedExp >= remainingExpToNextLevel)
            {
                ObtainedExp -= remainingExpToNextLevel;
                curExp += ObtainedExp;
                UpdateLevel();
                AddExperience(ObtainedExp);
                ApplyLevelModifiers();
                UpdateStatSlot();
            }
            else
            {
                curExp += ObtainedExp;
                curExpTemp += ObtainedExp;
                if (curExpTemp == expToNextLevel)
                {
                    UpdateLevel();
                }
            }
        }
        UpdateExpUI();
    }

    private void UpdateLevel()
    {
        if (Curlevel < maxLevel)
        {
            Curlevel++;
            curExpTemp = 0f;
            expToNextLevel *= incremetalValue;
        }
    }
    public void UpdateExpUI()
    {
        UI_InGame.Instance.UpdateExp(curExpTemp, expToNextLevel);
    }
    public override void UpdateHealthUI(float New_curhp, float New_maxhp)
    {
        UI_InGame.Instance.UpdateHealth(New_curhp, New_maxhp);
    }
    public void UpdateTraumaUI(float New_curTrauma, float New_maxTrauma)
    {
        UI_InGame.Instance.UpdateTrauma(New_curTrauma, New_maxTrauma);
    }

    private void ApplyLevelModifiers()
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
        for (int i = 1; i < Curlevel; i++)
        {
            float modifier = _stat.GetValue() * 0.05f;

            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }
    public override void TakeDamage(int _dmg)
    {
        base.TakeDamage(_dmg);
    }

    protected override void Die()
    {
        base.Die();
        player.Die();

        GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }

    protected override void DecreaseHealth(int _dmg)
    {
        base.DecreaseHealth(_dmg);
    }

    public void UpdateStatSlot()
    {
        for (int i = 0; i < statSlot.Length; i++)
        {
            statSlot[i].UpdateStatValueUI();
        }
    }
}
