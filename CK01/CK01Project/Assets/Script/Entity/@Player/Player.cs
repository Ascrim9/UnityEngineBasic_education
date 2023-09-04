using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private StageData stageData;

    #region States
    public EntityStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerCircleFireState ciecleFireState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerBlackholeState blackholeState { get; private set; }
    public PlayerDeathState deathState { get; private set; }
    #endregion

    [Header("Attack Info")]
    public Transform bullet_Pos;

    [Header("Move Info")]
    public float rotateSpeed = 10.0f;
    public float moveSpeed = 3;
    public float defaultMoveSpeed;

    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDuration;
    public Vector3 dashDir { get; private set; }
    public SkillManager skill { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EntityStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        ciecleFireState = new PlayerCircleFireState(this, stateMachine, "CircleFire");
        blackholeState = new PlayerBlackholeState(this, stateMachine, "Blackhole");
        deathState = new PlayerDeathState(this, stateMachine, "Dead");
    }
    protected override void Start()
    {
        base.Start();

        skill = SkillManager.Instance;

        stateMachine.Initialize(null, idleState, null);
    }
    protected override void Update()
    {
        
        if (stats.isDie) return;

        if (Time.timeScale == 0)
            return;
        base.Update();
        stateMachine.player_CurState.Update();

        if (Input.GetKeyDown(KeyCode.F))
            Inventory.Instance.UseFlask();

        CheckForDashInput();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }
    private void CheckForDashInput()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.Instance.dash.CanUseSkill())
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput != 0 || verticalInput != 0)
            {
                dashDir = new Vector2(horizontalInput, verticalInput);
                dashDir.Normalize();

                stateMachine.ChangeState(null, dashState, null);
            }
        }
    }


    public override void SlowEntity(float _slowPercentage, float _slowDuration)
    {
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke("ReturnDefaultSpeed", _slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
    }

    public void AnimationTrigger() => stateMachine.player_CurState.AnimationFinishTrigger();

   
    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(null,deathState,null);
    }


}
