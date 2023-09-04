using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Bat_Attack_State : EnemyState
{
    private Enemy_Bat enemy;
    public Bat_Attack_State(Enemy _enemyBase, EntityStateMachine _stateMachine, string _animBoolName, Enemy_Bat enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.xVelocity, enemy.yVelocity);

        if (triggerCalled)
        {
            enemy.Attack();
            triggerCalled = false;
        }

    }

}
