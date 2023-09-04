using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircleFireState : PlayerState
{
    public PlayerCircleFireState(Player _player, EntityStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        player.skill.circleFire.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Z))
            stateMachine.ChangeState(null, player.idleState, null);
    }
}
