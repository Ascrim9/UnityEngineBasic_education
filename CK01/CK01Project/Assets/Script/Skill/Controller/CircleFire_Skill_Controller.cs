using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFire_Skill_Controller : MonoBehaviour
{
    public int defaultSkillDmg;

    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetupProjectile(Vector2 _dir, float _gravityScale)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Enemy>() != null)
        {
            PlayerManager.Instance.player.stats.DoMagicalDamage(collision.GetComponent<CharacterStats>(), defaultSkillDmg);

            ItemData_Equipment equipAmulet = Inventory.Instance.GetEquipment(EquipmentType.Amulet);


            if (equipAmulet != null)
                equipAmulet.Effect(collision.transform);
        }
        else if (collision.GetComponent<Boss>() != null)
        {
            PlayerManager.Instance.player.stats.DoMagicalDamage(collision.GetComponent<CharacterStats>(), defaultSkillDmg);

            ItemData_Equipment equipAmulet = Inventory.Instance.GetEquipment(EquipmentType.Amulet);


            if (equipAmulet != null)
                equipAmulet.Effect(collision.transform);
        }


    }
}
