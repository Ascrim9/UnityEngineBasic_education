using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill : Skill
{
    [SerializeField] private int amountOfAttacks;
    [SerializeField] private float cloneCooldown;
    [SerializeField] private float blackholeDuration;

    [Space]
    [SerializeField] private GameObject blackHolePrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shinkSpeed;


    Blackhole_Skill_Controller curBlackhole;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newBlackHole = Instantiate(blackHolePrefab, player.transform.position, player.transform.rotation);

        curBlackhole = newBlackHole.GetComponent<Blackhole_Skill_Controller>();

        curBlackhole.SetUpBlackHole(maxSize, growSpeed, shinkSpeed, amountOfAttacks, cloneCooldown, blackholeDuration);
    }

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Update()
    {
        base.Update();
    }

    public bool SkillCompleted()
    {
        if (!curBlackhole)
            return false;

        if (curBlackhole.PlayerCanExitState)
        {
            curBlackhole = null;
            return true;
        }
        return false;

    }
}
