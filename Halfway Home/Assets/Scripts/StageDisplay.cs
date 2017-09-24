using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageDisplay : MonoBehaviour
{


    public List<CharacterList> CastList;

    public Sprite Backdrop;

    public SpriteRenderer FrontCurtain;
    public SpriteRenderer BackCuratin;
    public GameObject LeftSpot;
    public GameObject RightSpot;
    public float Varience = 2;

    List<CharacterDisplay> Actors;

    Dictionary<StagePosition, int> SpotLights;


	// Use this for initialization
	void Start ()
    {
    Actors = new List<CharacterDisplay>();
        SpotLights = new Dictionary<StagePosition, int>();
        for (var i = 0; i < Enum.GetValues(typeof(StagePosition)).Length; ++i)
        {
            SpotLights.Add((StagePosition)i, 0);
        }

        Space.Connect<StageDirectionEvent>(Events.CharacterCall, CharacterChanges);
        Space.Connect<StageDirectionEvent>(Events.Backdrop, SceneryChange);
        Space.Connect<StageDirectionEvent>(Events.MoveCharacter, MoveCharacter);


    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void CharacterChanges(StageDirectionEvent eventdata)
    {
        foreach(var Roll in Actors)
        {

            if(Roll == null)
            {
                Actors.Remove(Roll);
            }

            if(Roll.Character.Character == eventdata.character)
            {
                if (eventdata.Remove)
                {
                    Roll.ExitStage();
                    Actors.Remove(Roll);
                }
                else
                    Roll.ChangePose(eventdata.Pose);

                return;
            }
        }

        //if here, character is not on scene
        foreach (var person in CastList)
        {
            if(person.Character == eventdata.character)
            {
                var cast = Instantiate(person.Actor);

                var directions = cast.GetComponent<CharacterDisplay>();
                if (directions == null)
                    Debug.LogError("character: " + eventdata.character + "is missing at rollcall. See StageDisplay");
                else
                {
                    directions.EnterStage(eventdata.Pose);
                    Actors.Add(directions);

                }
            }
            
        }

        

    }


    void MoveCharacter(StageDirectionEvent eventdata)
    {
        if (GetActor(eventdata.character) < 0)
            return;


        var actor = Actors[GetActor(eventdata.character)];

        SpotLights[actor.Direction] -= 1;
        actor.Direction = eventdata.Direction;
        SpotLights[eventdata.Direction] += 1;

        MoveCharacters(eventdata.Direction);
        
    }


    void MoveCharacters(StagePosition pos)
    {
        int i = 0;
        bool Add = false;
        foreach (var Roll in Actors)
        {
            

            if(Roll.Direction == pos)
            {
                var j = new Vector3(0, 0, 0);
                switch(pos)
                {
                    case StagePosition.Center:
                        j = gameObject.transform.position;
                        break;
                    case StagePosition.Left:
                        j = LeftSpot.transform.position;
                        break;
                    case StagePosition.Right:
                        j = RightSpot.transform.position;
                        break;
                    default:
                        break;
                }
                if (Add)
                    j.x -= (Varience * 2) * (i / SpotLights[pos]);
                else
                    j.x += (Varience * 2) * (i / SpotLights[pos]);
                iTween.MoveTo(Roll.gameObject, j, 2);
                i += 1;
                Add = !Add;
                if (i >= SpotLights[pos])
                    return;

            }

            
        }
    }


    void SceneryChange(StageDirectionEvent eventdata)
    {

        StartCoroutine(BackdropChange(eventdata.Backdrop));

    }

    IEnumerator BackdropChange(Sprite newBackdrop)
    {

        BackCuratin.sprite = newBackdrop;
        var Awhite = Color.white;
        Awhite.a = 0;
        FrontCurtain.DispatchEvent(Events.Fade, new FadeEvent(Awhite, 2));
        
        yield return new WaitForSeconds(2);

        FrontCurtain.sprite = newBackdrop;
        FrontCurtain.color = Color.white;

    }

    public CharacterList GetCastMember(string name)
    {
        for(int i = 0; i < CastList.Count; ++i)
        {
            if (CastList[i].Character == name)
                return CastList[i];
        }
        return null;
    }

    public int GetActor(string name)
    {
        for (int i = 0; i < Actors.Count; ++i)
        {
            if (Actors[i].Character.Character == name)
                return i;
        }
        return -1;
    }


}


public class StageDirectionEvent : DefaultEvent
{
    public string character;
    public string Pose;
    public Vector3 Position;
    public bool Remove;
    public Sprite Backdrop;
    public StagePosition Direction;


    public StageDirectionEvent(string person, string pose, StagePosition dir = StagePosition.None, bool exit = false)
    {
        character = person;
        Pose = pose;
        Remove = exit;
        Direction = dir;
    }

    public StageDirectionEvent(Sprite scenery)
    {
        Backdrop = scenery;
    }

    public StageDirectionEvent(string person, StagePosition Dir)
    {
        character = person;
        Direction = Dir;
    }


}

public enum StagePosition
{
    None,
    Center,
    Left,
    Right
}