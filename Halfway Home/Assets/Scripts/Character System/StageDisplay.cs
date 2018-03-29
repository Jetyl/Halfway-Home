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

    public WipeTransitionAnimator WipeCurtain;
    public EyeTransitionAnimator EyeCurtain;

    public Sprite CurtainDefault;

    public Room StartingRoom;
    public float BackgroundFadeTime = 2;

    bool Load = false;

    bool Skip = false;

    Sprite CurrentBackdrop;

    [Range(0, 23)]
    public float DayTimeStart = 6;
    [Range(0, 23)]
    public float DayTimeEnd = 18;

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
        ResetBackdrop();
    }

    void OffSkip(DefaultEvent eventdata)
    {
        Skip = false;
    }

    void OnSave(DefaultEvent eventdata)
    {
        Game.current.CurrentRoom = CurrentRoom;
        Game.current.CurrentCG = CurrentCG;
    }

    void OnLoad(DefaultEvent eventdata)
    {
        Load = true;
        
        SceneryChange(new StageDirectionEvent(Game.current.CurrentRoom, Game.current.CurrentCG, TransitionTypes.None));
    }

    void SceneryChange(StageDirectionEvent eventdata)
    {
        if(eventdata.Backdrop == CurrentRoom)
        {
            if (eventdata.Backdrop != Room.None || eventdata.character == CurrentCG)
                if (eventdata.Transitions != TransitionTypes.BlackWipe && eventdata.Transitions != TransitionTypes.BlackWipeLeft)
                    return;
        }
        
        if(eventdata.Backdrop == Room.None)
        {

            CurrentRoom = Room.None;
            CurrentCG = eventdata.character;

            foreach (var room in SpecialBackdrops)
            {
                if (room.Tag.ToLower() == CurrentCG)
                {
                    BackdropChange(room.Backdrops[0], eventdata.Transitions);
                    return;
                }
            }

            //go to no background (likely for CG)
            BackdropChange(null, eventdata.Transitions);

        }
        else
        {
            CurrentRoom = eventdata.Backdrop;
            CurrentCG = "";
            foreach (var room in Backdrop)
            {
                if (room.ID == CurrentRoom)
                {

                    if(Game.current.Hour >= DayTimeStart && Game.current.Hour <= DayTimeEnd)
                        BackdropChange(room.Backdrops[0], eventdata.Transitions);
                    else
                        BackdropChange(room.Backdrops[1], eventdata.Transitions);
                }
                    
            }
        }

        
        
    }

    void ResetBackdrop()
    {
        StopAllCoroutines();

        WipeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = CurtainDefault;
        WipeCurtain.Progress = 1;

        EyeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = CurtainDefault;
        EyeCurtain.Progress = 1;

        FrontCurtain.sprite = CurrentBackdrop;
    }

    public void BackdropChange(Sprite newbackdrop, TransitionTypes transition)
    {

        ResetBackdrop();
        CurrentBackdrop = newbackdrop;

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


        yield return new WaitForSeconds(Time.deltaTime);
        WipeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = CurtainDefault;

        WipeCurtain.Progress = 1;

        FrontCurtain.sprite = newBackdrop;

    }

    IEnumerator BlackWipe(Sprite newBackdrop, int direction)
    {
        WipeCurtain.FadeDirection = new Vector2(direction, 0);
        WipeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = FrontCurtain.sprite;
        WipeCurtain.Progress = 0;
        FrontCurtain.sprite = null;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / (BackgroundFadeTime/2))
        {
            WipeCurtain.Progress = t;
            yield return null;
        }
        WipeCurtain.Progress = 1;


        yield return new WaitForSeconds(0.25f);
        WipeCurtain.FadeDirection = -WipeCurtain.FadeDirection;
        WipeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = newBackdrop;
        
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / (BackgroundFadeTime/2))
        {
            WipeCurtain.Progress = 1 - t;
            yield return null;
        }
        WipeCurtain.Progress = 0;
        
        FrontCurtain.sprite = newBackdrop;

        yield return new WaitForSeconds(Time.deltaTime);
        WipeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = CurtainDefault;
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

        yield return new WaitForSeconds(Time.deltaTime);
        EyeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = CurtainDefault;

    }

    IEnumerator EyeOpenFade(Sprite newBackdrop)
    {
        EyeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = newBackdrop;
        EyeCurtain.Progress = 1;
        
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / BackgroundFadeTime)
        {
            EyeCurtain.Progress = 1 - t;
            yield return null;
        }
        EyeCurtain.Progress = 0;
        
        FrontCurtain.sprite = newBackdrop;

        yield return new WaitForSeconds(Time.deltaTime);
        EyeCurtain.gameObject.GetComponent<SpriteRenderer>().sprite = CurtainDefault;
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
            MonoBehaviour.print(direct);
            bool stop = false;
            var directions = direct.Replace(" ", "");
            
            for (var i = 0; i < Enum.GetValues(typeof(TransitionTypes)).Length; ++i)
            {
                if (directions.ToLower() == ((TransitionTypes)i).ToString().ToLower())
                {
                    
                    Transitions = (TransitionTypes)i;
                    stop = true;
                    break;
                }
            }

            if (stop)
                continue;

            for (var i = 0; i < Enum.GetValues(typeof(Room)).Length; ++i)
            {
                if (directions.ToLower() == ((Room)i).ToString().ToLower())
                {
                    Backdrop = (Room)i;
                    stop = true;
                    break;
                }
            }

            if (stop)
                continue;
            
            character = directions.ToLower();
                

            
        }

    }

}

[Serializable]
public class RoomDetails
{
    public Room ID;
    public string Tag;
    public Sprite[] Backdrops;
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