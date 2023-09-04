using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{

    [Header("Clone Info")]
    [SerializeField] private GameObject[] clonePrefab;
    [SerializeField] private float cloneDuration;

    [Space]
    [SerializeField] private bool createCloneOnDashStart;
    [SerializeField] private bool createCloneOnDashOver;
    

    public void CreateBoomClone(int _cloneIndex,Transform _clonePosition, Vector3 _offset)
    {
        GameObject newClone = Instantiate(clonePrefab[_cloneIndex]);


        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition, cloneDuration, _offset);
    }

    public void CreateCloneOnDashStart()
    {
        if (createCloneOnDashStart)
            CreateBoomClone(1,player.transform, Vector3.zero);
    }

    public void CreateCloneOnDashOver()
    {
        if (createCloneOnDashOver)
            CreateBoomClone(1,player.transform, Vector3.zero);
    }
}
