using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{

    public TextAsset DefaultActions;

    //script for handling the map logic of the game.
    List<ConvMap> ChoicesAvalible;

    public bool Debug;

	// Use this for initialization
	void Start ()
    {
        Space.Connect<DefaultEvent>(Events.ReturnToMap, TurnMapOn);

        Space.Connect<MapEvent>(Events.MapChoiceConfirmed, MapChoice);

        if (!Debug)
            gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void TurnMapOn(DefaultEvent Eventdata)
    {
        gameObject.SetActive(true);
        ChoicesAvalible = TimelineSystem.Current.GetOptionsAvalible(Game.current.Day, Game.current.Hour);
    }

    void MapChoice(MapEvent eventdata)
    {

        for(int i = 0; i < ChoicesAvalible.Count; ++i)
        {
            if(ChoicesAvalible[i].RoomLocation == eventdata.Destination)
            {

                gameObject.SetActive(false);
                Space.DispatchEvent(Events.LeaveMap, new DestinationNodeEvent(ChoicesAvalible[i].ID));

                Game.current.AlterTime(eventdata.Length);
                return;
            }
        }

        //if gets here, no scene was there. send a default sorta dealie

        Space.DispatchEvent(Events.NewStory, new StoryEvent(DefaultActions));

        gameObject.SetActive(false);
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