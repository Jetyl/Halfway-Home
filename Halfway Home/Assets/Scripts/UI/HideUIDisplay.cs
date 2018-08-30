﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideUIDisplay : MonoBehaviour
{
    bool UIOn = true;
    private bool OnMap;
    private bool Paused;
    public GameObject[] IgnoredObjects;
    public string IgnoredTag;

    private List<Graphic> activeUIElements = new List<Graphic>();

    // Use this for initialization
    void Start()
    {
        Space.Connect<DefaultEvent>(Events.ReturnToMap, OnReturnToMap);
        Space.Connect<DefaultEvent>(Events.MapTransitionOutCompleted, OnMapTransition);

        Space.Connect<DefaultEvent>(Events.Pause, OnPause);
        Space.Connect<DefaultEvent>(Events.UnPause, OnUnPause);

        OnMap = false;
        Paused = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1) == true && !OnMap && !Paused)
        {
            if (UIOn)
                TurnUIOff();
            else
                TurnUIOn();
        }

        if (!UIOn)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Next"))
            {
                TurnUIOn();
            }
        }
    }

    void OnReturnToMap(DefaultEvent e)
    {
        OnMap = true;
    }

    void OnMapTransition(DefaultEvent e)
    {
        OnMap = false;
    }


    void OnPause(DefaultEvent e)
    {
        Paused = true;
    }

    void OnUnPause(DefaultEvent e)
    {
        Paused = false;
    }


    void TurnUIOn()
    {
        UIOn = true;

        activeUIElements.RemoveAll(item => item == null);

        foreach (var obj in activeUIElements)
        {
            obj.enabled = true;
            if (obj.GetComponent<Button>() != null) obj.GetComponent<Button>().enabled = true;
        }
        Space.DispatchEvent(Events.OpenUI);
    }

    void TurnUIOff()
    {
        var gImages = gameObject.GetComponentsInChildren<Graphic>();
        UIOn = false;
        foreach (var obj in gImages)
        {
            bool ignore = false;
            foreach (GameObject g in IgnoredObjects)
            {
                if (g == obj.gameObject) ignore = true;
            }
            if (obj.gameObject.activeSelf && obj.enabled && !ignore && !(obj.tag == IgnoredTag))
            {
                obj.enabled = false;
                if (obj.GetComponent<Button>() != null) obj.GetComponent<Button>().enabled = false;
                activeUIElements.Add(obj);
            }
        }
        Space.DispatchEvent(Events.CloseUI);
    }
}
