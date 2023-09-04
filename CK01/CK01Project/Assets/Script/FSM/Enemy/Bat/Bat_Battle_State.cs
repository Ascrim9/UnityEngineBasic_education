using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Bat_Battle_State : EnemyState
{
    private Enemy_Bat enemy;
    float rotationSpeed = 300f;
    public Bat_Battle_State(Enemy _enemyBase, EntityStateMachine _stateMachine, string _animBoolName, Enemy_Bat enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {

        base.Update();

        if (enemy.IsPlayerDetected())
        {
            if (enemy.CanAttack())
                stateMachine.ChangeState(enemy.attackState,null,null);
        }
        else
        {
            enemy.SetVelocity(enemy.xVelocity, enemy.yVelocity);
        }
       
    }

    public override void Exit()
    {
        base.Exit();
    }


}
