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

    //character name, day of week, then has seen whereabouts that hour
    private Dictionary<string, List<List<bool>>> ScheduleUnderstanding;

    private Dictionary<Room, List<List<SceneSeen>>> WatchList;

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
        ScheduleUnderstanding = new Dictionary<string, List<List<bool>>>();
        CastCall = new List<CharacterIntermission>();

        var schedulefiller = TextParser.ToJson("Characters");

        foreach(JsonData character in schedulefiller)
        {
            var Aday = new List<List<Room>>();
            var Cday = new List<List<bool>>();

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
                Cday.Add(seen);
            }

            Schedule.Add((string)character["Name"], Aday);
            ScheduleUnderstanding.Add((string)character["Name"], Cday);

        }

        WatchList = new Dictionary<Room, List<List<SceneSeen>>>();
        for (var i = 0; i < Enum.GetValues(typeof(Room)).Length; ++i)
        {
            var Bday = new List<List<SceneSeen>>();

            for (int k = 0; k <= 7; ++k)
            {
                var See = new List<SceneSeen>();
                for (int j = 0; j < 24; ++j)
                {
                    See.Add(SceneSeen.Unseen);
                }

                Bday.Add(See);
            }

            WatchList.Add((Room)i, Bday);
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
        Space.DispatchEvent(Events.TimeChange);

        if(DrainEnergy)
            Self.IncrementWellbeingStat(Personality.Wellbeing.fatigue, 10 * CurrentTimeBlock);
        Self.IncrementWellbeingStat(Personality.Wellbeing.delusion, 1 * CurrentTimeBlock);
        Space.DispatchEvent(Events.StatChange);

        DrainEnergy = false;
        CurrentTimeBlock = 0;
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

    public bool KnowsWhereAbouts(string character)
    {
        return ScheduleUnderstanding[character][Day][Hour];
        
    }

    public void UpdateScheduleUnderstanding(string Character)
    {
        ScheduleUnderstanding[Character][Day][Hour] = true;

        int i = Hour + 1;
        int j = Day;

        //if they character stays there for several hours, update that here
        while(Schedule[Character][j][i] == Schedule[Character][Day][Hour])
        {
            ScheduleUnderstanding[Character][j][i] = true;

            i += 1;

            if(i == 24)
            {
                i = 0;
                j += 1;
            }
        }

    }

    public SceneSeen FlagMap(Room location)
    {
        return WatchList[location][Day][Hour];
    }

    public void SetSeenFlag(Room location, int Length, bool Complete, int day = -1, int hour = -1)
    {

        if (day == -1)
            day = Day;
        if (hour == -1)
            hour = Hour;

        for(int i = 0; i < Length; ++i)
        {
            if (Complete)
                WatchList[location][day][hour + i] = SceneSeen.Completed;
            else
                WatchList[location][day][hour + i] = SceneSeen.Seen;

            if (hour + i == 24)
            {

                while (hour + i > 0)
                {
                    hour -= 1;
                }
                

                day += 1;
            }
        }

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
public enum SceneSeen
{
    Unseen,
    Seen,
    Completed
}