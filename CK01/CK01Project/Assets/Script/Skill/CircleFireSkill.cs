using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFire_Skill : Skill
{
    [Header("Skill Info")]
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float projectileGravity;


    private Vector2 finalDir;


    [Header("Aim dots")]
    [SerializeField] private float distanceFromDots = 0;
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBwnDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private Vector2 mousePos;


    public bool usingSkill;



   [HideInInspector] public GameObject[] dots;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }


    protected override void Start()
    {
        base.Start();


        GenerateDots();
    }
    protected override void Update()
    {
        base.Update();

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyUp(KeyCode.Z))
        {
            usingSkill = false;
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);

        }


        if (Input.GetKey(KeyCode.Z))
        {
            usingSkill = true;
            for (int i =0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBwnDots);
            }
        }
    }

    public void CreateProjectile()
    {
        GameObject newProjectile = Instantiate(firePrefab, player.bullet_Pos.position, player.bullet_Pos.rotation);
        CircleFire_Skill_Controller newProjectileScript = newProjectile.GetComponent<CircleFire_Skill_Controller>();

        newProjectileScript.SetupProjectile(finalDir, projectileGravity);

        DotsActive(false);
    }

    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 dir = mousePos - playerPosition;

        return dir;
    }


    public void DotsActive(bool _isActive)
    {
        for(int i =0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for(int i =0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * projectileGravity) * (t * t);

        return position;
    }

}
