using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Boss_ReinForcedGolem_Phase_1_State : BossState
{
    Vector3 dir = Vector3.up;
    private Boss_RainforcedGolem boss;
    public Boss_ReinForcedGolem_Phase_1_State(Boss _bossBase, EntityStateMachine _stateMachine, string _animBoolName, Boss_RainforcedGolem boss) : base(_bossBase, _stateMachine, _animBoolName)
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

        boss.entityMovement.MoveTo(dir);

        if (boss.transform.position.y >= boss.stageData.LimitMax.y ||
            boss.transform.position.y <= boss.stageData.LimitMin.y)
        {
            dir *= -1;
            boss.entityMovement.MoveTo(dir);
        }

        if (boss.stats.curHp <= boss.stats.maxHp * 0.7f)
        {
            stateMachine.ChangeState(null,null,boss.phase2State);
            boss.ChangeState(FinalBossState.Phase02);

        }

    }



}
