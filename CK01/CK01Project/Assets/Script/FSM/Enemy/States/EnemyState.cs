using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EntityStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;
    protected Transform player;


    protected bool triggerCalled;
    private string animBoolName;

    protected float stateTimer;


    public EnemyState(Enemy _enemyBase, EntityStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }


    public virtual void Update()
    {

        stateTimer -= Time.deltaTime;

    }

    public virtual void Enter()
    {
        player = PlayerManager.Instance.player.transform;

        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
        enemyBase.AssignLastAnimName(animBoolName);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
