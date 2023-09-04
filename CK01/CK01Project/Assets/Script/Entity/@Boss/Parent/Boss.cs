using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    CircleFire = 0,
    BigCircleFire = 1,
    SingleFireToPlayer = 2,
    SingleFireToPlayer_Corutine = 3,
    ShieldOn = 4,
    BossLazer = 5,
    BossLazer_Corutine = 6,
    BossTeleport = 7,
    FreezeAll = 8,
    Tornado = 9
}
public enum FinalBossState { MoveToCenter = 0, Phase01, Phase02, Phase03, Phase04, Phase05 }
public class Boss : Entity
{
    public StageData stageData;
    [SerializeField] protected LayerMask whatIsPlayer;
    public EnemyStats enemystats { get; private set; }

    [HideInInspector] public EntityMovment entityMovement;

    [Header("Score")]
    public int score;
    public int exp;


    [Header("MoveInfo")]
    public float xVelocity;
    public float default_xVelocity;
    public float yVelocity;
    public float default_yVelocity;

    [Header("AttackInfo")]
    public float attackRadius;
    public float attackCoolDown;

    [Header("Bullet Info")]
    public GameObject lazerPrefab;
    public GameObject[] bulletPrefabs;
    public Transform bulletPos;


    [Header("Skill Info")]
    [SerializeField] private GameObject bossShield;
    [SerializeField] private GameObject freezeWarning;
    [SerializeField] private Transform freezeZone;
    [SerializeField] private GameObject freezeExplosion;
    [SerializeField] private GameObject teleportWarning;
    [SerializeField] private Transform[] teleportPos;

    private FinalBossState bosstate;


    public EntityStateMachine stateMachine { get; private set; }

    [HideInInspector] public float lastTimeAttacked;

    public string lastAnimBoolName { get; private set; }


    float weightAngle = 0;

    protected override void Start()
    {
        base.Start();

        entityMovement = GetComponent<EntityMovment>();
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
        stateMachine.boss_CurState.Update();
    }

    public virtual void AssignLastAnimName(string _animBoolName)
    {
        lastAnimBoolName = _animBoolName;
    }

    public virtual void AnimationFinishTrigger() => stateMachine.boss_CurState.AnimationFinishTrigger();
    public virtual void SkillFinishTrigger() => stateMachine.boss_CurState.SkillFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.CircleCast(transform.position, attackRadius, Vector2.left, 0f, whatIsPlayer);



    public override void SlowEntity(float _slowPercentage, float _slowDuration)
    {
        xVelocity = xVelocity * (1 - _slowPercentage);
        yVelocity = yVelocity * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);


        Invoke("ReturnDefaultSpeed", _slowDuration);
    }

    public override void Die()
    {
        base.Die();

        PlayerManager.Instance.playerStats.AddExperience(exp);
        PlayerManager.Instance.score += score;
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

    public void DestoryBoss() => Destroy(gameObject);

    protected virtual IEnumerator CircleFire()
    {
        int count = 16;
        float interval = 360 / count;

        weightAngle += 30;

        for (int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(bulletPrefabs[0], bulletPos.position, Quaternion.identity);
            BossProjectile bullet = clone.GetComponent<BossProjectile>();
            bullet.SetShooter(this);

            float angle = weightAngle + interval * i;
            float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
            float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
            clone.GetComponent<EntityMovment>().MoveTo(new Vector2(x, y));
        }

        yield return null;

    }
    protected virtual IEnumerator SingleFireToPlayer()
    {

        while (true)
        {
            GameObject clone = Instantiate(bulletPrefabs[0], transform.position, Quaternion.identity);
            BossProjectile bullet = clone.GetComponent<BossProjectile>();
            bullet.SetShooter(this);

            Vector3 dir = (PlayerManager.Instance.player.transform.position - transform.position).normalized;
            clone.GetComponent<EntityMovment>().MoveTo(dir);
            yield return null;
        }
    }
    private IEnumerator BossLazer()
    {
        float lazerduration = .8f;
        while (true)
        {
            Vector3 playerPos = PlayerManager.Instance.player.transform.position;
            Vector3 bossPos = transform.position;

            Vector3 dir = (playerPos - bossPos).normalized;


            for (float t = 0; t < 1; t += Time.deltaTime / lazerduration)
            {
                Vector3 curpos = Vector3.Lerp(bossPos, playerPos, t);

                GameObject lazerclone = Instantiate(lazerPrefab, curpos, Quaternion.identity);
                lazerclone.transform.forward = dir; 

                yield return null;
            }
            yield return null;

        }
    }

    private IEnumerator BossTeleport()
    {
        while (true)
        {
            int ranpos = Random.Range(0, 3);

            yield return new WaitForSeconds(5f);
            Instantiate(teleportWarning, teleportPos[ranpos].transform.position, transform.rotation);
            yield return new WaitForSeconds(.5f);
            transform.position = teleportPos[ranpos].position;
        }
    }


    private IEnumerator ShieldOn()
    {
        while (true)
        {
            float ranTime = Random.Range(4, 7);
            yield return new WaitForSeconds(ranTime);
            bossShield.SetActive(true);
            yield return new WaitForSeconds(4);
            bossShield.SetActive(false);
            yield return new WaitForSeconds(ranTime);
        }
    }
    private IEnumerator FreezeAll()
    {
        while (true)
        {
            float ranTime = Random.Range(2, 6);
            yield return new WaitForSeconds(ranTime);
            GameObject clone = Instantiate(freezeWarning, freezeZone.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            Instantiate(freezeExplosion, clone.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            yield return new WaitForSeconds(ranTime);
        }
    }
    protected virtual IEnumerator BigCircleFire()
    {
        int count = 20;
        float interval = 360 / count;
        float attackRate_circle = 1.5f;
        float weightAngle = 0;
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject clone = Instantiate(bulletPrefabs[0], bulletPos.position, Quaternion.identity);
                BossProjectile bullet = clone.GetComponent<BossProjectile>();
                bullet.SetShooter(this);

                float angle = weightAngle + interval * i;
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                clone.GetComponent<EntityMovment>().MoveTo(new Vector2(x, y));
            }

            weightAngle += 15;
            yield return new WaitForSeconds(attackRate_circle);
        }

    }
    protected virtual IEnumerator SingleFireToPlayer_Corutine()
    {
        float attackRate = 0.3f;

        while (true)
        {
            GameObject clone = Instantiate(bulletPrefabs[0], transform.position, Quaternion.identity);
            BossProjectile bullet = clone.GetComponent<BossProjectile>();
            bullet.SetShooter(this);

            Vector3 dir = (PlayerManager.Instance.player.transform.position - transform.position).normalized;
            clone.GetComponent<EntityMovment>().MoveTo(dir);
            yield return new WaitForSeconds(attackRate);
        }
    }
    private IEnumerator BossLazer_Corutine()
    {
        float lazerduration = .8f;
        float lazerCooldown = 2.0f;
        while (true)
        {
            Vector3 playerPos = PlayerManager.Instance.player.transform.position;
            Vector3 bossPos = transform.position;

            Vector3 dir = (playerPos - bossPos).normalized;


            for (float t = 0; t < 1; t += Time.deltaTime / lazerduration)
            {
                Vector3 curpos = Vector3.Lerp(bossPos, playerPos, t);

                GameObject lazerclone = Instantiate(lazerPrefab, curpos, Quaternion.identity);
                lazerclone.transform.forward = dir;

                yield return null;
            }
            yield return new WaitForSeconds(lazerCooldown);
        }
    }

    // ---------------------------------------------------------------------------------Final Boss ---------------------------------------------------------------------------------

    private IEnumerator Phase01()
    {

        StartFiring(AttackType.BigCircleFire);
        StartFiring(AttackType.SingleFireToPlayer_Corutine);


        while (true)
        {
            if (stats.curHp <= stats.maxHp * 0.8f)
            {
                StopFiring(AttackType.SingleFireToPlayer_Corutine);
                StopFiring(AttackType.BigCircleFire);
                ChangeState(FinalBossState.Phase02);
            }
            yield return null;
        }
    }
    private IEnumerator Phase02()
    {
        StartFiring(AttackType.BossTeleport);
        StartFiring(AttackType.SingleFireToPlayer_Corutine);
        StartFiring(AttackType.ShieldOn);

        while (true)
        {
            if (stats.curHp <= stats.maxHp * 0.6f)
            {
                StopFiring(AttackType.BigCircleFire);
                StopFiring(AttackType.SingleFireToPlayer_Corutine);
                ChangeState(FinalBossState.Phase03);
            }

            yield return null;
        }

    }

    private IEnumerator Phase03()
    {
        StartFiring(AttackType.SingleFireToPlayer_Corutine);
        StartFiring(AttackType.BossLazer_Corutine);
        StartFiring(AttackType.FreezeAll);
        while (true)
        {    
            if (stats.curHp <= stats.maxHp * 0.6f)
            {

                yield return null;
            }

            yield return null;
        }

    }

    public void ChangeState(FinalBossState newState)
    {
        StopCoroutine(bosstate.ToString());
        bosstate = newState;
        StartCoroutine(bosstate.ToString());
    }


    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }
    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }
  

}