﻿/******************************************************************************/
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

    public WipeTransitionAnimator WipeCurtain;
    public EyeTransitionAnimator EyeCurtain;

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
                    BackdropChange(room.Backdrop, eventdata.Transitions);
            }
        }
        else
        {
            CurrentRoom = eventdata.Backdrop;
            CurrentCG = "";
            foreach (var room in Backdrop)
            {
                if (room.ID == CurrentRoom)
                    BackdropChange(room.Backdrop, eventdata.Transitions);
            }
        }

        
        
    }

    public void BackdropChange(Sprite newbackdrop, TransitionTypes transition)
    {

        if(Skip)
        {

            FrontCurtain.sprite = newbackdrop;

            FrontCurtain.color = Color.white;

            return;
        }

        switch (transition)
        {
            case TransitionTypes.None:
                FrontCurtain.sprite = newbackdrop;
                FrontCurtain.color = Color.white;
                break;
            case TransitionTypes.CrossFade:
                StartCoroutine(CrossFade(newbackdrop));
                break;
            case TransitionTypes.Wipe:
                StartCoroutine(Wipe(newbackdrop, 1));
                break;
            case TransitionTypes.WipeLeft:
                StartCoroutine(Wipe(newbackdrop, -1));
                break;
            case TransitionTypes.BlackWipe:
                StartCoroutine(BlackWipe(newbackdrop, 1));
                break;
            case TransitionTypes.BlackWipeLeft:
                StartCoroutine(BlackWipe(newbackdrop, -1));
                break;
            case TransitionTypes.EyeClose:
                StartCoroutine(EyeCloseFade(newbackdrop));
                break;
            case TransitionTypes.EyeOpen:
                StartCoroutine(EyeOpenFade(newbackdrop));
                break;
            default:
                break;
        }

    }


    IEnumerator CrossFade(Sprite newBackdrop)
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

    IEnumerator Wipe(Sprite newBackdrop, int direction)
    {
        WipeCurtain.FadeDirection = new Vector2(direction, 0);
        WipeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = FrontCurtain.sprite;
        WipeCurtain.Progress = 0;
        FrontCurtain.sprite = newBackdrop;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / BackgroundFadeTime)
        {
            WipeCurtain.Progress = t;
            yield return null;
        }

        WipeCurtain.Progress = 1;

        FrontCurtain.sprite = newBackdrop;

    }

    IEnumerator BlackWipe(Sprite newBackdrop, int direction)
    {
        WipeCurtain.FadeDirection = new Vector2(direction, 0);
        WipeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = FrontCurtain.sprite;
        WipeCurtain.Progress = 0;
        FrontCurtain.sprite = null;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / BackgroundFadeTime)
        {
            WipeCurtain.Progress = t;
            yield return null;
        }
        WipeCurtain.Progress = 1;


        yield return new WaitForSeconds(0.25f);
        WipeCurtain.FadeDirection = -WipeCurtain.FadeDirection;
        WipeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = newBackdrop;
        
        for (float t = 1.0f; t < 0.0f; t -= Time.deltaTime / BackgroundFadeTime)
        {
            WipeCurtain.Progress = t;
            yield return null;
        }
        WipeCurtain.Progress = 0;
        
        FrontCurtain.sprite = newBackdrop;

        yield return new WaitForSeconds(Time.deltaTime);
        WipeCurtain.Progress = 1;
    }


    IEnumerator EyeCloseFade(Sprite newBackdrop)
    {
        EyeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = FrontCurtain.sprite;
        EyeCurtain.Progress = 0;
        FrontCurtain.sprite = newBackdrop;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / BackgroundFadeTime)
        {
            EyeCurtain.Progress = t;
            yield return null;
        }

        EyeCurtain.Progress = 1;

    }

    IEnumerator EyeOpenFade(Sprite newBackdrop)
    {
        EyeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = newBackdrop;
        EyeCurtain.Progress = 1;
        
        for (float t = 1.0f; t < 0.0f; t -= Time.deltaTime / BackgroundFadeTime)
        {
            EyeCurtain.Progress = t;
            yield return null;
        }
        EyeCurtain.Progress = 0;

        FrontCurtain.sprite = newBackdrop;

        yield return new WaitForSeconds(Time.deltaTime);
        EyeCurtain.Progress = 1;
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
    BlackWipe,
    WipeLeft,
    BlackWipeLeft,
    EyeOpen,
    EyeClose
}