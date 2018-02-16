using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeakerBoxDisplay : MonoBehaviour
{
    
    Dictionary<string, StagePosition> Actors;

	// Use this for initialization
	void Start ()
    {
        Actors = new Dictionary<string, StagePosition>();

        Space.Connect<DescriptionEvent>(Events.Description, OnNewLine);
        
        Space.Connect<CastDirectionEvent>(Events.CharacterCall, CharacterChanges);
        Space.Connect<DefaultEvent>(Events.Load, OnLoad);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void OnNewLine(DescriptionEvent eventdata)
    {


        if (eventdata.Speaker == "")
        {
            return;
        }
        

        if (Actors.ContainsKey(eventdata.Speaker.ToLower()))
        {
            //do whatever you want it to do here, regarding position
        }
        


    }


    void CharacterChanges(CastDirectionEvent eventdata)
    {
        if (eventdata.Exiting)
        {
            CharacterExit(eventdata);
            return;
        }
        
        if (!Actors.ContainsKey(eventdata.character.ToLower()))
            Actors.Add(eventdata.character.ToLower(), eventdata.Direction);

        Actors[eventdata.character.ToLower()] = eventdata.Direction;

    }


    void CharacterExit(CastDirectionEvent eventdata)
    {

        if (eventdata.character.ToLower() == "all")
        {
            Actors.Clear();
        }


        foreach (var Roll in Actors)
        {
            if (Roll.Key == eventdata.character.ToLower())
            {
                Actors.Remove(Roll.Key);
                return;
            }
        }
    }


    void OnLoad(DefaultEvent eventdata)
    {
        foreach (var actor in Game.current.CastCall)
        {
            Actors.Add(actor.chara, actor.Dir);
            
        }
    }


}
