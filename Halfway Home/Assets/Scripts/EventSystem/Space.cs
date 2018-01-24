/******************************************************************************/
/*!
File:   Space.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System;

public class Space : MonoBehaviour
{

    private static Space _instance;

    public static Space Instance { get { return _instance; } }


    private void Awake()
    {
        
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    static public void DispatchEvent(string eventName, EventData data = null)
    {
        Instance.gameObject.DispatchEvent(eventName, data);
    }

    static public void Connect<T>(string eventName, Action<T> function) where T : EventData
    {
        EventSystem.ConnectEvent<T>(Instance.gameObject, eventName, function);
    }

    static public void DisConnect<T>(string eventName, Action<T> function) where T : EventData
    {
        if (Instance.gameObject != null)
            EventSystem.DisconnectEvent<T>(Instance.gameObject, eventName, function);
    }
}

public enum EventListener
{
    Space,
    Owner,
}