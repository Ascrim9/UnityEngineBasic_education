using UnityEngine;

public class Boss_Crab_Phase_3_State : BossState
{
    private Boss_Crab boss;
    Vector3 dir = Vector3.up;

    public Boss_Crab_Phase_3_State(Boss _bossBase, EntityStateMachine _stateMachine, string _animBoolName, Boss_Crab boss) : base(_bossBase, _stateMachine, _animBoolName)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        boss.entityMovement.MoveTo(dir);



        if (triggerCalled)
        {
            boss.StartFiring(AttackType.SingleFireToPlayer);
            triggerCalled = false;
            boss.StopFiring(AttackType.SingleFireToPlayer);
        }
        if (skillCalled)
        {
            boss.StopFiring(AttackType.BossLazer);
            boss.StartFiring(AttackType.BossLazer);
            skillCalled = false;

        }

        if (boss.transform.position.y >= boss.stageData.LimitMax.y ||
             boss.transform.position.y <= boss.stageData.LimitMin.y)
        {
            dir *= -1;
            boss.entityMovement.MoveTo(dir);

        }

    }

    public override void Exit()
    {
        base.Exit();
    }


}
