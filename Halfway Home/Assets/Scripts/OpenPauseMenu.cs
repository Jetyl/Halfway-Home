using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK;

public class OpenPauseMenu : MonoBehaviour
{
   public PauseMenu MenuObject;
   bool Paused;
   public AkAmbient HackySoundThing;

	// Use this for initialization
	void Start ()
    {
        Space.Connect<DefaultEvent>(Events.Pause, OnPause);
        Space.Connect<DefaultEvent>(Events.UnPause, OnUnPause);
	  }
	
	// Update is called once per frame
	void Update ()
    {
		  if(Input.GetButtonDown("Pause") && !Paused)
        {
            Space.DispatchEvent(Events.Pause);
        }
      else if(Input.GetButtonDown("Pause"))
        {
            Space.DispatchEvent(Events.UnPause);
        }
	  }

    void OnPause(DefaultEvent eventdata)
    {
        this.Paused = true;
        this.MenuObject.gameObject.SetActive(true);
    // test audio thing
    }

   void OnUnPause(DefaultEvent eventdata)
    {
        this.Paused = false;
        this.MenuObject.gameObject.SetActive(false);
    }

}
