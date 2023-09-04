using UnityEngine;

public class Boss_ReinForcedGolem_Phase_3_State : BossState
{
    private Boss_RainforcedGolem boss;
    Vector3 dir = Vector3.up;

    public Boss_ReinForcedGolem_Phase_3_State(Boss _bossBase, EntityStateMachine _stateMachine, string _animBoolName, Boss_RainforcedGolem boss) : base(_bossBase, _stateMachine, _animBoolName)
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

        if (boss.transform.position.y >= boss.stageData.LimitMax.y ||
            boss.transform.position.y <= boss.stageData.LimitMin.y)
        {
            dir *= -1;
            boss.entityMovement.MoveTo(dir);
        }

        if (boss.stats.curHp <= boss.stats.maxHp * 0.3f)
        {
            return;
        }

    }

    public override void Exit()
    {
        base.Exit();
    }


}
