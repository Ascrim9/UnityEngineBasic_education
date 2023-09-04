using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Scene03 : MonoBehaviour
{
    [SerializeField] private PlayableDirector PlayTimeline;

    [SerializeField] private GameObject GoOne;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TimeLineLoad()
    {
        yield return new WaitForSeconds(11.0f);

        Scene03Player();
    }

    public void Scene03Player()
    {
        PlayTimeline.Play();
    }
}
