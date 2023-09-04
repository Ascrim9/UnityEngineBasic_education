using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "thunder strike Effect", menuName = "Data/Item Effect/Thunder strike")]
public class ThunderStike_Effect : ItemEffect
{
    [SerializeField] private GameObject thunderStrikePrefab;
    public override void ExecuteEffect(Transform _enemyPos)
    {
        GameObject newThunderStrike = Instantiate(thunderStrikePrefab, _enemyPos.position, Quaternion.identity);

        Destroy(newThunderStrike, 1);
    }
}
