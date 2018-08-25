/******************************************************************************/
/*!
File:   TimeSlider.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HalfwayHome
{

  public class TimeSlider : MonoBehaviour
  {

    Room Location;

    public Slider TimeSpendingThere;

    public TextMeshProUGUI Txt;

    public int Time;

    bool DrainEnergy;

    // Use this for initialization
    void Start()
    {
      Space.Connect<MapEvent>(Events.MapChoiceMade, GotToRoom);
      gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSlider()
    {
      //AssignText((int)TimeSpendingThere.value);
    }


    public void GotToRoom(MapEvent EventData)
    {

      if (Location == EventData.Destination && gameObject.activeSelf)
      {
        Location = Room.None;
        gameObject.SetActive(false);
        return;
      }

      Location = EventData.Destination;
      Time = EventData.Length;
      DrainEnergy = EventData.DrainEnergy;
      AssignText(Location);
      //turn on visiblity
      gameObject.SetActive(true);

    }


    public void ConfirmDestination()
    {
      Space.DispatchEvent(Events.MapChoiceConfirmed, new MapEvent(Location, Time, DrainEnergy));

      gameObject.SetActive(false);
    }


    public void AssignText(Room value)
    {
            string room = "";
            switch(value)
            {
                case Room.ArtRoom:
                    room = "the Art Room";
                    break;
                case Room.CharlottesRoom:
                    room = "Charlotte's Room";
                    break;
                case Room.Commons:
                    room = "the Commons Area";
                    break;
                case Room.Garden:
                    room = "the Garden";
                    break;
                case Room.Kitchen:
                    room = "the Cafe";
                    break;
                case Room.Library:
                    room = "the Library";
                    break;
                case Room.Store:
                    room = "the Store";
                    break;
                case Room.YourRoom:
                    room = "Your Room";
                    break;
            }

            Txt.text = "Go to " + room + "?";

    }


  }

}