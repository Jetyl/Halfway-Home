using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CastDisplay : MonoBehaviour
{

    public List<CharacterList> CastList;
    
    
    public GameObject LeftSpot;
    public GameObject RightSpot;
    public float Varience = 2;
    public float SpotSize = 12;

    List<CharacterDisplay> Actors;

    Dictionary<StagePosition, int> SpotLights; //number of people in spot

    public Room StartingRoom;
    public float SpriteMoveTime = 2;

    bool Load = false;

    bool Skip = false;

    // Use this for initialization
    void Start()
    {
        Actors = new List<CharacterDisplay>();
        SpotLights = new Dictionary<StagePosition, int>();
        for (var i = 0; i < Enum.GetValues(typeof(StagePosition)).Length; ++i)
        {
            SpotLights.Add((StagePosition)i, 0);
        }

        Space.Connect<CastDirectionEvent>(Events.CharacterCall, CharacterChanges);
        //Space.Connect<CastDirectionEvent>(Events.CharacterExit, CharacterExit);

        Space.Connect<DefaultEvent>(Events.SkipTyping, OnSkip);
        Space.Connect<DefaultEvent>(Events.StopSkipTyping, OffSkip);

        Space.Connect<DefaultEvent>(Events.Save, OnSave);
        Space.Connect<DefaultEvent>(Events.Load, OnLoad);
        

    }

    // Update is called once per frame
    void Update()
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
        Game.current.CastCall = new List<CharacterIntermission>();

        foreach (var actor in Actors)
        {
            actor.OnSave();
        }
    }

    void OnLoad(DefaultEvent eventdata)
    {
        Load = true;

        foreach (var actor in Game.current.CastCall)
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
                        break;

                    }
                }

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
            
        //if the command is all, affect all
        if (eventdata.character.ToLower() == "all")
        {
            CallAllCast(eventdata.Pose);
            return;
        }

        //for characters already on scene
        foreach (var Roll in Actors)
        {

            if (Roll == null)
            {
                Actors.Remove(Roll);
            }

            if (Roll.Character.Character == eventdata.character)
            {

                Roll.ChangePose(eventdata.Pose, Skip);
                if(eventdata.Distance!=StageDistance.Same)Roll.ChangeDistance(eventdata.Distance);
                Roll.ChangeFacing(eventdata.FacingDirection);

                if (eventdata.Direction != StagePosition.None && eventdata.Direction != Roll.Direction)
                {
                    var oldDirections = Roll.Direction;
                    SpotLights[Roll.Direction] -= 1;
                    Roll.Direction = eventdata.Direction;
                    SpotLights[eventdata.Direction] += 1;

                    UpdateStagePositions(oldDirections);
                    UpdateStagePositions(eventdata.Direction);
                }
                
                return;
            }
        }
        

        //if here, character is not on scene
        foreach (var person in CastList)
        {
            //adding character to scene
            if (person.Character == eventdata.character)
            {

                var cast = Instantiate(person.Actor, Vector3.zero, Quaternion.identity);

                var directions = cast.GetComponent<CharacterDisplay>();
                if (directions == null)
                    Debug.LogError("character: " + eventdata.character + "is missing at rollcall. See StageDisplay");
                else
                {
                    if (eventdata.FacingDirection == StagePosition.None)
                        eventdata.FacingDirection = StagePosition.Center;

                    directions.EnterStage(eventdata.Pose, eventdata.Distance, eventdata.FacingDirection, Skip);
                    Actors.Add(directions);

                    if (eventdata.Direction == StagePosition.None)
                        eventdata.Direction = StagePosition.Center;

                    directions.Direction = eventdata.Direction;
                    SpotLights[directions.Direction] += 1;
                    UpdateStagePositions(directions.Direction);

                }
            }

        }



    }

    void CharacterExit(CastDirectionEvent eventdata)
    {

        if (eventdata.character.ToLower() == "all")
        {
            while (Actors.Count != 0)
            {
                Actors[0].ExitStage(eventdata.Direction, Skip);
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
                Roll.ExitStage(eventdata.Direction, Skip);
                Actors.Remove(Roll);
                UpdateStagePositions(Roll.Direction);
                return;
            }
        }
    }


    


    void UpdateStagePositions(StagePosition pos)
    {
        int i = 0;
        var spot = new Vector3();
        float CenterPoint = 0;
        float Spacing = 0;
        switch (pos)
        {
            case StagePosition.Center:
                spot = gameObject.transform.localPosition;
                CenterPoint = 0.5f;
                break;
            case StagePosition.Left:
                spot = LeftSpot.transform.localPosition;
                CenterPoint = 1 / 4;
                break;
            case StagePosition.Right:
                spot = RightSpot.transform.localPosition;
                CenterPoint = 3 / 4;
                break;
            default:
                break;
        }

        //if the number of people in that spot is more than the spacing given allows
        if (Varience * SpotLights[pos] > SpotSize + 1)
        {
            Spacing = SpotSize / SpotLights[pos];
            spot.x -= SpotSize * CenterPoint;
            print(SpotSize);
        }
        else if (SpotLights[pos] != 1)
        {
            Spacing = Varience;
            spot.x -= (Varience * (SpotLights[pos] - 1)) / 2;
        }


        foreach (var Roll in Actors)
        {


            if (Roll.Direction == pos)
            {
                spot.z = Roll.transform.localPosition.z;
                spot.y = Roll.transform.localPosition.y;

                if (!Skip)
                    Roll.MoveOnStage(spot, SpriteMoveTime);
                else
                    Roll.transform.localPosition = spot;

                spot.x += Spacing;
                i += 1;
                if (i >= SpotLights[pos])
                    return;

            }


        }
    }

    

    public CharacterList GetCastMember(string name)
    {
        for (int i = 0; i < CastList.Count; ++i)
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


    void CallAllCast(string Pose)
    {
        foreach (var Roll in Actors)
        {

            if (Roll == null)
            {
                Actors.Remove(Roll);
                continue;
            }
            
            Roll.ChangePose(Pose, Skip);
            
        }
    }
}

public class CastDirectionEvent : DefaultEvent
{
    public string character;
    public string Pose;
    public Vector3 Position;
    public StagePosition Direction = StagePosition.Center;
    public StagePosition FacingDirection = StagePosition.Right;
    public StageDistance Distance = StageDistance.Same;
    public bool Exiting;

    
    public CastDirectionEvent(string person, string pose = "", StageDistance Dis = StageDistance.Same, StagePosition Pos = StagePosition.Center, StagePosition face = StagePosition.Right)
    {
        character = person;
        Pose = pose;
        Direction = Pos;
        Distance = Dis;
        FacingDirection = face;
    }
    
    public CastDirectionEvent(string person, string calls)
    {
        character = person;
        Pose = "None";

        string[] directions = calls.Split(',');

        foreach (string call in directions)
        {
            //MonoBehaviour.print(call);
            var calling = call.Replace(" ", "");

            switch (calling.ToLower())
            {
                case "exit":
                    Exiting = true;
                    break;
                case "left":
                    FacingDirection = StagePosition.Left;
                    break;
                case "right":
                    FacingDirection = StagePosition.Right;
                    break;
                case "close":
                    Distance = StageDistance.Close;
                    break;
                case "medium":
                    Distance = StageDistance.Center;
                    break;
                case "far":
                    Distance = StageDistance.Far;
                    break;
                case "stage_right":
                    Direction = StagePosition.Right;
                    break;
                case "stage_left":
                    Direction = StagePosition.Left;
                    break;
                case "stage_center":
                    Direction = StagePosition.Center;
                    break;
                default:
                    Pose = calling;
                    break;
            }
            
        }
        

    }


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
    Same,
    Close,
    Center,
    Far
}