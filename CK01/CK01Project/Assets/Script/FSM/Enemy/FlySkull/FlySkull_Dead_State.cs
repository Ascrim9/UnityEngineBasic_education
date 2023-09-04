using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySkull_Dead_State : EnemyState
{
    private Enemy_FlySkull enemy;

    public FlySkull_Dead_State(Enemy _enemyBase, EntityStateMachine _stateMachine, string _animBoolName, Enemy_FlySkull _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        enemy.anim.SetBool(enemy.lastAnimBoolName, false);
        enemy.capsuleCollider2D.enabled = false;

        stateTimer = 0.1f;
    }


    public override void Exit()
    {
        base.Exit();
        enemy.DestroyEnemy();
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
