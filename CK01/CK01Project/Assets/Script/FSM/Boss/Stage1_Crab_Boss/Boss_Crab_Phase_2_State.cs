using UnityEngine;

public class Boss_Crab_Phase_2_State : BossState
{
    private Boss_Crab boss;
    Vector3 dir = Vector3.up;

    public Boss_Crab_Phase_2_State(Boss _bossBase, EntityStateMachine _stateMachine, string _animBoolName, Boss_Crab boss) : base(_bossBase, _stateMachine, _animBoolName)
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

        if (boss.transform.position.y >= boss.stageData.LimitMax.y ||
             boss.transform.position.y <= boss.stageData.LimitMin.y)
        {
            dir *= -1;
            boss.entityMovement.MoveTo(dir);
        }

        if (boss.stats.curHp <= boss.stats.maxHp * 0.4f)
        {
            boss.StopFiring(AttackType.SingleFireToPlayer);
            stateMachine.ChangeState(null, null, boss.phase3State);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }


}
