using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{

    public bool Hour;
    public bool Day;

    TextMeshProUGUI txt;

	// Use this for initialization
	void Start ()
    {
        txt = GetComponent<TextMeshProUGUI>();

        Space.Connect<DefaultEvent>(Events.ReturnToMap, UpdateDisplay);

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void UpdateDisplay(DefaultEvent eventdata)
    {
        if (Hour)
            txt.text = Game.current.Hour + "";
        else if (Day)
            txt.text = Game.current.Day + "";
    }

}
