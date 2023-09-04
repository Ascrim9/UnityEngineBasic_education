using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private float flyTime = 0.4f;
    private bool skillUsed;

    public PlayerBlackholeState(Player _player, EntityStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        skillUsed = false;
        stateTimer = flyTime;

        player.stats.MakeInvincile(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.fx.MakeTransprent(false);
        player.stats.MakeInvincile(false);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            rb.velocity = new Vector2(0, 0f);
            
            if(!skillUsed)
            {
                if(player.skill.blackhole.CanUseSkill())
                    skillUsed = true;
            }
        }

        if (player.skill.blackhole.SkillCompleted())
            stateMachine.ChangeState(null, player.idleState, null);
    }
}
