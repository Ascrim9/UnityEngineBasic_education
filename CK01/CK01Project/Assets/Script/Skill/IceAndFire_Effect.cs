using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ice And Fire Effect", menuName = "Data/Item Effect/Ice And Fire")]
public class IceAndFire_Effect : ItemEffect
{
    [SerializeField] private GameObject iceAndFirePrefab;
    [SerializeField] private Vector2 newVelocity;

    public override void ExecuteEffect(Transform _respawnPos)
    {
        Transform player = PlayerManager.Instance.player.transform;

        GameObject newIceAndFire = Instantiate(iceAndFirePrefab, _respawnPos.position, player.transform.rotation);

        newIceAndFire.GetComponent<Rigidbody2D>().velocity = newVelocity;  
    }
}
