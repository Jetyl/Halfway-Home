using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MoveSpeakerBoxDisplay : MonoBehaviour
{
    
  Dictionary<string, StagePosition> Actors;
  private bool IsSkipping = false;
    private StagePosition CurrentPos;

	// Use this for initialization
	void Start ()
    {
        Actors = new Dictionary<string, StagePosition>();
        CurrentPos = StagePosition.Same;
        Space.Connect<DescriptionEvent>(Events.Description, OnNewLine);
        
        Space.Connect<CastDirectionEvent>(Events.CharacterCall, CharacterChanges);
        Space.Connect<DefaultEvent>(Events.Load, OnLoad);
        
        Space.Connect<DefaultEvent>(Events.SkipTyping, OnSkipTyping);
        Space.Connect<DefaultEvent>(Events.StopSkipTyping, OnStopSkipTyping);
    }


  void OnNewLine(DescriptionEvent eventdata)
    {
        print(eventdata.Speaker);

        if (eventdata.Speaker == "")
        {
            return;
        }
        
        if (Actors.ContainsKey(eventdata.TrueSpeaker.ToLower()))
        {
            StagePosition pos = Actors[eventdata.TrueSpeaker.ToLower()];
            print(eventdata.TrueSpeaker.ToLower() + " " + pos);
            GetComponent<Animator>().SetBool("Skipping", IsSkipping);

            //if (pos == CurrentPos)
              //  return;

            //pos = CurrentPos;

            switch(pos)
            {
              case StagePosition.Left:
                GetComponent<Animator>().SetInteger("Position", 0);
                break;
              case StagePosition.Center:
                    print("here!");
                GetComponent<Animator>().SetInteger("Position", 1);
                break;
              case StagePosition.Right:
                GetComponent<Animator>().SetInteger("Position", 2);
                break;
              default:
                    print("whut");
                GetComponent<Animator>().SetInteger("Position", 0);
                break;
            }
        }
        else
        {
            print(eventdata.Speaker);
            if(CurrentPos != 0)
            {
                CurrentPos = 0;
                GetComponent<Animator>().SetInteger("Position", 0);
            }
        }
    }


    void CharacterChanges(CastDirectionEvent eventdata)
    {
        if (eventdata.Exiting)
        {
            CharacterExit(eventdata);
            return;
        }


        print(eventdata.character.ToLower() + " " + eventdata.Direction);

        if (!Actors.ContainsKey(eventdata.character.ToLower()))
        {
            if (eventdata.Direction != StagePosition.Same)
                Actors.Add(eventdata.character.ToLower(), eventdata.Direction);
            else
                Actors.Add(eventdata.character.ToLower(), StagePosition.Center);
        }
            
        if (eventdata.Direction != StagePosition.Same)
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

    void OnSkipTyping(DefaultEvent eventdata)
    {
        IsSkipping = true;
    }

    void OnStopSkipTyping(DefaultEvent eventdata)
    {
        IsSkipping = false;
    }


}
