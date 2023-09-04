using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pebble : Enemy
{

    #region States
    public Pebble_Battle_State battleState { get; private set; }
    public Pebble_Dead_State deadState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        battleState = new Pebble_Battle_State(this, stateMachine, "Chase", this);
        deadState = new Pebble_Dead_State(this, stateMachine, "Dead", this);
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
