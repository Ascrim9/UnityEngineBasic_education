using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ReinForcedGolem_Run_State : BossState
{
    private Boss_RainforcedGolem boss;
    float rotationSpeed = 300f;

    public Boss_ReinForcedGolem_Run_State(Boss _bossBase, EntityStateMachine _stateMachine, string _animBoolName, Boss_RainforcedGolem _boss) : base(_bossBase, _stateMachine, _animBoolName)
    {
        this.boss = _boss;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        EnemySpawner.Instance.bossWarning[1].SetActive(true);
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        boss.entityMovement.MoveTo(Vector3.left);

        if (boss.transform.position.x <= 17.99f)
        {
            boss.entityMovement.MoveTo(Vector3.zero);
            stateMachine.ChangeState(null,null,boss.phase1State);
            boss.ChangeState(FinalBossState.Phase01);
        }

    }
}
