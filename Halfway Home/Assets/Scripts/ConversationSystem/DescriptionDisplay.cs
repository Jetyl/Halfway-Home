/******************************************************************************/
/*!
File:   DescriptionDisplay.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

public class DescriptionDisplay : MonoBehaviour
{
    
    
    Animator anime;
    AutoType Description;
    public GameObject Speaker;

    public Animator NextLine;

    public SpriteSwapper AutoButton;

    public float AutoTimeDelay = 2;

    public bool DebugSkipping;

    float AutoTimer = 0;

    bool Auto = false;

    bool Active = false;

    bool isFinished = false;
        
    string Line;
  
    bool Paused = false;

    bool Skipping = false;

    bool Stop = false;

    bool Next = false;

    [HideInInspector]
    public bool NoClick = false;

    // Use this for initialization
    void Start ()
    {
        anime = gameObject.GetComponent<Animator>();
        Description = gameObject.GetComponentInChildren<AutoType>();
        //Speaker = gameObject.transform.Find("DialogBox").Find("Speaker").gameObject;
        

        Speaker.GetComponentInChildren<TextMeshProUGUI>().text = "";

        Space.Connect<DescriptionEvent>(Events.Description, UpdateDescription);
        Space.Connect<DefaultEvent>(Events.FinishedAutoType, OnFinishedTyping);
        Space.Connect<DefaultEvent>(Events.CloseDescription, CloseDisplay);
        Space.Connect<DefaultEvent>(Events.Pause, OnPause);
        Space.Connect<DefaultEvent>(Events.UnPause, OnUnPause);
        Space.Connect<DefaultEvent>(Events.StopSkipTyping, OnStopSkipping);
        Space.Connect<DefaultEvent>(Events.ReturnToMap, OnSkipOff);
        Space.Connect<DefaultEvent>(Events.Debug, OnDebug);
        Space.Connect<DefaultEvent>(Events.NextLine, OnNext);

        //events that activate/deactivate the text box
        Space.Connect<DefaultEvent>(Events.GetPlayerInfo, OnStop);
        Space.Connect<DefaultEvent>(Events.GetPlayerInfoFinished, OnNonStop);
        Space.Connect<DefaultEvent>(Events.OpenHistory, OnStop);
        Space.Connect<DefaultEvent>(Events.CloseHistory, OnNonStop);
        Space.Connect<DefaultEvent>(Events.OpenUI, OnNonStop);
        Space.Connect<DefaultEvent>(Events.CloseUI, OnStop);
        Space.Connect<DefaultEvent>(Events.TimeChange, OnStop);
        Space.Connect<DefaultEvent>(Events.ClockFinished, OnNonStop);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!Active)
            return;

        if (Paused)
            return;

        if (Stop)
            return;

        if (NoClick)
            return;

        if(Input.GetButtonDown("Skip"))
            ToggleSkip();
            
        if (Input.GetButtonDown("Auto"))
            ToggleAuto();
        
        
        if (Skipping)
        {
            if (isFinished)
                Finished();
        }
        else if(Auto)
        {
            if(isFinished)
            {
                if (AutoTimer > AutoTimeDelay)
                {
                    AutoTimer = 0;
                    Finished();
                }
                else
                    AutoTimer += Time.deltaTime;
            }

            
        }
        else if (Input.GetMouseButtonDown(0) == true || Input.GetButtonDown("Next"))
        {
            if (!isFinished)
            {
                Description.gameObject.DispatchEvent(Events.PrintLine);
                
                NextLine.SetBool("Play", true);
                
                return;
            }

            Finished();
            
        }
        else if (Next)
        {
            if (isFinished)
            {
                Next = false;
                Finished();
            }
            
        }
        

    }

    public void ToggleAuto()
    {
        Auto = !Auto;
        AutoTimer = AutoTimeDelay;
        AutoButton.Swap();
    }

    public void ToggleSkip()
    {
        Skipping = !Skipping;

        if (Skipping)
            Space.DispatchEvent(Events.SkipTyping);
        else
            Space.DispatchEvent(Events.StopSkipTyping);

        Description.SetSkipping(Skipping);
    }

    public void Finished()
    {
        isFinished = false;

         NextLine.SetBool("Play", false);
         // NextLine.Play("LinePlaying");
        
        Active = false;
        
        Space.DispatchEvent(Events.FinishedDescription);
        

    }


    void UpdateDescription(DescriptionEvent eventdata)
    {
        gameObject.SetActive(true);
        //Space.DispatchEvent(Events.OpenUI, new UIEvent(this));
        //dynamically edit the lines so they adhere to certain parameters
        Line = TextParser.DynamicEdit(eventdata.Line);
        
        if(!DebugSkipping)
        {
            if (!eventdata.CanSkip && Skipping)
            {
                ToggleSkip();
            }
        }
        
        Active = true;
        Description.gameObject.DispatchEvent(Events.AutoType, new AutoTypeEvent(Line));
        isFinished = false;
    }

    void CloseDisplay (DefaultEvent eventdata)
    {
        print("off");
        //Space.DispatchEvent(Events.CloseUI, new UIEvent(this));
        anime.SetBool("IsUp", false);
        Active = false;
        Description.Clear();
        StartCoroutine(WaitTilClosed());
    }

    


    IEnumerator WaitTilClosed()
    {

        yield return new WaitForSeconds(0.9f);

        Space.DispatchEvent(Events.DescriptionClosed);

    }

    IEnumerator WaitTilOpened()
    {

        Description.Clear();


        yield return new WaitForSeconds(1.5f);
        
        Active = true;
        Description.gameObject.DispatchEvent(Events.AutoType, new AutoTypeEvent(Line));
        isFinished = false;
    }

    void OnFinishedTyping(DefaultEvent eventdata)
    {
        isFinished = true;
        if (!Auto)
            NextLine.SetBool("Play", true);
    }

    void OnPause(DefaultEvent eventdata)
    {
        Paused = true;
    }
    void OnUnPause(DefaultEvent eventdata)
    {
        Paused = false;
    }

    void OnStop(DefaultEvent eventdata)
    {
        Stop = true;
    }
    void OnNonStop(DefaultEvent eventdata)
    {
        Stop = false;
    }

    void OnStopSkipping(DefaultEvent eventdata)
    {
        Skipping = false;
        Description.SetSkipping(Skipping);
        
    }

    void OnSkipOff(DefaultEvent eventdata)
    {
        Space.DispatchEvent(Events.StopSkipTyping);
    }

    void OnNext(DefaultEvent eventdata)
    {
    Debug.Log("Skip");
        Next = true;
    }

    void OnDebug(DefaultEvent eventdata)
    {
        DebugSkipping = true;
    }

    public void ClickableOff()
    {
        NoClick = true;
    }
    public void ClickableOn()
    {
        NoClick = false;
    }

    void OnDestroy()
    {

        Space.DisConnect<DescriptionEvent>(Events.Description, UpdateDescription);
        Space.DisConnect<DefaultEvent>(Events.FinishedAutoType, OnFinishedTyping);
        Space.DisConnect<DefaultEvent>(Events.CloseDescription, CloseDisplay);
        Space.DisConnect<DefaultEvent>(Events.Pause, OnPause);
        Space.DisConnect<DefaultEvent>(Events.UnPause, OnUnPause);
        Space.DisConnect<DefaultEvent>(Events.StopSkipTyping, OnStopSkipping);
        Space.DisConnect<DefaultEvent>(Events.ReturnToMap, OnSkipOff);
        Space.DisConnect<DefaultEvent>(Events.Debug, OnDebug);
        Space.DisConnect<DefaultEvent>(Events.NextLine, OnNext);

        //events that activate/deactivate the text box
        Space.DisConnect<DefaultEvent>(Events.GetPlayerInfo, OnStop);
        Space.DisConnect<DefaultEvent>(Events.GetPlayerInfoFinished, OnNonStop);
        Space.DisConnect<DefaultEvent>(Events.OpenHistory, OnStop);
        Space.DisConnect<DefaultEvent>(Events.CloseHistory, OnNonStop);
        Space.DisConnect<DefaultEvent>(Events.OpenUI, OnNonStop);
        Space.DisConnect<DefaultEvent>(Events.CloseUI, OnStop);
        Space.DisConnect<DefaultEvent>(Events.TimeChange, OnStop);
        Space.DisConnect<DefaultEvent>(Events.ClockFinished, OnNonStop);
    }

}


public class DescriptionEvent : DefaultEvent
{
    
    public bool CanSkip;
    //public List<Line> Lines;
    public string Line;
    public string Speaker;
    public string TrueSpeaker; //speaker without the nickname
   
    public DescriptionEvent(string _lines, string _speaker, bool CanSkip_ = false)
    {
        Line = _lines;
        CanSkip = CanSkip_;
        
        Speaker = _speaker.Replace("[", "");
        Speaker = Speaker.Replace("]", "");

        string[] calls = Speaker.Split('>');

        TrueSpeaker = calls[0].Replace(" ", "");
        if (calls.Length > 1)
            Speaker = calls[1];
        else
            Speaker = TrueSpeaker;

    }

}

[Serializable]
public struct Line
{
    public string Speaker;
    public bool NewSpeaker;
    public string Dialog;
    public float Pace;
}
