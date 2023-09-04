using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace AEA
{
    public class BackgroundScoller : MonoBehaviour
    {
        [SerializeField] private Vector3 movedir = Vector3.zero;
        [SerializeField] private float speed = 3.0f;
        [SerializeField] private Transform target;
        [SerializeField] private float scollerRange = 0.0f;
        private void LateUpdate()
        {
            ParallaxEffect();
        }

        private void ParallaxEffect()
        {
            transform.position += movedir * speed * Time.deltaTime;
            
            if (transform.position.x <= -scollerRange && target != null)
            {
                transform.position = target.position + Vector3.right * scollerRange;
            }
        }
    }
}
