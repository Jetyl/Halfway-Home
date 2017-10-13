using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPauseMenu : MonoBehaviour
{
    bool Paused;

	// Use this for initialization
	void Start ()
    {
        Space.Connect<DefaultEvent>(Events.UnPause, OnUnPause);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Paused)
            return;
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            Space.DispatchEvent(Events.Pause);
        }
	}

    void OnUnPause(DefaultEvent eventdata)
    {
        Paused = false;
    }

}
