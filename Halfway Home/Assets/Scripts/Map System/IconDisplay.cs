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

    List<CharacterList> locations;

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
        var scenes = TimelineSystem.Current.GetOptionsAvalible(Game.current.Day, Game.current.Hour);
        
        //go thru every room
        for (var i = 0; i < Enum.GetValues(typeof(Room)).Length; ++i)
        {
            if (i == (int)Room.None) //if none, skip
                continue;

            if ((Room)i == Room.Sleeping) //if sleping, activate sleeping icon, and move on
            {
                foreach (JsonData character in schedules)
                {
                    if ((Room)(int)character["Schedule"][Game.current.Day][Game.current.Hour] == (Room)i)
                    {
                         Space.DispatchEvent(Events.SleepIcon, new CharacterEvent((string)character["Name"]));
                    }
                    else
                    {
                        //decativating all other sleep icons
                        Space.DispatchEvent(Events.AwakeIcon, new CharacterEvent((string)character["Name"]));
                    }
                }
            }

            var icons = new MapIconEvent((Room)i);

            foreach(ConvMap scene in scenes) //for every current scene availbe
            {
                if (scene.RoomLocation != (Room)i)
                    continue;

                foreach (JsonData character in schedules) //go thru each character
                {
                    if (!scene.Characters.Contains((string)character["Name"])) //are they there?
                        continue;

                    //has the player seen this scene already?
                    if (scene.WatchedScene() == false)
                    {
                        icons.Icons.Add(UnknownPersonSprite);
                    }
                    else if (character["slug"] != null)
                    {
                        var slug = (string)character["slug"];
                        icons.Icons.Add(Resources.Load<Sprite>("Sprites/" + slug));
                    }
                }
                
            }
            
            //print("Room to call: " + icons.CurrentRoom);

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
