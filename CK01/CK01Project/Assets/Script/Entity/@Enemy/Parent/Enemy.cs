using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStats enemystats { get; private set; }

    public EntityMovment entitymovement { get; private set; }

    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Score Info")]
    public int exp;
    public int score;

    [Header("MoveInfo")]
    public float xVelocity;
    public float default_xVelocity;
    public float yVelocity;
    public float default_yVelocity;


    [Header("AttackInfo")]
    public float attackRadius;
    public float attackCoolDown;

    [Header("Bullet Info")]
    public GameObject bulletPrefab;


    [HideInInspector] public float lastTimeAttacked;


    public EntityStateMachine stateMachine { get; private set; }
    public string lastAnimBoolName { get; private set; }

    protected override void Start()
    {
        base.Start();

        entitymovement = GetComponent<EntityMovment>();
        enemystats = GetComponent<EnemyStats>();
    }
    protected override void Awake()
    {
        base.Awake();

        default_xVelocity = xVelocity;
        default_yVelocity = yVelocity;
        stateMachine = new EntityStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.enemy_CurState.Update();
    }

    public void DestroyEnemy() => Destroy(gameObject);


    public virtual void AssignLastAnimName(string _animBoolName)
    {
        lastAnimBoolName = _animBoolName;
    }

    public virtual void AnimationFinishTrigger() => stateMachine.enemy_CurState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.CircleCast(transform.position, attackRadius, Vector2.left, 0f, whatIsPlayer);
    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        xVelocity = default_xVelocity;
        yVelocity = default_yVelocity;
    }

    public override void SlowEntity(float _slowPercentage, float _slowDuration)
    {
        
        if(entitymovement != null)
            entitymovement.movespeed = entitymovement.movespeed * (1 - _slowPercentage);

        xVelocity = xVelocity * (1 - _slowPercentage);
        yVelocity = yVelocity * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);


        Invoke("ReturnDefaultSpeed", _slowDuration);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public bool CanAttack()
    {
        if (Time.time >= lastTimeAttacked + attackCoolDown)
        {
            lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }

    public void Attack()
    {

        if (PlayerManager.Instance.player != null)
        {
            Vector3 dir = (PlayerManager.Instance.player.transform.position - transform.position).normalized;
            GameObject clone = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            EnemyProjectile bullet = clone.GetComponent<EnemyProjectile>();
            bullet.SetShooter(this);
            clone.GetComponent<EntityMovment>().MoveTo(dir);

        }
    }
    public void MiniCircleFire()
    {
        int count = 10;
        float interval = 360 / count;
        float weightAngle = 0;
        for (int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            EnemyProjectile bullet = clone.GetComponent<EnemyProjectile>();
            bullet.SetShooter(this);

            float angle = weightAngle + interval * i;
            float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
            float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
            clone.GetComponent<EntityMovment>().MoveTo(new Vector2(x, y));

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            PlayerStats _target = collision.GetComponent<PlayerStats>();

            if (_target != null)
            {
                stats.DoDamage(_target);
                Destroy(gameObject);
            }
        }
    }

    public override void Die()
    {
        base.Die();

        PlayerManager.Instance.playerStats.AddExperience(exp);
        PlayerManager.Instance.score += score;
    }
}
