using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Space.Connect<DefaultEvent>(Events.Pause, OnPause);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnPause(DefaultEvent eventdata)
    {
        gameObject.SetActive(true);
    }

    void Continue()
    {
        Space.DispatchEvent(Events.UnPause);
    }

    void Quit()
    {
        SaveLoad.Save();
        Application.Quit();
    }

}
