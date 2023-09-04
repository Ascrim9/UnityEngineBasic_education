using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Scene01 : MonoBehaviour
{
    [SerializeField] private PlayableDirector PlayTimeline;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayTimeline.Play();
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
