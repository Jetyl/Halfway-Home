/******************************************************************************/
/*!
File:   Game.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System;
using LitJson;

[System.Serializable]
public class Game
{

    public static Game current;

    public string PlayerName;

    public Room CurrentRoom;

    public int Day;

    public int Hour;

    private int CurrentTimeBlock;
    private bool DrainEnergy;

    public ProgressSystem Progress;

    public Personality Self;

    //character name, day of week, the location that hour
    private Dictionary<string, List<List<Room>>> Schedule;

    private Dictionary<string, TimeStamp> SceneList;
    
    public bool InCurrentStory;

    public string CurrentStory;

    public int CurrentNode;

    public string CurrentCG;

    public string SavedInk;

    public List<CharacterIntermission> CastCall;

    public string CurrentHistory = "";
    public string CurrentSpeaker = "";

    public Game()
    {
       
        Day = 0;
        Hour = 0;
        PlayerName = "Sam";
        CurrentRoom = Room.YourRoom;
        Progress = new ProgressSystem();
        Self = new Personality();

        Progress.SetValue("MasterVolume", 1.0f);
        Progress.SetValue("BackgroundVolume", 1.0f);
        Progress.SetValue("SFXVolume", 1.0f);
        Progress.SetValue("TextSpeed", 1.0f);
        Progress.SetValue("week", 1);
        Progress.SetValue("Depression Time Dilation", true);

        Schedule = new  Dictionary<string, List<List<Room>>>();
        SceneList = new Dictionary<string, TimeStamp>();
        CastCall = new List<CharacterIntermission>();

        var schedulefiller = TextParser.ToJson("Characters");

        foreach(JsonData character in schedulefiller)
        {
            var Aday = new List<List<Room>>();

            for (int i = 0; i <= 7; ++i)
            {
                var hours = new List<Room>();
                var seen = new List<bool>();
                for (int j = 0; j < 24; ++j)
                {
                    var lol = (Room)(int)character["Schedule"][i][j];
                    hours.Add(lol);
                    seen.Add(false);
                }

                Aday.Add(hours);
            }

            Schedule.Add((string)character["Name"], Aday);

        }
        
    }

    public Game(Game copy_)
    {
        
        Day = copy_.Day;
        PlayerName = copy_.PlayerName;
        Progress = copy_.Progress;
        Self = copy_.Self;

    }


    public void SaveGame()
    {
        if(InCurrentStory)
        {
            
        }


    }

    public void LoadGame()
    {
       
    }

    public void SoftReset()
    {
        Day = 0;

        Progress.ResetBeats();
        
    }

    public void HardReset()
    {
        PlayerName = "";
        
        Day = 0;
        Progress = new ProgressSystem();

    }

    public void AlterTime()
    {
        //Debug.Log(CurrentTimeBlock);
        
        Hour += CurrentTimeBlock;
        if(Hour >= 24)
        {
            Hour -= 24;
            NewDay();
        }

        if(DrainEnergy)
            Self.IncrementWellbeingStat(Personality.Wellbeing.Fatigue, 10 * CurrentTimeBlock);
        Self.IncrementWellbeingStat(Personality.Wellbeing.Depression, 1 * CurrentTimeBlock);
        Space.DispatchEvent(Events.StatChange);

        DrainEnergy = false;
        CurrentTimeBlock = 0;

        Space.DispatchEvent(Events.TimeChange);
    }

    public void SetTimeBlock(int Amount)
    {
        Debug.Log(Amount);
        CurrentTimeBlock = Amount;
        DrainEnergy = true; 
    }

    public void SetTimeBlock(int Amount, bool DrainFatigue)
    {
        Debug.Log(Amount);
        CurrentTimeBlock = Amount;
        DrainEnergy = DrainFatigue;
    }
    public bool HasSeenBeenScene(string scene_name, TimeStamp time_stamp)
    {
        if(!SceneList.ContainsKey(scene_name))
        {
            return false;
        }

        return SceneList[scene_name] == time_stamp;
    }

    public void SetSceneData(string scene_name, TimeStamp time_stamp)
    {
        if (!SceneList.ContainsKey(scene_name))
        {
            SceneList.Add(scene_name, time_stamp);
        }
    }
    public bool IsSceneUnlocked(string scene_name, TimeStamp time_stamp)
    {
        if (!SceneList.ContainsKey(scene_name))
        {
            return true;
        }

        return SceneList[scene_name] == time_stamp;
    }
    
    public void NewDay()
    {

        Progress.ResetDay();

        Day += 1;
        //update the day in the progression system
        var point = new ProgressPoint("Day", PointTypes.Integer);
        point.IntValue = Day;

        Progress.UpdateProgress("Day", point);
        
        //checks if any special conditions have occured, to change rooms or anything.
            // prolly do this in scene checks, not here

        //cast new day event
        Space.DispatchEvent(Events.NewDay);

    }

    public void Slept()
    {
        Progress.ResetSleep();
    }

    public bool WithinTimeDifference(int HourToCheck, int Date, int LengthOfTime)
    {
        if(Date == Day)
        {
            if (Hour - HourToCheck <= LengthOfTime)
                return true;
        }
        else
        {
            if ((Hour * (24 * (Day - Date))) - HourToCheck <= LengthOfTime)
                return true;
        }


        return false;
    }


}
[Serializable]
public enum Room
{
    None,
    YourRoom,
    Commons,
    FrontDesk,
    Kitchen,
    Garden,
    Library,
    ArtRoom,
    Store,
    CharlottesRoom,
    EduardosRoom,
    Sleeping
}

[Serializable]
public class TimeStamp
{
    public int day;
    public int hour;
    public int duration;

    public TimeStamp(int _day, int _hour, int _duration)
    {
        day = _day;
        hour = _hour;
        duration = _duration;
    }

    public static bool operator ==(TimeStamp x, TimeStamp y)
    {
        if (x.day == y.day && x.hour == y.hour && x.duration == y.duration) return true;
        return false;
    }

    public static bool operator !=(TimeStamp x, TimeStamp y)
    {
        if (x.day != y.day || x.hour != y.hour || x.duration != y.duration) return true;
        return false;
    }


    public override bool Equals(System.Object obj)
    {
        if (obj == null)
            return false;
        TimeStamp c = obj as TimeStamp;
        if ((System.Object)c == null)
            return false;
        return (day == c.day && hour == c.hour && duration == c.duration);
    }
    public bool Equals(TimeStamp c)
    {
        if ((object)c == null)
            return false;
        return (day == c.day && hour == c.hour && duration == c.duration);
    }

    public override int GetHashCode()
    {
        return this.day.GetHashCode();
    }

}