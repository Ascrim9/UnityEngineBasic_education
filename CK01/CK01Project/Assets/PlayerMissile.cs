using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMissile : Projectile
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 15.0f;
    [SerializeField] private float rotateSpeed = 200.0f;
    Transform followTarget;
    Boss boss;


    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    Transform FindFarthestTarget()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        if (enemies.Length <= 0)
            return null;

        Enemy farthestEnemy = null;
        float farthestDistance = 0.0f;
        for (int i = 0; i < enemies.Length; i++)
        {
            // curState가 DeadState가 아닌 경우에만 고려
            if (!enemies[i].stats.isDie)
            {
                float sqrDistance = Vector3.SqrMagnitude(enemies[i].transform.position - transform.position);
                if (sqrDistance > farthestDistance)
                {
                    farthestDistance = sqrDistance; // 오타 수정
                    farthestEnemy = enemies[i];
                }
            }
        }
        return farthestEnemy != null ? farthestEnemy.transform : null;
    }



    Transform FindNearestTarget()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        if (enemies.Length <= 0)
            return null;

        Enemy nearestEnemy = enemies[0];
        float nearestDistance = float.MaxValue;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].stats.isDie)
            {
                float sqrDistance = Vector3.SqrMagnitude(enemies[i].transform.position - transform.position);
                if (sqrDistance < nearestDistance)
                {
                    sqrDistance = nearestDistance;
                    nearestEnemy = enemies[i];
                }
            }
        }
        return nearestEnemy.transform;
    }

    public void DetermineTarget()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        if (followTarget == null)
        {
            if (Random.Range(0, 100) < 50)
            {
                followTarget = FindFarthestTarget();
            }
            else if (boss != null)
            {
                followTarget = boss.transform;
            }
            else
            {
                followTarget = FindNearestTarget();
            }
        }
    }

    protected override void Update()
    {
        DetermineTarget();
        if (followTarget != null)
        {
            Vector2 dir = (followTarget.transform.position - transform.position);
            dir.Normalize();
            float rotateAmount = Vector3.Cross(dir, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * speed;
        }
        else
        {
            Destroy(gameObject, 0.2f);
        }
    }
}
