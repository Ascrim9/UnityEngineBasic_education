using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Player player;
    public PlayerStats playerStats;

    public int score;

    [Header("Attack Info")]
    public bool isAttacking;
    public float attackRate;

    public int GetScore() => score;

    private void Update()
    {
        PlayerPrefs.SetInt("Score", score);
    }
    public void StartFiring()
    {
        StartCoroutine("TryAttack");
    }
    private IEnumerator TryAttack()
    {

        isAttacking = true;
        AttackByLevel();
        yield return new WaitForSeconds(attackRate);
        isAttacking = false;
    }
    private void AttackByLevel()
    {
        GameObject bulletClone = null;

        ItemData_Equipment curWeapon = Inventory.Instance.GetEquipment(EquipmentType.Weapon);

        if (curWeapon == null)
        {
            Debug.Log("Not Equip Any Weapon !");
            return;
        }


        switch (curWeapon.attackLevel)
        {
            case 1:
                bulletClone = Instantiate(curWeapon.bulletPrefabs[0], player.bullet_Pos.position + Vector3.up * 0.9f, player.bullet_Pos.rotation);
                bulletClone = Instantiate(curWeapon.bulletPrefabs[0], player.bullet_Pos.position + Vector3.down * 0.9f, player.bullet_Pos.rotation);
                break;
            case 2:
                bulletClone = Instantiate(curWeapon.bulletPrefabs[0], player.bullet_Pos.position + Vector3.zero * 0.1f, player.bullet_Pos.rotation);

                bulletClone = Instantiate(curWeapon.bulletPrefabs[0], player.bullet_Pos.position + Vector3.up * 0.9f, player.bullet_Pos.rotation);
                bulletClone = Instantiate(curWeapon.bulletPrefabs[0], player.bullet_Pos.position + Vector3.down * 0.9f, player.bullet_Pos.rotation);
                break; 
            case 3:
                bulletClone = Instantiate(curWeapon.bulletPrefabs[2], player.bullet_Pos.position + Vector3.zero * 0.1f, player.bullet_Pos.rotation);
                                                                                     
                bulletClone = Instantiate(curWeapon.bulletPrefabs[1], player.bullet_Pos.position + Vector3.up * 0.9f, player.bullet_Pos.rotation);
                bulletClone = Instantiate(curWeapon.bulletPrefabs[1], player.bullet_Pos.position + Vector3.down * 0.9f, player.bullet_Pos.rotation);
                break;                                                              
            case 4:                                                                  
                bulletClone = Instantiate(curWeapon.bulletPrefabs[1], player.bullet_Pos.position + Vector3.zero * 0.1f, player.bullet_Pos.rotation);
                                                                                     
                bulletClone = Instantiate(curWeapon.bulletPrefabs[2], player.bullet_Pos.position + Vector3.up * 0.9f, player.bullet_Pos.rotation);
                bulletClone = Instantiate(curWeapon.bulletPrefabs[2], player.bullet_Pos.position + Vector3.down * 0.9f, player.bullet_Pos.rotation);

                bulletClone = Instantiate(curWeapon.bulletPrefabs[3], player.bullet_Pos.position + Vector3.up * 1.5f, player.bullet_Pos.rotation);
                bulletClone = Instantiate(curWeapon.bulletPrefabs[3], player.bullet_Pos.position + Vector3.down * 1.5f, player.bullet_Pos.rotation);
                break;
        }
    }
}
