using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Scene02 : MonoBehaviour
{
    [SerializeField] private PlayableDirector PlayTimeline;

    private bool A = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(A)
        {
            if (collision.CompareTag("Player"))
                PlayTimeline.Play();
            A = false;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Flag");
        if (A)
        {
            if (other.CompareTag("Player"))
                PlayTimeline.Play();
            A = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
