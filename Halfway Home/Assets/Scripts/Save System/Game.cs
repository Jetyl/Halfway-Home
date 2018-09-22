/******************************************************************************/
/*!
File:   Game.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using LitJson;
using Stratus.Modules.InkModule;

[System.Serializable]
public class Game
{

    public static Game current { get; set; }
    
    public string PlayerName;

    public Room CurrentRoom;

    public int Day;

    public int Hour;

    [SerializeField]
    private int CurrentTimeBlock;
    [SerializeField]
    private bool DrainEnergy;

    public ProgressSystem Progress;
    public Personality Self;

    //character name, day of week, the location that hour
    [SerializeField]
    private List<CharacterSchedule> Schedule;

    private Dictionary<string, TimeStamp> SceneList;

    [SerializeField]
    private List<TimeStamp> SavedSceneList;

    public Dictionary<string, string> ScenePath;

    [SerializeField]
    private List<SceneData> SavedPathData;

    public bool InCurrentStory;

    [SerializeField]
    private string CurrentStory;

    public int CurrentNode;

    public string CurrentBackdrop;
    public string CurrentCG;
    public List<string> CGCalls = new List<string>();

    public string SavedInk;

    public string CurrentTrack = "";
    public string CurrentAmbience = "";
    public string CurrentStorySoundbank = "story_memory";
    public string CurrentRoomSoundbank = "";
    
    // Current RTPC values
    public float CurrentAmbienceLPF;
    public float CurrentAmbienceVol;
    public float CurrentMusicLPF;
    public float CurrentMusicTensionState;
    public float CurrentMusicVol;
    public float CurrentTextVol;

    [SerializeField]
    public List<CharacterIntermission> CastCall;

    public string CurrentHistory = "";
    public string CurrentSpeaker = "";
    
    public DateTime SaveStamp;

    [SerializeField]
    private long SavedDate;

    public Dictionary<string, string> AllHistories;

    [SerializeField]
    private List<SceneHistory> SavedHistoryData;

    public StorySave StorySave = new StorySave();


    public Game()
    {
        Day = 0;
        Hour = 0;
        PlayerName = "Sam";
        CurrentRoom = Room.YourRoom;
        Progress = new ProgressSystem();
        Self = new Personality();

        //Progress.SetValue("MasterVolume", 1.0f);
        //Progress.SetValue("MusicVolume", 1.0f);
        //Progress.SetValue("SFXVolume", 1.0f);
        //Progress.SetValue("AmbianceVolume", 1.0f);
        //Progress.SetValue("InterfaceVolume", 1.0f);
        //Progress.SetValue("MuteTextScroll", false);
        //Progress.SetValue("TextSpeed", 1.0f);
        Progress.SetValue("week", 1);
        Progress.SetValue("Depression Time Dilation", true);
        Progress.SetValue("Guardian", "parents");

        Schedule = new List<CharacterSchedule>();
        SceneList = new Dictionary<string, TimeStamp>();
        CastCall = new List<CharacterIntermission>();
        ScenePath = new Dictionary<string, string>();
        AllHistories = new Dictionary<string, string>();

        var schedulefiller = TextParser.ToJson("Characters");

        foreach (JsonData character in schedulefiller)
        {
            var Aday = new List<DaySchedule>();

            for (int i = 0; i <= 7; ++i)
            {
                var hours = new DaySchedule();
                //var seen = new List<bool>();
                for (int j = 0; j < 24; ++j)
                {
                    var lol = (Room)(int)character["Schedule"][i][j];
                    hours.Schedule.Add(lol);
                    //seen.Add(false);
                }

                Aday.Add(hours);
            }

            Schedule.Add(new CharacterSchedule((string)character["Name"], Aday));

        }
        
    }

    public Game(Game copy_)
    {
        Day = copy_.Day;
        Hour = copy_.Hour;
        PlayerName = copy_.PlayerName;

        Progress = new ProgressSystem(copy_.Progress);
        Self = new Personality(copy_.Self);

        Schedule = new List<CharacterSchedule>();
        SceneList = new Dictionary<string, TimeStamp>();
        CastCall = new List<CharacterIntermission>();
        ScenePath = new Dictionary<string, string>();
        AllHistories = new Dictionary<string, string>();

        foreach (CharacterSchedule character in copy_.Schedule)
        {
            var Aday = new List<DaySchedule>();

            for (int i = 0; i <= 7; ++i)
            {
                var hours = new DaySchedule();
                for (int j = 0; j < 24; ++j)
                {
                    var lol = character.Day[i].Schedule[j];
                    hours.Schedule.Add(lol);
                }

                Aday.Add(hours);
            }

            Schedule.Add(new CharacterSchedule(character.name, Aday));

        }

        foreach (string scene in copy_.SceneList.Keys)
        {
            SceneList.Add(scene, new TimeStamp(copy_.SceneList[scene]));

        }
        
        for(int i = 0; i < copy_.CastCall.Count; ++i)
        {
            CastCall.Add(new CharacterIntermission(copy_.CastCall[i]));
        }

        foreach (string scene in copy_.ScenePath.Keys)
        {
            ScenePath.Add(scene, (copy_.ScenePath[scene]));

        }

        foreach (string scene in copy_.AllHistories.Keys)
        {
            AllHistories.Add(scene, (copy_.AllHistories[scene]));

        }

        StorySave.Overwrite(copy_.StorySave);

        CurrentRoom = copy_.CurrentRoom;
        CurrentTimeBlock = copy_.CurrentTimeBlock;
        DrainEnergy = copy_.DrainEnergy;
        InCurrentStory = copy_.InCurrentStory;
        CurrentStory = copy_.CurrentStory;
        CurrentNode = copy_.CurrentNode;
        CurrentBackdrop = copy_.CurrentBackdrop;
        CurrentCG = copy_.CurrentCG;

        CGCalls = new List<string>();
        for(int i = 0; i < copy_.CGCalls.Count; ++i)
        {
            CGCalls.Add(copy_.CGCalls[i]);
        }

        SavedInk = copy_.SavedInk;
        CurrentTrack = copy_.CurrentTrack;
        CurrentAmbience = copy_.CurrentAmbience;
        CurrentStorySoundbank = copy_.CurrentStorySoundbank;
        CurrentRoomSoundbank = copy_.CurrentRoomSoundbank;
        
        // Current RTPC values
        CurrentAmbienceLPF = copy_.CurrentAmbienceLPF;
        CurrentAmbienceVol = copy_.CurrentAmbienceVol;
        CurrentMusicLPF = copy_.CurrentMusicLPF;
        CurrentMusicTensionState = copy_.CurrentMusicTensionState;
        CurrentMusicVol = copy_.CurrentMusicVol;
        CurrentTextVol = copy_.CurrentTextVol;
        
        CurrentHistory = copy_.CurrentHistory;
        CurrentSpeaker = copy_.CurrentSpeaker;

        SaveStamp = copy_.SaveStamp;


    }


    public void SaveGame()
    {
        if (InCurrentStory)
        {

        }

        SaveStamp = DateTime.Now;
        SavedDate = SaveStamp.ToFileTime();

        //MonoBehaviour.print("Save #" + SaveStamp.Hour + SaveStamp.Minute + SaveStamp.Second);

        Self.Save();

        SavedSceneList = new List<TimeStamp>();

        foreach( string scene in SceneList.Keys)
        {
            SceneList[scene].Scene = scene;
            SavedSceneList.Add(new TimeStamp(SceneList[scene]));
        }

        SavedPathData = new List<SceneData>();

        foreach (string scene in ScenePath.Keys)
        {
            SavedPathData.Add(new SceneData(scene, ScenePath[scene]));

        }

        SavedHistoryData = new List<SceneHistory>();

        foreach (string scene in AllHistories.Keys)
        {
            SavedHistoryData.Add(new SceneHistory(scene, AllHistories[scene]));

        }

    }


    public void LoadGame()
    {
        Progress.LoadProgress();
        Self.Load();

        SaveStamp = DateTime.FromFileTime(SavedDate);

        SceneList = new Dictionary<string, TimeStamp>();
        for (int i = 0; i < SavedSceneList.Count; ++i)
        {
            SceneList.Add(SavedSceneList[i].Scene, SavedSceneList[i]);
        }

        ScenePath = new Dictionary<string, string>();
        for(int j = 0; j < SavedPathData.Count; ++j)
        {
            ScenePath.Add(SavedPathData[j].name, SavedPathData[j].path);
        }

        AllHistories = new Dictionary<string, string>();
        for (int j = 0; j < SavedHistoryData.Count; ++j)
        {
            AllHistories.Add(SavedHistoryData[j].name, SavedHistoryData[j].History);
        }

    }

    public void SoftReset()
    {
        Day = 0;

        Progress.ResetBeats();

        //add reset tasks
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
        if (Hour >= 24)
        {
            Hour -= 24;
            NewDay();
        }

        Self.IncrementWellbeingStat(Personality.Wellbeing.Depression, 1 * CurrentTimeBlock);

        if (DrainEnergy)
        {
            //Self.IncrementWellbeingStat(Personality.Wellbeing.Fatigue, 10 * CurrentTimeBlock);
            Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(10 * CurrentTimeBlock, Personality.Wellbeing.Fatigue));
        }
        
        //Space.DispatchEvent(Events.StatChange);

        DrainEnergy = false;
        CurrentTimeBlock = 0;

        Space.DispatchEvent(Events.TimeChange);
    }

    public void SetTimeBlock(int Amount)
    {
        //Debug.Log(Amount);
        CurrentTimeBlock = Amount;
        DrainEnergy = true;
    }

    public void SetTimeBlock(int Amount, bool DrainFatigue)
    {
        //Debug.Log(Amount);
        CurrentTimeBlock = Amount;
        DrainEnergy = DrainFatigue;
    }
    public bool HasSceneBeenSeen(string scene_name, TimeStamp time_stamp)
    {
        if (!SceneList.ContainsKey(scene_name))
        {
            return false;
        }

        if (SceneList[scene_name].Exclusive == false)
            return true;
        else
            return SceneList[scene_name] == time_stamp;
    }

    public void SetSceneData(string scene_name, TimeStamp time_stamp, bool Override = false)
    {
        if (!SceneList.ContainsKey(scene_name))
        {
            SceneList.Add(scene_name, time_stamp);
        }
        else if(Override)
        {
            SceneList[scene_name] = time_stamp;
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
        return ((Hour + (24 * Day)) - (HourToCheck + (24 * Date)) <= LengthOfTime);
    }

    public int GetNewTimeAfterDuration(int InitialHour, int Duration)
    {
        var newHour = InitialHour + 1 + Duration;
        if (newHour >= 24) newHour -= 24;
        return newHour;
    }

    public int TimeDifference(int time, int Date)
    {
        var dif = 0;

        dif = (Hour + (Day * 24)) - (time + (Date * 24));

        return dif;

    }

    public void SetCurrentStory(string name, string path)
    {
        if (!ScenePath.ContainsKey(name))
            ScenePath.Add(name, path);
        CurrentStory = path;
    }

    public string GetCurrentStory()
    {
        return CurrentStory;
    }


    public void UpdateHistory(string history)
    {
        if (AllHistories.ContainsKey(CurrentStory))
            AllHistories[CurrentStory] = history;
        else
            AllHistories.Add(CurrentStory, history);
    }

    public string GetHistory()
    {
        if (AllHistories.ContainsKey(CurrentStory))
            return AllHistories[CurrentStory];
        else
            return "";
    }

    public bool UnlockAchievement(string Achievment_ID)
    {
        MonoBehaviour.print("Acheivment Got: " + Achievment_ID);
        if(GameLoad.IsSteamEnabled())
        {
            bool Got;
            Steamworks.SteamUserStats.GetAchievement(Achievment_ID, out Got);
            if(!Got)
            {
            Steamworks.SteamUserStats.SetAchievement(Achievment_ID);
            return Steamworks.SteamUserStats.StoreStats();
            }
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
    public string Scene;
    public int day;
    public int hour;
    public int duration;
    public bool Exclusive = false;

    public TimeStamp(int _day, int _hour, int _duration, bool _Exclusive)
    {
        day = _day;
        hour = _hour;
        duration = _duration;
        Exclusive = _Exclusive;
    }

    public TimeStamp(TimeStamp copy_)
    {
        Scene = copy_.Scene;
        day = copy_.day;
        hour = copy_.hour;
        duration = copy_.duration;
        Exclusive = copy_.Exclusive;
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

[Serializable]
public class CharacterSchedule
{
    public string name;

    [SerializeField]
    public List<DaySchedule> Day;

    public CharacterSchedule (string name_, List<DaySchedule> schedule_)
    {
        name = name_;
        Day = schedule_;
    }
}

[Serializable]
public class DaySchedule
{
    public List<Room> Schedule = new List<Room>();
}

[Serializable]
public class SceneData
{
    public string name;

    public string path;

    public SceneData(string name_, string path_)
    {
        name = name_;
        path = path_;
    }
}

[Serializable]
public class SceneHistory
{
    public string name;

    public string History;

    public SceneHistory(string name_, string history_)
    {
        name = name_;
        History = history_;
    }
}