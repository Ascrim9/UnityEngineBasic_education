using UnityEngine;

public class Entity : MonoBehaviour
{


    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public CapsuleCollider2D capsuleCollider2D { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public CharacterStats stats { get; private set; }
    #endregion


    [Header("Move Info")]
    public Vector3 movedir = Vector3.zero;



    public System.Action onFlipped;

    protected virtual void Awake()
    {


    }

    protected virtual void Start()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        stats = GetComponent<CharacterStats>();
    }

    protected virtual void Update()
    {

    }

    public virtual void SlowEntity(float _slowPercentage, float _slowDuration)
    {

    }
    protected virtual void ReturnDefaultSpeed()
    {
        anim.speed = 1;
    }


    protected virtual void OnDrawGizmos()
    {

    }

    public void ZeroVelocity()
    {

        rb.velocity = Vector2.zero;
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }


    public virtual void Die()
    {

    }

}
