/******************************************************************************/
/*!
File:   DelayObject.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System.Collections;

public class DelayObject : MonoBehaviour
{

    public EventListener ListeningOn = EventListener.Owner;

    public Events DelayOn = Events.Null;

    public EventListener TalkTo = EventListener.Space;

    public Events WhatToDoOnFinish = Events.Null;

    public float DelayTime = 1f;

    // Use this for initialization
    void Start ()
    {
        
        if (ListeningOn == EventListener.Owner)
            EventSystem.ConnectEvent<DefaultEvent>(gameObject, DelayOn, Delay);
        else if (ListeningOn == EventListener.Space)
            Space.Connect<DefaultEvent>(DelayOn, Delay);

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void Delay(DefaultEvent eventdata)
    {
        StartCoroutine(Wait());
    }


    IEnumerator Wait()
    {
        
        yield return new WaitForSeconds(DelayTime);


        if (TalkTo == EventListener.Owner)
            gameObject.DispatchEvent(WhatToDoOnFinish);
        else if (TalkTo == EventListener.Space)
            Space.DispatchEvent(WhatToDoOnFinish);

    }


    void OnDestroy()
    {
        if (ListeningOn == EventListener.Owner)
            EventSystem.DisconnectEvent(gameObject, DelayOn, this);
        else if (ListeningOn == EventListener.Space && Space.Instance != null)
            EventSystem.DisconnectEvent(Space.Instance.gameObject, DelayOn, this);
        
        gameObject.DispatchEvent(Events.Destroy);
    }

}
