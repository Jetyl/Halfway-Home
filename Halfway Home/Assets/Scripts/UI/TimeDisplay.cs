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
  public TextMeshProUGUI AMPMText;
  public Image Day;

  public Sprite[] Days;

  void Start ()
    {
       Space.Connect<DefaultEvent>(Events.TimeChange, UpdateDisplay);

       UpdateDisplay(new DefaultEvent());

	  }

  void UpdateDisplay(DefaultEvent eventdata)
    {
      if (Game.current.Hour == 12)
      {
          TimeText.text = "12:00";
          AMPMText.text = "PM";
      }
      else if(Game.current.Hour < 12)
      {
          TimeText.text = Game.current.Hour + ":00";
          AMPMText.text = "AM";
      }
      else
      {
          TimeText.text = (Game.current.Hour - 12) + ":00";
          AMPMText.text = "PM";

      }

      Day.sprite = Days[Game.current.Day  - 1];
    }

}
