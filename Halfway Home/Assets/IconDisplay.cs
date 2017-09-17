using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

public class IconDisplay : MonoBehaviour
{

    JsonData schedules;

    public Sprite UnknownPersonSprite;

	// Use this for initialization
	void Start ()
    {

        schedules = TextParser.ToJson("Characters");

        Space.Connect<DefaultEvent>(Events.ReturnToMap, TurnMapOn);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void TurnMapOn(DefaultEvent Eventdata)
    {
        
        foreach (JsonData character in schedules)
        {
            for (var i = 0; i < Enum.GetValues(typeof(Room)).Length; ++i)
            {
                if (i == (int)Room.None)
                    continue;
                
                
                if ((Room)(int)character["Schedule"][Game.current.Day][Game.current.Hour] == (Room)i)
                {
                    if ((Room)i == Room.Sleeping) //if sleping, activate sleeping icon, and move on
                    {
                        Space.DispatchEvent(Events.SleepIcon, new CharacterEvent((string)character["Name"]));
                    }
                    else
                    {
                        Space.DispatchEvent(Events.AwakeIcon, new CharacterEvent((string)character["Name"]));

                        Sprite icon = null;
                        if(Game.current.KnowsWhereAbouts((string)character["Name"]) == false)
                        {
                            icon = UnknownPersonSprite;
                        }
                        else if (character["slug"] != null)
                        {
                            var slug = (string)character["slug"];
                            icon = Resources.Load<Sprite>("Sprites/" + slug);
                        }
                        Space.DispatchEvent(Events.MapIcon, new MapIconEvent((Room)i, icon));
                    }
                }
                
            }
        }

    }
}


public class MapIconEvent : DefaultEvent
{
    public Room CurrentRoom;

    public Sprite Icon;

    public MapIconEvent(Room location, Sprite icon)
    {
        CurrentRoom = location;
        Icon = icon;
    }

}