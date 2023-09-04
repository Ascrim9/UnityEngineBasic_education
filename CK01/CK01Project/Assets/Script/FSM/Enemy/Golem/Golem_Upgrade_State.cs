using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Upgrade_State : EnemyState
{
    private Enemy_Golem enemy;

    public Golem_Upgrade_State(Enemy _enemyBase, EntityStateMachine _stateMachine, string _animBoolName, Enemy_Golem _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        if(enemy.enemystats != null)
        {
            enemy.enemystats.curHp += enemy.enemystats.curHp * 0.7f;
            enemy.enemystats.maxHp += enemy.enemystats.maxHp * 0.7f;

            enemy.enemystats.curHp = enemy.enemystats.maxHp;

        }


        enemy.anim.SetBool(enemy.lastAnimBoolName, false);

        stateTimer = 0.1f;
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(-1, 0);
    }
}
