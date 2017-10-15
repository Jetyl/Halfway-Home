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

    Dictionary<StagePosition, int> SpotLights; //number of people in spot


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
        Space.Connect<StageDirectionEvent>(Events.CharacterExit, CharacterExit);
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
                    directions.Direction = eventdata.Direction;
                    SpotLights[eventdata.Direction] += 1;
                    UpdateStagePositions(eventdata.Direction);

                }
            }
            
        }

        

    }

    void CharacterExit(StageDirectionEvent eventdata)
    {
        foreach (var Roll in Actors)
        {

            if (Roll == null)
            {
                Actors.Remove(Roll);
            }

            if (Roll.Character.Character == eventdata.character)
            {

                SpotLights[Roll.Direction] -= 1;
                UpdateStagePositions(Roll.Direction);
                Roll.ExitStage();
                Actors.Remove(Roll);
                return;
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

        UpdateStagePositions(eventdata.Direction);
        
    }


    void UpdateStagePositions(StagePosition pos)
    {
        int i = 0;
        var spot = new Vector3();
        switch(pos)
        {
            case StagePosition.Center:
                spot = gameObject.transform.position;
                break;
            case StagePosition.Left:
                spot = LeftSpot.transform.position;
                break;
            case StagePosition.Right:
                spot = RightSpot.transform.position;
                break;
            default:
                break;
        }


        if (SpotLights[pos] != 1)
            spot.x -= (Varience/2) * (SpotLights[pos] - 1);

        foreach (var Roll in Actors)
        {
            

            if(Roll.Direction == pos)
            {
                spot.z = Roll.transform.localPosition.z;
                
                iTween.MoveTo(Roll.gameObject, spot, 2);
                spot.x += Varience;
                i += 1;
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
    public Sprite Backdrop;
    public StagePosition Direction;


    public StageDirectionEvent(string person, string pose)
    {
        character = person;
        Pose = pose;
        Direction = StagePosition.Center;
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