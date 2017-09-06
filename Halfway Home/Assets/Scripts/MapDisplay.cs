using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    //script for handling the map logic of the game.
    List<ConvMap> ChoicesAvalible;

	// Use this for initialization
	void Start ()
    {
        Space.Connect<DefaultEvent>(Events.ReturnToMap, TurnMapOn);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void TurnMapOn(DefaultEvent Eventdata)
    {
        ChoicesAvalible = TimelineSystem.Current.GetOptionsAvalible(Game.current.Day, Game.current.Hour);
    }

}
