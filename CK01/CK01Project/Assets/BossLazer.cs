using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLazer : MonoBehaviour
{
    public int dmg = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            PlayerStats _target = collision.GetComponent<PlayerStats>();

            if (_target != null)
            {
                _target.TakeDamage(dmg);
                Destroy(gameObject);
            }
        }

    }
}
