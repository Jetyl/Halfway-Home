using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CGDisplay : MonoBehaviour
{

    public List<CGDetails> CGs;
    
    public string OpenCGTag = "Open";

    public string CloseCGTag = "Exit";

    CGDetails ActiveCG;

	// Use this for initialization
	void Start ()
    {
        ActiveCG = new CGDetails();
        
        Space.Connect<CustomGraphicEvent>(Events.CG, OnDisplay);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnDisplay(CustomGraphicEvent eventdata)
    {
        if(!Contains(eventdata.Tag))
        {
            Debug.LogError("CG tag: " + eventdata.Tag + "is unknown. please add to CGDisplay");
            return;
        }

        if (eventdata.Tag != ActiveCG.Tag)
        {
            ActiveCG = new CGDetails();
            ActiveCG.Tag = eventdata.Tag;
            ActiveCG.Graphic = Instantiate(GetCG(ActiveCG.Tag).Graphic, transform);
        }

        StartCoroutine(TextParser.FrameDelay(ActiveCG.Graphic, Events.CG, eventdata));

        if(eventdata.ContainsAct(OpenCGTag))
        {
            Space.DispatchEvent(Events.Backdrop, new StageDirectionEvent(Room.None, "", eventdata.HasTransition()));
        }

        if(eventdata.ContainsAct(CloseCGTag))
        {
            Destroy(ActiveCG.Graphic, 5);
            ActiveCG = new CGDetails();
        }

        //ActiveCG.Graphic.DispatchEvent(Events.CG, eventdata);

    }

    CGDetails GetCG(string tag)
    {
        foreach (var CG in CGs)
        {
            if (CG.Tag.ToLower() == tag.ToLower())
                return CG;
        }

        return null;
    }

    bool Contains(string tag)
    {
        foreach(var CG in CGs)
        {
            if (CG.Tag.ToLower() == tag.ToLower())
                return true;
        }

        return false;
    }

    

}

[Serializable]
public class CGDetails
{
    public string Tag;
    public GameObject Graphic;
}


public class CustomGraphicEvent : DefaultEvent
{
    public string Tag;
    public string[] Actions;

    public CustomGraphicEvent(string tag = "")
    {
        Tag = tag;
    }

    public CustomGraphicEvent(string tag, string data)
    {
        Tag = tag;

        Actions = data.Split(',');
        
    }

    public bool ContainsAct(string tag)
    {
        foreach(var act in Actions)
        {
            if (act.ToLower() == tag.ToLower())
                return true;
        }

        return false;
    }

    public TransitionTypes HasTransition()
    {
        foreach (var direct in Actions)
        {
            var directions = direct.Replace(" ", "");

            for (var i = 0; i < Enum.GetValues(typeof(TransitionTypes)).Length; ++i)
            {
                if (directions.ToLower() == ((TransitionTypes)i).ToString().ToLower())
                {
                    return ((TransitionTypes)i);
                }
            }
            
        }

        return TransitionTypes.None;

    }

}