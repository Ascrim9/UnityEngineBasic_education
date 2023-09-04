using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ReinForcedGolem_Dead_State : BossState
{
    private Boss_RainforcedGolem boss;

    public Boss_ReinForcedGolem_Dead_State(Boss _bossBase, EntityStateMachine _stateMachine, string _animBoolName, Boss_RainforcedGolem _boss) : base(_bossBase, _stateMachine, _animBoolName)
    {
        this.boss = _boss;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        boss.anim.SetBool(boss.lastAnimBoolName, false);
        boss.capsuleCollider2D.enabled = false;

        stateTimer = 0.1f;
    }


    public override void Exit()
    {
        base.Exit();
        boss.DestoryBoss();
    }

    public override void Update()
    {
        base.Update();


        if (stateTimer > 0)
        {
            rb.velocity = new Vector2(0, -10);
        }

    }
}
