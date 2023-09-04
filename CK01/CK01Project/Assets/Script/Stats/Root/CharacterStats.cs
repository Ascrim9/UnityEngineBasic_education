
using System.Collections;
using UnityEngine;


public enum StatType
{
    level,
    exp,
    strength,
    agility,
    intelegence,
    vitality,
    dmg,
    criticalChance,
    criticalPower,
    health,
    armor,
    evasion,
    magicRes,
    fireDamage,
    iceDamage,
}

public class CharacterStats : MonoBehaviour
{

    private EntityFX fx;

    [Header("Major stats")]
    public Stat level;
    public Stat exp;
    public Stat strength; // 힘
    public Stat agility; // 민첩성
    public Stat intelligence; // 지능
    public Stat vitality; // 활력

    [Header("Offensive stats")]
    public Stat dmg;
    public Stat criticalChance;
    public Stat criticalPower;
    public Stat magicResistance;

    [Header("Defensive stats")]
    public Stat health;
    public Stat armor;
    public Stat evasion;


    [Header("Magic stats")]
    public Stat fireDmg;
    public Stat iceDmg;

    public bool isIgnited;
    public bool isChilled;

    [SerializeField] private float ailmentsDuration;



    private float ignitedTimer;
    private float chilledTimer;


    private float igniteDmgCooldown = 0.3f;


    private float igniteDmgTimer;


    private int igniteDmg;


    public float curHp;
    public float maxHp;

    public System.Action onHealthChanged;
    public bool isDie { get; private set; }
    public bool isInvincible { get; private set; }


    protected virtual void Start()
    {
        criticalPower.SetDefaultValue(150);

        maxHp = GetMaxHealthValue();
        curHp = GetMaxHealthValue();

        fx = GetComponent<EntityFX>();

    }

    protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;

        igniteDmgTimer -= Time.deltaTime;



        if (ignitedTimer < 0)
            isIgnited = false;

        if (chilledTimer < 0)
            isChilled = false;

        ApplyIgniteDamage();
    }


    public virtual void IncreaseStat(int _modifier, float _duration, Stat _statToModify)
    {
        StartCoroutine(StatModCoroutine(_modifier, _duration, _statToModify));
    }

    private IEnumerator StatModCoroutine(int _modifier, float _duration, Stat _statToModify)
    {
        _statToModify.AddModifier(_modifier);

        yield return new WaitForSeconds(_duration);

        _statToModify.RemoveModifier(_modifier);
    }

    private void ApplyIgniteDamage()
    {
        if (igniteDmgTimer < 0 && isIgnited)
        {

            DecreaseHealth(igniteDmg);

            if (curHp <= 0)
            {
                curHp = 0;
                Die();
            }


            igniteDmgTimer = igniteDmgCooldown;
        }
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (isDie) return;

        if (TargetCanAvoidAttack(_targetStats))
            return;

        int totalDmg = dmg.GetValue() + strength.GetValue();


        if (CanCritical())
        {
            totalDmg = CalculateCriticalDamage(totalDmg);
        }


        totalDmg = CheckTargetArmor(_targetStats, totalDmg);


        _targetStats.TakeDamage(totalDmg);

        DoMagicalDamage(_targetStats, 0);
    }


    public virtual void DoMagicalDamage(CharacterStats _targetStats, int plusdmg)
    {
        if (isDie) return;

        int _fireDmg = fireDmg.GetValue();
        int _iceDmg = iceDmg.GetValue();

        int totalMagicalDmg = _fireDmg + _iceDmg + intelligence.GetValue() + plusdmg;

        totalMagicalDmg = CheckTargetMagicResistance(_targetStats, totalMagicalDmg);
        _targetStats.TakeDamage(totalMagicalDmg);


        if (Mathf.Max(_fireDmg, _iceDmg) <= 0)
            return;

        TryToApplyAilments(_targetStats, _fireDmg, _iceDmg);

    }

    private void TryToApplyAilments(CharacterStats _targetStats, int _fireDmg, int _iceDmg)
    {
        bool canApplyIngite = _fireDmg > _iceDmg;
        bool canApplyChil = _iceDmg > _fireDmg;

        while (!canApplyIngite && !canApplyChil)
        {
            if (Random.value < 0.5f && _fireDmg > 0)
            {
                canApplyIngite = true;
                _targetStats.ApplyAilments(canApplyIngite, canApplyChil);
                return;
            }
            if (Random.value < 0.5f && _iceDmg > 0)
            {
                canApplyChil = true;
                _targetStats.ApplyAilments(canApplyIngite, canApplyChil);
                return;
            }
        }

        if (canApplyIngite)
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDmg * 0.2f));

        _targetStats.ApplyAilments(canApplyIngite, canApplyChil);
    }

    private static int CheckTargetMagicResistance(CharacterStats _targetStats, int totalMagicalDmg)
    {
        totalMagicalDmg -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);
        totalMagicalDmg = Mathf.Clamp(totalMagicalDmg, 0, int.MaxValue);
        return totalMagicalDmg;
    }

    public void ApplyAilments(bool _ignite, bool _chill)
    {
        if (isIgnited || isChilled)
            return;


        if (_ignite)
        {
            isIgnited = _ignite;
            ignitedTimer = ailmentsDuration;

            fx.IgniteFXFor(ailmentsDuration);
        }
        if (_chill)
        {
            isChilled = _chill;
            chilledTimer = 2;

            float slowPercentage = 0.2f;
            GetComponent<Entity>().SlowEntity(slowPercentage, ailmentsDuration);
            fx.ChillFXFor(ailmentsDuration);
        }
    }

    public void SetupIgniteDamage(int _dmg) => igniteDmg = _dmg;


    public virtual void TakeDamage(int _dmg)
    {
        if (isDie) return;
        if (isInvincible) return;

        DecreaseHealth(_dmg);

        fx.StartCoroutine("FlashFX");

        if (curHp <= 0 && !isDie)
        {
            curHp = 0;
            Die();
        }
    }
    public virtual void IncreaseHealth(int _amount)
    {
        if (isDie) return;


        if (curHp < maxHp)
        {
            curHp += _amount;

            if (curHp > GetMaxHealthValue())
                curHp = GetMaxHealthValue();

            UpdateHealthUI(curHp, maxHp);
        }
    }
    protected virtual void DecreaseHealth(int _dmg)
    {
        if (isDie) return;

        curHp -= _dmg;

        if (curHp <= 0)
        {
            curHp = 0;
            Die();
        }
        UpdateHealthUI(curHp, maxHp);
    }

    protected virtual void Die()
    {
        isDie = true;
    }
    private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    }
    private int CheckTargetArmor(CharacterStats _targetStats, int totalDmg)
    {
        if (_targetStats.isChilled)
            totalDmg -= Mathf.RoundToInt(_targetStats.armor.GetValue() * 0.8f);
        else
            totalDmg -= _targetStats.armor.GetValue();


        totalDmg -= _targetStats.armor.GetValue();

        totalDmg = Mathf.Clamp(totalDmg, 0, int.MaxValue);
        return totalDmg;
    }

    private bool CanCritical()
    {
        int totalCriticalChance = criticalChance.GetValue() + agility.GetValue();

        if (Random.Range(0, 100) <= totalCriticalChance)
            return true;

        return false;
    }

    private int CalculateCriticalDamage(int _dmg)
    {
        float totalCriticalPower = (criticalPower.GetValue() + strength.GetValue()) * 0.01f;

        float criticalDamage = _dmg + totalCriticalPower;

        return Mathf.RoundToInt(criticalDamage);
    }

    public int GetMaxHealthValue()
    {
        return health.GetValue() + vitality.GetValue() * 5;
    }

    public Stat GetStat(StatType _statType)
    {
        if (_statType == StatType.strength) return strength;
        else if (_statType == StatType.agility) return agility;
        else if (_statType == StatType.intelegence) return intelligence;
        else if (_statType == StatType.vitality) return vitality;
        else if (_statType == StatType.dmg) return dmg;
        else if (_statType == StatType.criticalChance) return criticalChance;
        else if (_statType == StatType.criticalPower) return criticalPower;
        else if (_statType == StatType.health) return health;
        else if (_statType == StatType.armor) return armor;
        else if (_statType == StatType.evasion) return evasion;
        else if (_statType == StatType.magicRes) return magicResistance;
        else if (_statType == StatType.fireDamage) return fireDmg;
        else if (_statType == StatType.iceDamage) return iceDmg;
        else if (_statType == StatType.level) return level;
        else if (_statType == StatType.exp) return exp;

        return null;
    }

    public virtual void UpdateHealthUI(float curhp, float maxhp)
    {

    }

    public void MakeInvincile(bool _invincible) => isInvincible = _invincible;

}
