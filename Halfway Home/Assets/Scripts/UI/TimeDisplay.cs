/******************************************************************************/
/*!
File:   TimeDisplay.cs
Author: Jesse Lozano & John Myres
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
  public TextMeshProUGUI TimeText;
  public Image Day;
  public Image Loop;

  public Sprite[] Days;

  void Start ()
    {
       Space.Connect<DefaultEvent>(Events.TimeChange, UpdateDisplay);

       UpdateDisplay(new DefaultEvent());

	  }

  void UpdateDisplay(DefaultEvent eventdata)
    {
      // Time Display
      if (Game.current.Hour == 0)
      {
          TimeText.text = "12:00<sub>AM</sub>";
      }
      else if(Game.current.Hour < 12)
      {
          TimeText.text = Game.current.Hour + ":00<sub>AM</sub>";
      }
      else if(Game.current.Hour == 12)
      {
          TimeText.text = "12:00<sub>PM</sub>";
      }
      else
      {
          TimeText.text = (Game.current.Hour - 12) + ":00<sub>PM</sub>";
      }
      // Day Display
      Day.sprite = Days[Game.current.Day  - 1];
      // Week Display
      if(Game.current.Progress.GetIntValue("week")==1) Loop.gameObject.SetActive(false);
      else
      {
        Loop.gameObject.SetActive(true);
        Loop.GetComponentInChildren<TextMeshProUGUI>().text = (Game.current.Progress.GetIntValue("week") - 1).ToString();
      }
    }

}
