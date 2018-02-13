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


        Space.Connect<DefaultEvent>(Events.GetPlayerInfo, OnStop);
        Space.Connect<DefaultEvent>(Events.GetPlayerInfoFinished, OnNonStop);
        Space.Connect<DefaultEvent>(Events.OpenHistory, OnStop);
        Space.Connect<DefaultEvent>(Events.CloseHistory, OnNonStop);
        Space.Connect<DefaultEvent>(Events.OpenUI, OnNonStop);
        Space.Connect<DefaultEvent>(Events.CloseUI, OnStop);

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
        else if (Input.GetMouseButtonDown(0) == true)
        {
            if (!isFinished)
            {
                Description.gameObject.DispatchEvent(Events.PrintLine);
                
                NextLine.SetBool("Play", true);
                
                return;
            }

            Finished();
            
        }
        

    }

    public void ToggleAuto()
    {
        Auto = !Auto;
        AutoTimer = AutoTimeDelay;
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
        
        /*
        if(!anime.GetBool("IsUp"))
        {
            anime.SetBool("IsUp", true);
            StartCoroutine(WaitTilOpened());
            return;
        }*/

        //UpdateSpeaker(0);
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


}


public class DescriptionEvent : DefaultEvent
{
    
    public bool CanSkip;
    //public List<Line> Lines;
    public string Line;
    public string Speaker;
   
    public DescriptionEvent(string _lines, string _speaker, bool CanSkip_ = false)
    {
        Line = _lines;
        CanSkip = CanSkip_;
        Speaker = _speaker;
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
