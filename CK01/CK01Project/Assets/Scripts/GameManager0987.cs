using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager0987 : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Quit();
		}
	}

	public void Quit()
	{
		Application.Quit();
	}



    [SerializeField] private PlayableDirector PlayTimeline;

    public void Scene03Player()
    {
        PlayTimeline.Play();


    }


    //public TextDashh textReveal;

    //// Call this method when the event occurs
    //public void OnEventTriggered()
    //{
    //    // Start revealing the text
    //    textReveal.StartReveal();
    //}
}
