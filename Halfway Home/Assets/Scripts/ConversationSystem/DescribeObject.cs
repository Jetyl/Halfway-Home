/******************************************************************************/
/*!
File:   DescribeObject.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DescribeObject : MonoBehaviour
{


    public EventListener ListeningOn = EventListener.Owner;

    public Events DecribeOn = Events.Null;

    public EventListener TalkTo = EventListener.Space;

    public Events WhatToDoOnFinish = Events.CloseDescription;

    public TextAsset DynamicText;
    
    public List<Line> DefaultText;

    public bool CallFinishImmediately;

    public bool DenyPlayerInput;

    public bool DisableUI;

    public bool ReenableUIWhenDone;

    bool Active = false;
    

    // Use this for initialization
    void Start ()
    {
        
        if(DynamicText != null)
        {
            //var dynamic = DynamicText.text.Split("\n"[0]);
            DefaultText = TextParser.ParseLines(DynamicText);

        }
        
        //edit the lines to be withing a certain standard
        //DefaultText = TextParser.DynamicEdit(DefaultText);
        
        

        Space.Connect<DefaultEvent>(Events.FinishedDescription, FinishedDecription);

        if (ListeningOn == EventListener.Owner)
            EventSystem.ConnectEvent<DefaultEvent>(gameObject, DecribeOn, Decribe);
        else if (ListeningOn == EventListener.Space)
            Space.Connect<DefaultEvent>(DecribeOn, Decribe);

        

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FinishedDecription(DefaultEvent eventdata)
    {

        if (!Active)
            return;

        Active = false;

        if (ReenableUIWhenDone)
            Space.DispatchEvent(Events.UnPause);


        if (TalkTo == EventListener.Owner)
            gameObject.DispatchEvent(WhatToDoOnFinish);
        else if (TalkTo == EventListener.Space)
            Space.DispatchEvent(WhatToDoOnFinish);

    }

    void Decribe(DefaultEvent eventdata)
    {
        //Space.DispatchEvent(Events.Description, new DescriptionEvent(DefaultText, CallFinishImmediately));
        StartCoroutine(FrameDelay());

        if (DisableUI)
            Space.DispatchEvent(Events.Pause);

    }

    IEnumerator FrameDelay()
    {

        yield return new WaitForSeconds(Time.deltaTime);

        Active = true;

    }

    void OnDestroy()
    {
        
        if(Space.Instance != null)
            EventSystem.DisconnectEvent(Space.Instance.gameObject, Events.FinishedDescription, this);
    }

}
