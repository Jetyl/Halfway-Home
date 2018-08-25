/******************************************************************************/
/*!
File:   DisableEnableDisplay.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableDisplay : MonoBehaviour
{
    bool ToMap = false;

    // Use this for initialization
    void Start ()
    {
        Space.Connect<DefaultEvent>(Events.ReturnToMap, Disable);
        Space.Connect<DefaultEvent>(Events.Description, Enable);
        Space.Connect<DefaultEvent>(Events.TimeChange, CloseDisplay);
        Space.Connect<DefaultEvent>(Events.ClockFinished, OpenDisplay);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    

    void Enable(DefaultEvent eventdata)
    {
        ToMap = false;
        //gameObject.SetActive(true);
    }

    void Disable(DefaultEvent eventdata)
    {
        ToMap = true;
        gameObject.SetActive(false);

    }

    void CloseDisplay(DefaultEvent eventdata)
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(WaitTilClosed());
    }
    void OpenDisplay(DefaultEvent eventdata)
    {
        if (!ToMap)
        {
            ToMap = false;
            gameObject.SetActive(true);
            StartCoroutine(WaitTilOpened());
        }
        
    }

    IEnumerator WaitTilClosed()
    {
        yield return new WaitForSeconds(Time.deltaTime);

        gameObject.SetActive(false);

    }

    IEnumerator WaitTilOpened()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        

    }

}
