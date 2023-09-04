using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovment : MonoBehaviour
{
    public float movespeed = 1.0f;
    [SerializeField] private Vector3 movedir;

    private void Update()
    {
        transform.position += movespeed * movedir * Time.deltaTime;
    }
    public void MoveTo(Vector3 dir)
    {
        movedir = dir;
    }
}
