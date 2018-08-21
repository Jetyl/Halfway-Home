/******************************************************************************/
/*!
File:   OpenPauseMenu.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPauseMenu : MonoBehaviour
{
   public PauseMenu MenuObject;
   bool Paused;
    public Button SaveButton;
    public Button LoadButton;

    bool Disabled;

	// Use this for initialization
	void Start ()
    {
        Space.Connect<DefaultEvent>(Events.Pause, OnPause);
        Space.Connect<DefaultEvent>(Events.UnPause, OnUnPause);
        Space.Connect<DefaultEvent>(Events.Debug, OnDebug);

        Space.Connect<DefaultEvent>(Events.ReturnToMap, OnDisablePause);
        Space.Connect<DefaultEvent>(Events.MapTransitionInCompleted, OnEnablePause);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Disabled)
            return;

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
        MenuObject.Back();

    // test audio thing
    }

   void OnUnPause(DefaultEvent eventdata)
    {
        this.Paused = false;
        this.MenuObject.gameObject.SetActive(false);
    }

    void OnDisablePause(DefaultEvent eventdata)
    {
        this.Disabled = true;
    }

    void OnEnablePause(DefaultEvent eventdata)
    {
        this.Disabled = false;
    }

    void OnDebug(EventData eventdata)
    {
        LoadButton.interactable = false;
        SaveButton.interactable = false;
    }
}
