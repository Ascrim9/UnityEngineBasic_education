using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Boss_Crab_Phase_1_State : BossState
{
    private Boss_Crab boss;
    public Boss_Crab_Phase_1_State(Boss _bossBase, EntityStateMachine _stateMachine, string _animBoolName, Boss_Crab boss) : base(_bossBase, _stateMachine, _animBoolName)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        boss.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();


        if (triggerCalled)
        {
            boss.StartFiring(AttackType.CircleFire);

            if (boss.stats.curHp <= boss.stats.maxHp * 0.7f)
            {
                boss.StopFiring(AttackType.CircleFire);
                stateMachine.ChangeState(null, null, boss.phase2State);
            }

            triggerCalled = false;
        }
    }



}
