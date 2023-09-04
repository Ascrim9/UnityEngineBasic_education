using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Golem_Attack_State : EnemyState
{
    private Enemy_Golem enemy;
    public Golem_Attack_State(Enemy _enemyBase, EntityStateMachine _stateMachine, string _animBoolName, Enemy_Golem enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        if (enemy.transform.rotation.eulerAngles.y < 180f)
        {
            enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, Quaternion.Euler(0f, -180f, 0f), Time.deltaTime * 300f);

            if (enemy.onFlipped != null)
                enemy.onFlipped();
        }


        if (enemy.stats.curHp <= enemy.stats.maxHp * 0.5f)
        {
            stateMachine.ChangeState(enemy.upgradedState, null, null);
        }

        enemy.SetVelocity(enemy.xVelocity, enemy.yVelocity);

        if (triggerCalled)
        {
            enemy.MiniCircleFire();
            triggerCalled = false;
        }
    }

  

}
