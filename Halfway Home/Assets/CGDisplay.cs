using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGDisplay : MonoBehaviour
{

    public List<CGDetails> CGs;

    CGDetails ActiveCG;

	// Use this for initialization
	void Start ()
    {
        ActiveCG = new CGDetails();
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
            ActiveCG.Graphic = Instantiate(GetCG(tag).Graphic, transform);
        }



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

public class CGDetails
{
    public string Tag;
    public GameObject Graphic;
}


public class CustomGraphicEvent : DefaultEvent
{
    public string Tag;
    public string Act;

    public CustomGraphicEvent(string tag = "")
    {
        Tag = tag;
    }

    public CustomGraphicEvent(string tag, string data)
    {
        string[] calls = data.Split(',');

        foreach (var direct in calls)
        {
            
        }

    }

}