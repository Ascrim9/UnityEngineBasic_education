using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Boss shooter;

    public void SetShooter(Boss boss)
    {
        shooter = boss;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            PlayerStats _target = collision.GetComponent<PlayerStats>();

            if (_target != null)
            {
                shooter.stats.DoDamage(_target);
                Destroy(gameObject);
            }
        }

    }
}
