using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    [Header("Initialization")]
    protected EntityStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    [Header("Input")]
    protected float xInput;
    protected float yInput;

    [Header("State")]
    private string animBoolName;
    protected float stateTimer;
    protected bool triggerCalled;

    /// <summary>
    /// ?�레?�어 ?�태 ?�성??
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_stateMachine"></param>
    /// <param name="_animBoolName"></param>
    public PlayerState(Player _player, EntityStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }


    /// <summary>
    /// ?�태 머신?�서 Entry 부분이??
    /// </summary>
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }


    /// <summary>
    /// ?�태�??�데?�트 ?�주???�수.
    /// </summary>
    public virtual void Update()
    {

        player.SetVelocity(xInput * player.moveSpeed, yInput * player.moveSpeed);

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        stateTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z) && SkillManager.Instance.circleFire.CanUseSkill())
            stateMachine.ChangeState(null,player.ciecleFireState, null);
        if (Input.GetKeyDown(KeyCode.E) && SkillManager.Instance.blackhole.CanUseSkill())
            stateMachine.ChangeState(null,player.blackholeState,null);
        if (Input.GetKey(KeyCode.Space) && !PlayerManager.Instance.isAttacking)
        {
            if (SkillManager.Instance.circleFire.usingSkill)
                return;
            PlayerManager.Instance.StartFiring();
        }
        if (Input.GetKey(KeyCode.Space) && !(stateMachine.player_CurState == player.dashState))
        {
            if (SkillManager.Instance.circleFire.usingSkill)
                return;
            stateMachine.ChangeState(null,player.attackState,null);
        }


    }


    /// <summary>
    /// ?�태 머신?�서 Exit 부분이??
    /// </summary>
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);

    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
