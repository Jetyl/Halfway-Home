using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        
        Space.Connect<DefaultEvent>(Events.Pause, OnPause);
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnPause(DefaultEvent eventdata)
    {
        gameObject.SetActive(true);
    }

    public void Continue()
    {
        Space.DispatchEvent(Events.UnPause);
    }

    public void Quit()
    {
        SaveLoad.Save();
        Application.Quit();
    }

}
