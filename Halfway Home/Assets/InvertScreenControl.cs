using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertScreenControl : MonoBehaviour
{
    public string ActiveTag;

    public string[] Speakers;
    
    InvertColorEffect Inverter;

    bool Active;


	// Use this for initialization
	void Start ()
    {

        Inverter = GetComponent<InvertColorEffect>();
        Inverter.enabled = false;

        if (Game.current != null)
            Active = Game.current.Progress.GetBoolValue(ActiveTag);

        Space.Connect<DescriptionEvent>(Events.Description, OnNewLine);
        Space.Connect<DefaultEvent>(Events.Progress, OnProgressUpdate);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void OnNewLine(DescriptionEvent eventdata)
    {
        if (!Active)
            return;

        Inverter.enabled = InvertOn(eventdata.TrueSpeaker);
        

    }

    bool InvertOn(string trueSpeaker)
    {
        foreach(var speaker in Speakers)
        {
            if (trueSpeaker.ToLower() == speaker.ToLower())
                return true;
        }

        return false;
    }


    void OnProgressUpdate(DefaultEvent eventdata)
    {
        Active = Game.current.Progress.GetBoolValue(ActiveTag);
    }
}
