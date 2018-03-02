/******************************************************************************/
/*!
File:   IconDisplay.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
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

        Space.Connect<DefaultEvent>(Events.UpdateMap, TurnMapOn);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void TurnMapOn(DefaultEvent Eventdata)
    {
        
        for (var i = 0; i < Enum.GetValues(typeof(Room)).Length; ++i)
        {
            if (i == (int)Room.None)
                continue;
            var icons = new MapIconEvent((Room)i);

            foreach (JsonData character in schedules)
            {
                if ((Room)(int)character["Schedule"][Game.current.Day][Game.current.Hour] == (Room)i)
                {
                    if ((Room)i == Room.Sleeping) //if sleping, activate sleeping icon, and move on
                    {
                        Space.DispatchEvent(Events.SleepIcon, new CharacterEvent((string)character["Name"]));
                    }
                    else
                    {
                        Space.DispatchEvent(Events.AwakeIcon, new CharacterEvent((string)character["Name"]));

                        //Sprite icon = null;
                        if(Game.current.KnowsWhereAbouts((string)character["Name"]) == false)
                        {
                            icons.Icons.Add(UnknownPersonSprite);
                        }
                        else if (character["slug"] != null)
                        {
                            var slug = (string)character["slug"];
                            icons.Icons.Add(Resources.Load<Sprite>("Sprites/" + slug));
                        }
                        //StartCoroutine(TextParser.FrameDelay(Events.MapIcon, new MapIconEvent((Room)i, icon)));
                        //Space.DispatchEvent(Events.MapIcon, new MapIconEvent((Room)i, icon));
                    }
                }
            }
            print("Room to call: " + icons.CurrentRoom);

            Space.DispatchEvent(Events.MapIcon, icons);
        }

    }
}


public class MapIconEvent : DefaultEvent
{
    public Room CurrentRoom;

    public List<Sprite> Icons;

    public MapIconEvent(Room location)
    {
        CurrentRoom = location;
        Icons = new List<Sprite>();
    }

}