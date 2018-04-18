using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CGDisplay : MonoBehaviour
{

    public List<CGDetails> CGList;
    
    public string OpenCGTag = "Open";

    public string CloseCGTag = "Exit";

    CGDetails ActiveCG;

    bool Active;

	// Use this for initialization
	void Start ()
    {
        ActiveCG = new CGDetails();
        
        Space.Connect<CustomGraphicEvent>(Events.CG, OnDisplay);
        Space.Connect<DefaultEvent>(Events.Backdrop, OnClose);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnClose(DefaultEvent eventdata)
    {
        if (!Active)
            return;
        CloseCG();

    }


    void OnSave(DefaultEvent eventdata)
    {
        if (!Active)
        {
            Game.current.CurrentCG = "";
            return;
        }

        Game.current.CurrentCG = ActiveCG.Tag;
    }

    void OnLoad(DefaultEvent eventdata)
    {
        if(Game.current.CurrentCG != "")
        {
            ActiveCG = new CGDetails();
            ActiveCG.Tag = Game.current.CurrentCG;
            ActiveCG.Graphic = Instantiate(GetCG(ActiveCG.Tag).Graphic, transform);

            //Space.DispatchEvent(Events.Backdrop, new StageDirectionEvent(Room.None, ""));
            //Space.DispatchEvent(Events.CharacterCall, new CastDirectionEvent("all", "exit"));
            Active = true;
        }

    }


    void OnDisplay(CustomGraphicEvent eventdata)
    {
        if(!Contains(eventdata.Tag))
        {
            Debug.LogError("CG tag: " + eventdata.Tag + "is unknown. please add to CGDisplay");
            return;
        }

        if (eventdata.Tag.ToLower() != ActiveCG.Tag.ToLower())
        {
            ActiveCG = new CGDetails();
            ActiveCG.Tag = eventdata.Tag;
            ActiveCG.Graphic = Instantiate(GetCG(ActiveCG.Tag).Graphic, transform);
        }

        StartCoroutine(TextParser.FrameDelay(ActiveCG.Graphic, Events.CG, eventdata));

        if(eventdata.ContainsAct(OpenCGTag))
        {
            Space.DispatchEvent(Events.Backdrop, new StageDirectionEvent(Room.None, "", eventdata.HasTransition()));
            Space.DispatchEvent(Events.CharacterCall, new CastDirectionEvent("all", "exit"));
            Active = true;
        }

        if(eventdata.ContainsAct(CloseCGTag))
        {
            CloseCG();
        }

        //ActiveCG.Graphic.DispatchEvent(Events.CG, eventdata);

    }

    void OpenCG()
    {
        Active = true;
    }

    void CloseCG()
    {
        Active = false;
        ActiveCG.Graphic.DispatchEvent(Events.CloseCG);
        Destroy(ActiveCG.Graphic, 5);
        ActiveCG = new CGDetails();
    }

    CGDetails GetCG(string tag)
    {
        foreach (var CG in CGList)
        {
            if (CG.Tag.ToLower() == tag.ToLower())
                return CG;
        }

        return null;
    }

    bool Contains(string tag)
    {
        foreach(var CG in CGList)
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
    public string Tag = "";
    public GameObject Graphic;
}

[Serializable]
public class CGGrouping
{
    public string Group = "";
    public string CloseTag = "Close";
    public CGDetails[] Details;
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
        
        for(int i = 0; i < Actions.Length; ++i)
        {
            Actions[i] = Actions[i].Trim();
            
        }
        
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