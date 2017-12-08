/*******************************************************************************
filename    EventSystem.cs
author      Jesse Lozano

Brief Description:
This is the system manger for the Event System I added to Unity

All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*******************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using System;


public static class EventSystem
{

    private static Dictionary<object, EventHandler> EventList = new Dictionary<object, EventHandler>();
	
    static public void ConnectEvent<T>(object listener, String eventName, Action<T> function) where T : EventData
    {

        //if this listener is not already in our list somewhere
        if(!EventList.ContainsKey(listener))
        {
            EventList.Add(listener, new EventHandler(listener));
        }

        var handle = EventList[listener];

        handle.AddListener(eventName, function);

    }

    static public void DisconnectEvent(object target, String eventName, object thisPointer = null)
    {

        

        if (thisPointer == null)
        {
            thisPointer = target;
        }

        //if the object is not connectted, stop
        if(!EventList.ContainsKey(target))
        {
            return;
        }
        
        var handle = EventList[target];
        handle.RemoveListener(eventName, thisPointer);
        
        
        if(handle.Count() == 0)
        {
            EventList.Remove(target);
        }
    }

    static public void DisconnectEvent<T>(object target, String eventName, Action<T> function) where T : EventData
    {
        //if the object is not connectted, stop
        if (!EventList.ContainsKey(target))
        {
            return;
        }

        var handle = EventList[target];
        handle.RemoveListener(eventName, function);
        
        if (handle.Count() == 0)
        {
            EventList.Remove(target);
        }

    }

    static public void SendEvent(object target, String eventName, EventData eventData = null)
    {

        //if the object is not connectted, stop
        if (!EventList.ContainsKey(target))
        {
            return;
        }

        var handle = EventList[target];
        handle.Call(eventName, eventData);


        

    }

    public static void DisconnectObject(object listener)
    {
        if (EventList.ContainsKey(listener))
        {
            EventList.Remove(listener);
        }
    }

    //ExtensionMethods
    public static void DispatchEvent<TObject>(this TObject instance, String eventName, EventData eventData = null)
    {
        EventSystem.SendEvent(instance, eventName, eventData);
    }
    public static void Connect<TObject>(this TObject instance, String eventName, Action<EventData> function)
    {
        EventSystem.ConnectEvent(instance, eventName, function);
    }
    public static void Disconnect<TObject>(this TObject instance, String eventName, Action<EventData> function)
    {
        EventSystem.DisconnectEvent(instance, eventName, function);
    }
    public static void Disconnect<TObject>(this TObject instance, String eventName, object funcThisPointer)
    {
        EventSystem.DisconnectEvent(instance, eventName, funcThisPointer);
    }
}


abstract public class EventData
{
  public Events EventName = Events.DefaultEvent;

  public void GiveEventName(Events eventName)
  {
    EventName = eventName;
  }
}

public class DefaultEvent : EventData
{

  public DefaultEvent()
  {
    GiveEventName(Events.DefaultEvent);
  }

  public DefaultEvent(Events eName)
  {
    GiveEventName(eName);
  }
}




public class EventDelegate<T>  where T: EventData
{

}