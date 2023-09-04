using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Pebble_Battle_State : EnemyState
{
    private Enemy_Pebble enemy;
    float rotationSpeed = 300f;
    public Pebble_Battle_State(Enemy _enemyBase, EntityStateMachine _stateMachine, string _animBoolName, Enemy_Pebble enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        if (PlayerManager.Instance.player == null) return;

        if (enemy.IsPlayerDetected())
        {
            Vector3 dir = (PlayerManager.Instance.player.transform.position - enemy.transform.position).normalized;

            enemy.GetComponent<EntityMovment>().MoveTo(dir);

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
