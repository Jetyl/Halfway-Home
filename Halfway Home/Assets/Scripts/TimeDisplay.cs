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

    UpdateDisplay(new DefaultEvent());

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void UpdateDisplay(DefaultEvent eventdata)
    {
        if (Hour)
        {
            string Txt = Game.current.Hour + ":00";

            if (Game.current.Hour < 12)
            {
                if (Game.current.Hour == 0)
                    Txt = "12:00 AM";
                else
                    Txt = Game.current.Hour + ":00 AM";

            }
            else
            {
                if (Game.current.Hour == 12)
                    Txt = "12:00 PM";
                else
                    Txt = (Game.current.Hour - 12) + ":00 PM";
            }
            txt.text = Txt;
        }
        else if (Day)
            txt.text = Game.current.Day + "";
    }

}
