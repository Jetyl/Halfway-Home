using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Continue()
    {
        Space.DispatchEvent(Events.UnPause);
    }

    public void Quit()
    {
        SaveLoad.Save();
        print("on");
        Application.Quit();
    }

}
