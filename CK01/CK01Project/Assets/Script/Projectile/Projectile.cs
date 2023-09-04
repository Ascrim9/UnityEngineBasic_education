using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    public float bulletSpeed;

    protected override void Update()
    {
        base.Update();

        this.SetVelocity(bulletSpeed, 0);

    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            EnemyStats _target = collision.GetComponent<EnemyStats>();

            if(_target != null)
            {
                PlayerManager.Instance.player.stats.DoDamage(_target);
                Destroy(gameObject);
            }
          
            ItemData_Equipment weaponData = Inventory.Instance.GetEquipment(EquipmentType.Weapon);
            
            if (weaponData != null)
                weaponData.Effect(_target.transform);
        }

        else if(collision.GetComponent<Boss>() != null)
        {
            BossStats _target = collision.GetComponent<BossStats>();

            if (_target != null)
            {
                PlayerManager.Instance.player.stats.DoDamage(_target);
                Destroy(gameObject);
            }

            ItemData_Equipment weaponData = Inventory.Instance.GetEquipment(EquipmentType.Weapon);

            if (weaponData != null)
                weaponData.Effect(_target.transform);
        }
    }

}