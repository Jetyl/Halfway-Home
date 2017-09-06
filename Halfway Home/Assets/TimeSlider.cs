using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlider : MonoBehaviour
{

    Room Location;
    
    public Slider TimeSpendingThere;

    public TextMeshProUGUI Txt;

	// Use this for initialization
	void Start ()
    {
        Space.Connect<MapEvent>(Events.MapChoiceMade, GotToRoom);
	}
	
	// Update is called once per frame
	void Update ()
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
        //TimeSpendingThere.value = EventData.Length;
        AssignText(Location);
        //turn on visiblity
        gameObject.SetActive(true);

    }


    public void ConfirmDestination()
    {
        Space.DispatchEvent(Events.MapChoiceConfirmed, new MapEvent(Location, 1));

        gameObject.SetActive(false);
    }


    public void AssignText(Room value)
    {
        Txt.text = "Go to " + value + "?";

    }


}
