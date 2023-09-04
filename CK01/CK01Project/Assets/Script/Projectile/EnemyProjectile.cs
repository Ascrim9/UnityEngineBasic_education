using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Enemy shooterEnemy;

    public void SetShooter(Enemy enemy)
    {
        shooterEnemy = enemy;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            PlayerStats _target = collision.GetComponent<PlayerStats>();

            if (_target != null)
            {
                shooterEnemy.stats.DoDamage(_target);
                Destroy(gameObject);
            }
        }

    }
}
