/*******************************************************************************
filename    EventHandler.cs
author      Jesse Lozano

Brief Description:
This is the handler for each event type, and what the user uses to interact 
with the event System

All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*******************************************************************************/


using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public class EventHandler 
{

    public delegate void EventDelegate<T>(T e) where T : EventData;
    private delegate void EventDelegate(EventData e);


    private object Handle;
    private Dictionary<String, List<EventDelegate>> EventList;

    private Dictionary<String, List<object>> EventTargets;
    private Dictionary<String, List<MethodInfo>> EventMethods;

	// Use this for initialization
	public EventHandler (object sender)
    {
        Handle = sender;
        EventList = new Dictionary<String, List<EventDelegate>>();
        EventList.Add(Events.Destroy, new List<EventDelegate>());

        EventTargets = new Dictionary<String, List<object>>();
        EventTargets.Add(Events.Destroy, new List<object>());

        EventMethods = new Dictionary<String, List<MethodInfo>>();
        EventMethods.Add(Events.Destroy, new List<MethodInfo>());

        //EventList[Events.Destroy].Add(Destroy);
        AddListener<DefaultEvent>(Events.Destroy, Destroy);
    }


    private EventDelegate<T> Convert<T>(Action<T> function) where T : EventData
    {
        EventDelegate<T> del = new EventDelegate<T>(function);

        return del;
    }

    public void AddListener<T>(String eventname, Action<T> function) where T : EventData
    {

        var del = Convert(function);
        
        //if the listener's handle doesn't have the the event in its list
        if (!EventList.ContainsKey(eventname))
        {

            

            EventList.Add(eventname, new List<EventDelegate>());
            EventTargets.Add(eventname, new List<object>());
            EventMethods.Add(eventname, new List<MethodInfo>());

        }
        

        EventDelegate internalDel = (e) => del((T)e);
        
        EventList[eventname].Add(internalDel);
        EventTargets[eventname].Add(function.Target);
        EventMethods[eventname].Add(function.Method);

    }

    public void RemoveListener<T>(String eventname, Action<T> fun) where T : EventData
    {
        //if the event is not connected, stop
        if (!EventList.ContainsKey(eventname))
        {
            return;
        }
        
        var functionList = EventList[eventname];

        var methodList = EventMethods[eventname];

        for (int i = 0; i < functionList.Count; ++i)
        {
            if (methodList[i].Equals(fun.Method))
            {
                functionList.RemoveAt(i);
                methodList.RemoveAt(i);
                EventTargets[eventname].RemoveAt(i);
                break;
            }
        }

        if (functionList.Count == 0)
        {
            EventList.Remove(eventname);
            EventTargets.Remove(eventname);
            EventMethods.Remove(eventname);
        }


    }


    public void RemoveListener(String eventName, object target)
    {
        
        //if the event is not connected, stop
        if (!EventList.ContainsKey(eventName))
        {
            return;
        }
        
        var functionList = EventList[eventName];
        var TarList = EventTargets[eventName];

        for (int i = 0; i < functionList.Count; ++i)
        {
            if (TarList[i].Equals(target))
            {
                functionList.RemoveAt(i);
                TarList.RemoveAt(i);
                EventMethods[eventName].RemoveAt(i);
                break;
            }
        }

        if (functionList.Count == 0)
        {
            EventList.Remove(eventName);
            EventTargets.Remove(eventName);
            EventMethods.Remove(eventName);
        }

    }

    public void Call(String eventName, EventData eventData = null)
    {

        //if the event is not connected, stop
        if (!EventList.ContainsKey(eventName))
        {
            return;
        }

        if (eventData == null)
        {
            eventData = new DefaultEvent(eventName);
        }

        else if (eventData.EventName != eventName)
        {
          eventData.GiveEventName(eventName);
        }

        var functionList = EventList[eventName];

        for (int i = 0; i < functionList.Count; ++i)
        {
            //if the function is static, or exists
            //Debug.Log(EventTargets[eventName][i] + " is not Null: " + !EventTargets[eventName][i].Equals(null));

            if ((functionList[i].Method.IsStatic || !functionList[i].Target.Equals(null)) && !EventTargets[eventName][i].Equals(null))
            {
                functionList[i].Invoke(eventData);

            }
            else
            {
                functionList.RemoveAt(i);
                EventTargets[eventName].RemoveAt(i);
                EventMethods[eventName].RemoveAt(i);
                --i;
            }
        }



    }


    public int Count()
    {
        var num = EventList.Count;
        return num;
    }


    private void Destroy(DefaultEvent events)
    {
        EventSystem.DisconnectObject(Handle);
    }

	~EventHandler()
    {
        EventSystem.DisconnectObject(Handle);
    }

}
