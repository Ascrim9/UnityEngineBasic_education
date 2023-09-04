using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCamera : MonoBehaviour
{
    [SerializeField] bool XZAxis = true;

    private void Update()
    {
        if (XZAxis)
        {
            transform.rotation = Quaternion.Euler(0.0f, Camera.main.transform.rotation.eulerAngles.y, 0.0f);
        }
    }
}
