using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideUIDisplay : MonoBehaviour
{

    bool UIOn = true;

	// Use this for initialization
	void Start ()
    {
		


	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetMouseButtonDown(1) == true)
        {
            if (UIOn)
                TurnUIOff();
            else
                TurnUIOn(); 
        }

        if(!UIOn)
        {
            if (Input.GetMouseButtonDown(0) == true)
            {
                TurnUIOn();
            }
        }
    }


    void TurnUIOn()
    {
        var gImages = gameObject.GetComponentsInChildren<Graphic>();
        UIOn = true;
        foreach (var obj in gImages)
        {
            obj.enabled = true;
        }
        Space.DispatchEvent(Events.OpenUI);
    }

    void TurnUIOff()
    {
        var gImages = gameObject.GetComponentsInChildren<Graphic>();
        UIOn = false;
        foreach (var obj in gImages)
        {
            obj.enabled = false;
        }
        Space.DispatchEvent(Events.CloseUI);
    }
}
