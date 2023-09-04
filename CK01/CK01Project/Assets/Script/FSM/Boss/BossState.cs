using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState
{
    protected EntityStateMachine stateMachine;
    protected Boss bossBase;
    protected Rigidbody2D rb;
    protected Transform player;


    protected bool triggerCalled;
    protected bool skillCalled;

    private string animBoolName;

    protected float stateTimer;



    public BossState(Boss _bossBase, EntityStateMachine _stateMachine, string _animBoolName)
    {
        this.bossBase = _bossBase;
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
        rb = bossBase.rb;
        bossBase.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        bossBase.anim.SetBool(animBoolName, false);
        bossBase.AssignLastAnimName(animBoolName);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
    public virtual void SkillFinishTrigger()
    {
        skillCalled = true;
    }
}

