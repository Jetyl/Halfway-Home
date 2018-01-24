/******************************************************************************/
/*!
File:   StageDisplay.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageDisplay : MonoBehaviour
{
    
    public List<RoomDetails> Backdrop;
    
    public List<RoomDetails> SpecialBackdrops;

    private Room CurrentRoom = Room.None;
    private string CurrentCG = "";

    public SpriteRenderer FrontCurtain;
    public SpriteRenderer BackCuratin;
   
    public Room StartingRoom;
    public float BackgroundFadeTime = 2;

    bool Load = false;

    bool Skip = false;

	// Use this for initialization
	void Start ()
    {
        
        Space.Connect<StageDirectionEvent>(Events.Backdrop, SceneryChange);

        Space.Connect<DefaultEvent>(Events.SkipTyping, OnSkip);
        Space.Connect<DefaultEvent>(Events.StopSkipTyping, OffSkip);

        Space.Connect<DefaultEvent>(Events.Save, OnSave);
        Space.Connect<DefaultEvent>(Events.Load, OnLoad);

        if (Load == false)
            SceneryChange(new StageDirectionEvent(StartingRoom));

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnSkip(DefaultEvent eventdata)
    {
        Skip = true;
        StopAllCoroutines();
    }

    void OffSkip(DefaultEvent eventdata)
    {
        Skip = false;
    }

    void OnSave(DefaultEvent eventdata)
    {
        
    }

    void OnLoad(DefaultEvent eventdata)
    {
        Load = true;

        
        SceneryChange(new StageDirectionEvent(Game.current.CurrentRoom));
    }

    

    void SceneryChange(StageDirectionEvent eventdata)
    {
        if(eventdata.Backdrop == CurrentRoom)
        {
            if (eventdata.Backdrop != Room.None || eventdata.character == CurrentCG)
                return;
        }
        
        if(eventdata.Backdrop == Room.None)
        {

            CurrentRoom = Room.None;
            CurrentCG = eventdata.character;

            foreach (var room in SpecialBackdrops)
            {
                if (room.Tag == CurrentCG)
                {
                    if (!Skip)
                        StartCoroutine(BackdropChange(room.Backdrop));
                    else
                    {

                        FrontCurtain.sprite = room.Backdrop;

                        FrontCurtain.color = Color.white;
                    }
                    

                }

            }
        }
        else
        {
            CurrentRoom = eventdata.Backdrop;
            CurrentCG = "";
            foreach (var room in Backdrop)
            {

                if (room.ID == CurrentRoom)
                {
                    if (!Skip)
                        StartCoroutine(BackdropChange(room.Backdrop));
                    else
                    {

                        FrontCurtain.sprite = room.Backdrop;

                        FrontCurtain.color = Color.white;
                    }

                }

            }
        }

        
        
    }

    IEnumerator BackdropChange(Sprite newBackdrop)
    {
        BackCuratin.sprite = newBackdrop;
        var Awhite = Color.white;
        Awhite.a = 0;
        FrontCurtain.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Awhite, BackgroundFadeTime));
        BackCuratin.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Color.white, BackgroundFadeTime));

        yield return new WaitForSeconds(BackgroundFadeTime);

        FrontCurtain.sprite = newBackdrop;
        FrontCurtain.color = Color.white;
        BackCuratin.color = Awhite;

    }
    
}


public class StageDirectionEvent : DefaultEvent
{
    public string character;
    public Room Backdrop;
    public TransitionTypes Transitions;
    
    public StageDirectionEvent(Room scenery, string tag = "", TransitionTypes move = TransitionTypes.CrossFade)
    {
        Backdrop = scenery;
        character = tag;
        Transitions = move;
    }
    
    public StageDirectionEvent(string data)
    {
        string[] calls = data.Split(',');
        Backdrop = Room.None;
        Transitions = TransitionTypes.None;

        foreach(var direct in calls)
        {
            var directions = direct.Replace(" ", "");
            
            if(directions.ToLower().Contains("transition_"))
            {
                directions = directions.ToLower().Replace("transition_", "");

                for (var i = 0; i < Enum.GetValues(typeof(TransitionTypes)).Length; ++i)
                {
                    if (directions == ((TransitionTypes)i).ToString().ToLower())
                    {
                        Transitions = (TransitionTypes)i;
                        break;
                    }
                }

            }

            if (directions.ToLower().Contains("cg_"))
            {
                character = directions.ToLower().Replace("cg_", "");
                continue;

            }

            for (var i = 0; i < Enum.GetValues(typeof(Room)).Length; ++i)
            {
                if (directions.ToLower() == ((Room)i).ToString().ToLower())
                {
                    Backdrop = (Room)i;
                    break;
                }
            }
        }

    }

}

[Serializable]
public class RoomDetails
{
    public Room ID;
    public string Tag;
    public Sprite Backdrop;
}

public enum TransitionTypes
{
    None,
    CrossFade,
    Wipe,
    BlackWipe
}