using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Golem_Battle_State : EnemyState
{
    private Enemy_Golem enemy;
    float rotationSpeed = 300f;
    public Golem_Battle_State(Enemy _enemyBase, EntityStateMachine _stateMachine, string _animBoolName, Enemy_Golem enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        if (enemy.stats.curHp <= enemy.stats.maxHp * 0.5f)
        {
            stateMachine.ChangeState(enemy.upgradedState,null, null);
        }

        if (enemy.IsPlayerDetected())
        {
            if (enemy.CanAttack())
                stateMachine.ChangeState(enemy.attackState, null, null);
        }
        else
        {
            enemy.SetVelocity(enemy.xVelocity, enemy.yVelocity);
        }
        if (enemy.transform.rotation.eulerAngles.y < 180f)
        {
            enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, Quaternion.Euler(0f, -180f, 0f), Time.deltaTime * rotationSpeed);

            if (enemy.onFlipped != null)
                enemy.onFlipped();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }


}
