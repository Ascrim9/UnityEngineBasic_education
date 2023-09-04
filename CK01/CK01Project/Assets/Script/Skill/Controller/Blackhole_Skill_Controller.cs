using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill_Controller : MonoBehaviour
{



    public float maxSize;
    public float growSpeed;
    public float shinkSpeed;
    private float blackholeTimer;

    private bool canGrow = true;
    private bool canShrink;
    private bool cloneAttackReleased;
    private bool playerCanDisapear = true;

    private int amountOfAttacks = 4;
    private float cloneAttackCooldown = 0.3f;
    private float cloneAttackTimer;


    public List<Transform> targets = new List<Transform>();


    public bool PlayerCanExitState { get; private set; }

    public void SetUpBlackHole(float _maxSize, float _growSpeed, float _shinkSpeed, int _amountOfAttacks, float _cloneAttackColldown, float _blackholeTimer)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        shinkSpeed = _shinkSpeed;
        amountOfAttacks = _amountOfAttacks;
        cloneAttackCooldown = _cloneAttackColldown;
        blackholeTimer = _blackholeTimer;
    }


    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;
        blackholeTimer -= Time.deltaTime;

        if(blackholeTimer < 0)
        {
            blackholeTimer = Mathf.Infinity;

            if (targets.Count > 0)
                ReleaseCloneAttack();
            else
                FinishBlackHoleAbility();

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseCloneAttack();

        }

        CloneAttackLogic();

        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shinkSpeed * Time.deltaTime);

            if (transform.localScale.x < 0)
                Destroy(gameObject);
        }

    }

    private void ReleaseCloneAttack()
    {
        if (targets.Count <= 0)
            return;

        cloneAttackReleased = true;

        if(playerCanDisapear)
        {
            playerCanDisapear = false;
            PlayerManager.Instance.player.fx.MakeTransprent(true);
        }
      
    }

    private void CloneAttackLogic()
    {
        if (cloneAttackTimer < 0 && cloneAttackReleased && amountOfAttacks > 0)
        {
            cloneAttackTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);

            SkillManager.Instance.clone.CreateBoomClone(0,targets[randomIndex], new Vector3(0, 0));
            amountOfAttacks--;

            if (amountOfAttacks <= 0)
            {
                Invoke("FinishBlackHoleAbility", 1f);
            }

        }
    }

    private void FinishBlackHoleAbility()
    {
        PlayerCanExitState = true;
        canShrink = true;
        cloneAttackReleased = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
            Destroy(collision.gameObject);

        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().Die();
        }
    }
    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);
}
