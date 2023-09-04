using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FlySkull_Battle_State : EnemyState
{
    private Enemy_FlySkull enemy;
    float rotationSpeed = 300f;
    public FlySkull_Battle_State(Enemy _enemyBase, EntityStateMachine _stateMachine, string _animBoolName, Enemy_FlySkull enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        if (enemy.transform.rotation.eulerAngles.y < 180f)
        {
            enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, Quaternion.Euler(0f, -180f, 0f), Time.deltaTime * rotationSpeed);

            if (enemy.onFlipped != null)
                enemy.onFlipped();
        }

        enemy.SetVelocity(enemy.xVelocity, enemy.yVelocity);


    }


    public override void Exit()
    {
        base.Exit();
    }


}
