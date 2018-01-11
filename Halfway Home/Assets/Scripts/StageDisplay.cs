/******************************************************************************/
/*!
File:   StageDisplay.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageDisplay : MonoBehaviour
{


    public List<CharacterList> CastList;

    public List<RoomDetails> Backdrop;
    
    public List<RoomDetails> SpecialBackdrops;

    private Room CurrentRoom = Room.None;
    private string CurrentCG = "";

    public SpriteRenderer FrontCurtain;
    public SpriteRenderer BackCuratin;
    public GameObject LeftSpot;
    public GameObject RightSpot;
    public float Varience = 2;
    public float SpotSize = 12;

    List<CharacterDisplay> Actors;

    Dictionary<StagePosition, int> SpotLights; //number of people in spot

    public Room StartingRoom;
    public float BackgroundFadeTime = 2;
    public float SpriteMoveTime = 2;

    bool Load = false;

    bool Skip = false;

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

        Space.Connect<DefaultEvent>(Events.SkipTyping, OnSkip);
        Space.Connect<DefaultEvent>(Events.StopSkipTyping, OffSkip);

        Space.Connect<DefaultEvent>(Events.Save, OnSave);
        Space.Connect<DefaultEvent>(Events.Load, OnLoad);

        if (Load == false)
            SceneryChange(new StageDirectionEvent(StartingRoom));

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnSkip(DefaultEvent eventdata)
    {
        Skip = true;
        StopAllCoroutines();
    }

    void OffSkip(DefaultEvent eventdata)
    {
        Skip = false;
    }

    void OnSave(DefaultEvent eventdata)
    {
        foreach(var actor in Actors)
        {
            actor.OnSave();
        }
    }

    void OnLoad(DefaultEvent eventdata)
    {
        Load = true;

        foreach(var actor in Game.current.CastCall)
        {
            foreach (var person in CastList)
            {
                if (person.Character == actor.chara)
                {
                    var cast = Instantiate(person.Actor);

                    var directions = cast.GetComponent<CharacterDisplay>();
                    if (directions == null)
                        Debug.LogError("character: " + actor.chara + "is missing at rollcall. See StageDisplay");
                    else
                    {
                        directions.OnLoad(actor);
                        Actors.Add(directions);
                        SpotLights[actor.Dir] += 1;

                    }
                }

            }
        }
        Game.current.CastCall = new List<CharacterIntermission>();

        SceneryChange(new StageDirectionEvent(Game.current.CurrentRoom));
    }

    void CharacterChanges(StageDirectionEvent eventdata)
    {
        //for characters already on scene
        foreach(var Roll in Actors)
        {

            if(Roll == null)
            {
                Actors.Remove(Roll);
            }

            //if the command is all, affect all
            if (eventdata.character.ToLower() == "all")
            {
                Roll.ChangePose(eventdata.Pose, Skip);
            }
            else if(Roll.Character.Character == eventdata.character)
            {
               
                Roll.ChangePose(eventdata.Pose, Skip);
                Roll.ChangeDistance(eventdata.Distance);
                return;
            }
        }

        if (eventdata.character.ToLower() == "all")
            return;

        //if here, character is not on scene
        foreach (var person in CastList)
        {
            //adding character to scene
            if(person.Character == eventdata.character)
            {
                var cast = Instantiate(person.Actor);

                var directions = cast.GetComponent<CharacterDisplay>();
                if (directions == null)
                    Debug.LogError("character: " + eventdata.character + "is missing at rollcall. See StageDisplay");
                else
                {
                    directions.EnterStage(eventdata.Pose, eventdata.Distance, Skip);
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

        if (eventdata.character.ToLower() == "all")
        {
            while(Actors.Count != 0)
            {
                Actors[0].ExitStage(Skip);
                Actors.Remove(Actors[0]);
            }
            
            for (var i = 0; i < SpotLights.Count; ++i)
            {
                SpotLights[(StagePosition)i] = 0;
            }

        }


        foreach (var Roll in Actors)
        {

            if (Roll == null)
            {
                Actors.Remove(Roll);
            }

            
            if (Roll.Character.Character == eventdata.character)
            {

                SpotLights[Roll.Direction] -= 1;
                Roll.ExitStage(Skip);
                Actors.Remove(Roll);
                UpdateStagePositions(Roll.Direction);
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
        float CenterPoint = 0;
        float Spacing = 0;
        switch(pos)
        {
            case StagePosition.Center:
                spot = gameObject.transform.position;
                CenterPoint = 0.5f;
                break;
            case StagePosition.Left:
                spot = LeftSpot.transform.position;
                CenterPoint = 1/4;
                break;
            case StagePosition.Right:
                spot = RightSpot.transform.position;
                CenterPoint = 3/4;
                break;
            default:
                break;
        }

        //if the number of people in that spot is more than the spacing given allows
        if(Varience * SpotLights[pos] > SpotSize + 1)
        {
            Spacing = SpotSize / SpotLights[pos];
            spot.x -= SpotSize * CenterPoint;
            print(SpotSize);
        }
        else if (SpotLights[pos] != 1)
        {
            Spacing = Varience;
            spot.x -= (Varience * (SpotLights[pos] - 1))/2;
        }
        

        foreach (var Roll in Actors)
        {
            

            if(Roll.Direction == pos)
            {
                spot.z = Roll.transform.localPosition.z;
                spot.y = Roll.transform.localPosition.y;

                if (!Skip)
                    iTween.MoveTo(Roll.gameObject, spot, SpriteMoveTime);
                else
                    Roll.transform.localPosition = spot;

                spot.x += Spacing;
                i += 1;
                if (i >= SpotLights[pos])
                    return;

            }

            
        }
    }


    void SceneryChange(StageDirectionEvent eventdata)
    {
        if(eventdata.Backdrop == CurrentRoom)
        {
            if (eventdata.Backdrop != Room.None || eventdata.character == CurrentCG)
                return;
        }
        
        if(eventdata.Backdrop == Room.None)
        {

            CurrentRoom = Room.None;
            CurrentCG = eventdata.character;

            foreach (var room in SpecialBackdrops)
            {
                if (room.Tag == CurrentCG)
                {
                    if (!Skip)
                        StartCoroutine(BackdropChange(room.Backdrop));
                    else
                    {

                        FrontCurtain.sprite = room.Backdrop;

                        FrontCurtain.color = Color.white;
                    }
                    

                }

            }
        }
        else
        {
            CurrentRoom = eventdata.Backdrop;
            CurrentCG = "";
            foreach (var room in Backdrop)
            {

                if (room.ID == CurrentRoom)
                {
                    if (!Skip)
                        StartCoroutine(BackdropChange(room.Backdrop));
                    else
                    {

                        FrontCurtain.sprite = room.Backdrop;

                        FrontCurtain.color = Color.white;
                    }

                }

            }
        }

        
        
    }

    IEnumerator BackdropChange(Sprite newBackdrop)
    {
        BackCuratin.sprite = newBackdrop;
        var Awhite = Color.white;
        Awhite.a = 0;
        FrontCurtain.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Awhite, BackgroundFadeTime));
        BackCuratin.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Color.white, BackgroundFadeTime));

        yield return new WaitForSeconds(BackgroundFadeTime);

        FrontCurtain.sprite = newBackdrop;
        FrontCurtain.color = Color.white;
        BackCuratin.color = Awhite;

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
    public Room Backdrop;
    public StagePosition Direction;
    public StageDistance Distance;

    public StageDirectionEvent(string person, string pose = "", StageDistance Dis = StageDistance.Center, StagePosition Pos = StagePosition.Center)
    {
        character = person;
        Pose = pose;
        Direction = Pos;
        Distance = Dis;
    }

    public StageDirectionEvent(Room scenery, string tag = "")
    {
        Backdrop = scenery;
        character = tag;
    }

    //changing someone's position
    public StageDirectionEvent(string person, StagePosition Dir)
    {
        character = person;
        Direction = Dir;
    }

    public StageDirectionEvent(string person, StageDistance Dis)
    {
        character = person;
        Distance = Dis;
    }

}

[Serializable]
public class RoomDetails
{
    public Room ID;
    public string Tag;
    public Sprite Backdrop;
}

public enum StagePosition
{
    None,
    Center,
    Left,
    Right
}
public enum StageDistance
{
    None,
    Close,
    Center,
    Far
}