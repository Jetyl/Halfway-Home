/******************************************************************************/
/*!
File:   RoomIconDisplay.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomIconDisplay : MonoBehaviour
{

    public Room Location;
    

    List<Image> IconSpots;

	// Use this for initialization
	void Start ()
    {

        IconSpots = new List<Image>();

        for(int i = 0; i < transform.childCount; ++i)
        {
            var spot = transform.GetChild(i).GetComponent<Image>();
            if (spot != null)
                IconSpots.Add(spot);
        } 

        Space.Connect<DefaultEvent>(Events.ReturnToMap, ClearIcons);
        Space.Connect<MapIconEvent>(Events.MapIcon, PlaceIcon);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void ClearIcons(DefaultEvent Eventdata)
    {

        foreach(Image spot in IconSpots)
        {
            spot.sprite = null;
            var colo = spot.color;
            colo.a = 0;
            spot.color = colo;
        }
        
    }

    void PlaceIcon(MapIconEvent Eventdata)
    {
        if (Eventdata.CurrentRoom != Location)
            return;

        int i = Random.Range(0, IconSpots.Count);

        while(IconSpots[i].sprite != null)
        {
            i = Random.Range(0, IconSpots.Count);
        }

        IconSpots[i].sprite = Eventdata.Icon;

        var colo = IconSpots[i].color;
        colo.a = 1;
        IconSpots[i].color = colo;
    }

}
