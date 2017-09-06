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

        Space.Connect<MapEvent>(Events.MapChoiceConfirmed, MapChoice);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void TurnMapOn(DefaultEvent Eventdata)
    {
        ChoicesAvalible = TimelineSystem.Current.GetOptionsAvalible(Game.current.Day, Game.current.Hour);
    }

    void MapChoice(MapEvent eventdata)
    {

        foreach(var option in ChoicesAvalible)
        {
            if(option.RoomLocation == eventdata.Destination)
            {
                Space.DispatchEvent(Events.LeaveMap, new DestinationNodeEvent(option.ID));

                Game.current.AlterTime(eventdata.Length);
                return;
            }
        }

        //if gets here, no scene was there. send a default sorta dealie

        Game.current.AlterTime(eventdata.Length);
    }
    


}


public class MapEvent: DefaultEvent
{

    public Room Destination;
    public int Length;

    public MapEvent(Room spot, int timeThere)
    {
        Destination = spot;
        Length = timeThere;
    }

}