using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ?�레?�어가 Dash ?�태?????�출?�는 ?�크립트
/// </summary>
public class PlayerDashState : PlayerState
{

    public PlayerDashState(Player _player, EntityStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.clone.CreateCloneOnDashStart();
        SetSpriteAlpha(player.spriteRenderer, 0f);

        stateTimer = player.dashDuration;

        player.stats.MakeInvincile(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.skill.clone.CreateCloneOnDashOver();
        SetSpriteAlpha(player.spriteRenderer, 1f);

        player.SetVelocity(0, 0);


        player.stats.MakeInvincile(false);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.dashDir.x, player.dashSpeed * player.dashDir.y);

        if (stateTimer < 0)
            stateMachine.ChangeState(null, player.idleState, null);
    }
    private void SetSpriteAlpha(SpriteRenderer spriteRenderer, float alpha)
    {
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = alpha;
        spriteRenderer.color = spriteColor;
    }
}
