using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Golem : Enemy
{

    #region States
    public Golem_Battle_State battleState { get; private set; }
    public Golem_Attack_State attackState { get; private set; }
    public Golem_Dead_State deadState { get; private set; }
    public Golem_Upgrade_State upgradedState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        attackState = new Golem_Attack_State(this, stateMachine, "Attack", this);
        battleState = new Golem_Battle_State(this, stateMachine, "Fly", this);
        deadState = new Golem_Dead_State(this, stateMachine, "Dead", this);
        upgradedState = new Golem_Upgrade_State(this, stateMachine, "Upgrade", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(battleState, null, null);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState, null, null);
    }

}
