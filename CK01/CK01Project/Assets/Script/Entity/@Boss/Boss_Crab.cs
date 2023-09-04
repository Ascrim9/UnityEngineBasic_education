using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Boss_Crab : Boss
{
    #region States
    public Boss_Crab_Phase_1_State phase1State { get; private set; }
    public Boss_Crab_Phase_2_State phase2State { get; private set; }
    public Boss_Crab_Phase_3_State phase3State { get; private set; }
    public Boss_Crab_Dead_State deadState { get; private set; }
    public Boss_Crab_Run_State runState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        phase1State = new Boss_Crab_Phase_1_State(this, stateMachine, "Phase1", this);
        phase2State = new Boss_Crab_Phase_2_State(this, stateMachine, "Phase2", this);
        phase3State = new Boss_Crab_Phase_3_State(this, stateMachine, "Phase3", this);
        deadState = new Boss_Crab_Dead_State(this, stateMachine, "Dead", this);
        runState = new Boss_Crab_Run_State(this, stateMachine, "Run", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(null, null, runState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();

        EnemySpawner.Instance.isBossSpawned = false;
        EnemySpawner.Instance.curStageIndex++;
        EnemySpawner.Instance.stageName.SetActive(true);
        stateMachine.ChangeState(null, null, deadState);
    }

}
