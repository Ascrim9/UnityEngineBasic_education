using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : Enemy
{

    #region States
    public Bat_Battle_State battleState { get; private set; }
    public Bat_Attack_State attackState { get; private set; }
    public Bat_Dead_State deadState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        battleState = new Bat_Battle_State(this, stateMachine, "Fly", this);
        attackState = new Bat_Attack_State(this, stateMachine, "Attack", this);
        deadState = new Bat_Dead_State(this, stateMachine, "Dead", this);
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
