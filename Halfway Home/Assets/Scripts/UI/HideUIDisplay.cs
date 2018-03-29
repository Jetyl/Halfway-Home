using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideUIDisplay : MonoBehaviour
{

    bool UIOn = true;

  public GameObject[] IgnoredObjects;
  private List<Graphic> activeUIElements = new List<Graphic>();

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
        UIOn = true;
        foreach (var obj in activeUIElements)
        {
            obj.enabled = true;
            if(obj.GetComponent<Button>() != null) obj.GetComponent<Button>().enabled = true;
        }
        Space.DispatchEvent(Events.OpenUI);
    }

    void TurnUIOff()
    {
        var gImages = gameObject.GetComponentsInChildren<Graphic>();
        UIOn = false;
        foreach (var obj in gImages)
        {
          bool ignore = false;
          foreach (GameObject g in IgnoredObjects)
          {
            if(g == obj.gameObject) ignore = true;
          }
          if (obj.gameObject.activeSelf & obj.enabled && !ignore)
          {
            obj.enabled = false;
            if(obj.GetComponent<Button>() != null) obj.GetComponent<Button>().enabled = false;
            activeUIElements.Add(obj);
          }
        }
        Space.DispatchEvent(Events.CloseUI);
    }
}
