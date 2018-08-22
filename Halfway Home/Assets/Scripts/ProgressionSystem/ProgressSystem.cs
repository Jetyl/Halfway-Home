﻿/******************************************************************************/
/*!
File:   ProgressSystem.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System;
using LitJson;

[System.Serializable]
public class ProgressSystem
{

    
    private Dictionary<string, ProgressPoint> ProgressBook;

    [SerializeField]
    private List<Beat> PlotLines;
    [SerializeField]
    List<Task> Objectives;
    [SerializeField]
    List<Task> ChronologicalObjectives;


    public ProgressSystem()
    {
        ProgressBook = new Dictionary<string, ProgressPoint>();
        PlotLines = new List<Beat>();
        

        var jBeat = TextParser.ToJson("PlotBeats");

        for (int ima = 0; ima < jBeat.Count; ++ima)
        {
            PlotLines.Add(new Beat(jBeat[ima], this));
        }

        Objectives = new List<Task>();
        ChronologicalObjectives = new List<Task>();

        var jTask = TextParser.ToJson("TaskListing");

        for (int ima = 0; ima < jTask.Count; ++ima)
        {
            Objectives.Add(new Task(jTask[ima]));
        }

        //updates plots on initialize
        UpdatePlotLines();

    }

    public ProgressSystem (ProgressSystem copy_)
    {
        ProgressBook = new Dictionary<string, ProgressPoint>();
        PlotLines = new List<Beat>();

        

        for (int ima = 0; ima < copy_.PlotLines.Count; ++ima)
        {
            PlotLines.Add(new Beat(copy_.PlotLines[ima], this));
        }

        Objectives = new List<Task>();
        ChronologicalObjectives = new List<Task>();

        

        for (int ima = 0; ima < copy_.Objectives.Count; ++ima)
        {
            Objectives.Add(new Task(copy_.Objectives[ima]));
        }

        for(int ima = 0; ima < copy_.ChronologicalObjectives.Count; ++ima)
        {
            ChronologicalObjectives.Add(Objectives[copy_.ChronologicalObjectives[ima].Number]);

            MonoBehaviour.print(ChronologicalObjectives[ima].GetState());
        }


        UpdatePlotLines();
    }

    public void LoadProgress()
    {
        foreach(var beat in PlotLines)
        {
            beat.LoadBeat(this);
        }
    }

    public void UpdateProgress(string _key, ProgressPoint change)
    {

        if (!ProgressBook.ContainsKey(_key))
            return;

        switch (ProgressBook[_key].TypeID)
        {
            case PointTypes.Flag:
                ProgressBook[_key].BoolValue = change.BoolValue;
                break;
            case PointTypes.Float:
                if (change.FloatValue != 0)
                    ProgressBook[_key].FloatValue = change.FloatValue;
                else
                    ProgressBook[_key].FloatValue = 0;
                break;
            case PointTypes.Integer:
                if (change.IntValue != 0)
                    ProgressBook[_key].IntValue = change.IntValue;
                else
                    ProgressBook[_key].IntValue = 0;
                break;
            case PointTypes.String:
                if (change.StringValue != null)
                    ProgressBook[_key].StringValue = change.StringValue;
                else
                    ProgressBook[_key].StringValue = "";
                break;
            default:
                break;
        }
        

        UpdatePlotLines();

    }

    public void SetValue<T>(string key, T value)
    {
        
        if (!ProgressBook.ContainsKey(key))
            return;
        
        Type type = typeof(T);
        if (type == typeof(int))
        {
            if (ProgressBook[key].TypeID == PointTypes.Integer)
                ProgressBook[key].IntValue = Convert.ToInt32(value);
        }
        if(type == typeof(bool))
        {
            if (ProgressBook[key].TypeID == PointTypes.Flag)
                ProgressBook[key].BoolValue = Convert.ToBoolean(value);
        }
        if (type == typeof(float))
        {
            if (ProgressBook[key].TypeID == PointTypes.Float)
                ProgressBook[key].FloatValue = (float)Convert.ToDouble(value);
        }
        if (type == typeof(string))
        {
            if (ProgressBook[key].TypeID == PointTypes.String)
                ProgressBook[key].StringValue = Convert.ToString(value);
        }

        UpdatePlotLines();
    }

    public void SetValue(string _key, bool change)
    {

        if (!ProgressBook.ContainsKey(_key))
            return;

        if (ProgressBook[_key].TypeID != PointTypes.Flag)
            return;
        
         
        ProgressBook[_key].BoolValue = change;



        UpdatePlotLines();

    }
    public void SetValue(string _key, int change)
    {

        if (!ProgressBook.ContainsKey(_key))
            return;

        if (ProgressBook[_key].TypeID != PointTypes.Integer)
            return;

        
        ProgressBook[_key].IntValue = change;
        

        UpdatePlotLines();

    }

    public void SetValue(string _key, float change)
    {

        if (!ProgressBook.ContainsKey(_key))
            return;

        if (ProgressBook[_key].TypeID != PointTypes.Float)
            return;

        
        ProgressBook[_key].FloatValue = change;
        
        UpdatePlotLines();

    }

    public void SetValue(string _key, string change)
    {

        if (!ProgressBook.ContainsKey(_key))
            return;

        if (ProgressBook[_key].TypeID != PointTypes.String)
            return;

        ProgressBook[_key].StringValue = change;
        
        UpdatePlotLines();

    }

    public void ResetProgress(string _key)
    {

        if (!ProgressBook.ContainsKey(_key))
            return;

        switch(ProgressBook[_key].TypeID)
        {
            case PointTypes.Flag:
                ProgressBook[_key].BoolValue = false;
                break;
            case PointTypes.Float:
                ProgressBook[_key].FloatValue = 0.0f;
                break;
            case PointTypes.Integer:
                ProgressBook[_key].IntValue = 0;
                break;
            case PointTypes.String:
                ProgressBook[_key].StringValue = "";
                break;
            default:
                break;
        }

        //ProgressBook[_key].BoolValue = _newValue;
        UpdatePlotLines();

    }

    public void DegradeProgress(string _key)
    {
        if (!ProgressBook.ContainsKey(_key))
            return;

        

        switch (ProgressBook[_key].TypeID)
        {
            case PointTypes.Flag:
                ProgressBook[_key].BoolValue = false;
                break;
            case PointTypes.Float:
                ProgressBook[_key].FloatValue -= 1;
                if (ProgressBook[_key].FloatValue < 0)
                    ProgressBook[_key].FloatValue = 0;
                break;
            case PointTypes.Integer:
                ProgressBook[_key].IntValue -= 1;
                if (ProgressBook[_key].IntValue < 0)
                    ProgressBook[_key].IntValue = 0;
                break;
            default:
                break;
        }

    }

    public bool CheckProgress(ProgressPoint key)
    {
        if (!ProgressBook.ContainsKey(key.ProgressName))
            return false;


        switch (ProgressBook[key.ProgressName].TypeID)
        {
            case PointTypes.Flag:
                return key.BoolValue == ProgressBook[key.ProgressName].BoolValue;
            case PointTypes.Float:
                return TextParser.CompareValues(key.compare, ProgressBook[key.ProgressName].FloatValue, key.FloatValue);
            case PointTypes.Integer:
                return TextParser.CompareValues(key.compare, ProgressBook[key.ProgressName].IntValue, key.IntValue);
            case PointTypes.String:
                return ProgressBook[key.ProgressName].StringValue == key.StringValue;
            default:
                break;
        }

        return false;
    }


    public void AddPoint (string key, ProgressPoint value)
    {

        if (Contains(key))
            return;

        ProgressBook.Add(key, value);


    }

    public string GetStringValue(string progress)
    {

        if (!Contains(progress))
            return "";

        if (ProgressBook[progress].TypeID != PointTypes.String)
            return "";

        return ProgressBook[progress].StringValue;


    }
    public bool GetBoolValue(string progress)
    {

        if (!Contains(progress))
            return false;

        if (ProgressBook[progress].TypeID != PointTypes.Flag)
            return false;

        return ProgressBook[progress].BoolValue;


    }
    public int GetIntValue(string progress)
    {

        if (!Contains(progress))
            return -1;

        if (ProgressBook[progress].TypeID != PointTypes.Integer)
            return -1;

        return ProgressBook[progress].IntValue;


    }
    public float GetFloatValue(string progress)
    {

        if (!Contains(progress))
            return 0;

        if (ProgressBook[progress].TypeID != PointTypes.Float)
            return 0;

        return ProgressBook[progress].FloatValue;


    }
    
    public List<Task> GetObjectives()
    {
      return ChronologicalObjectives;
    }

    public bool Contains(string key)
    {
        if (ProgressBook.ContainsKey(key))
            return true;

        return false;
    }

    //updates the plotlines, checkng off beats if they need to be checked off
    void UpdatePlotLines()
    {
        Space.DispatchEvent(Events.Progress);
    }

    public void UpdateTask(int Number, Task.TaskState newState, int SubTask = -1)
    {
      
        if (Objectives.Count <= Number)
            return;

        //Debug.Log("Task #" + Number + " (subtask:" + SubTask + ") is in state: " + newState);

        if (SubTask != -1 && Objectives[Number].SubTasks.Count > SubTask)
        {
            Objectives[Number].SubTasks[SubTask].SetState(newState);
            if (Objectives[Number].SubtasksComplete())
                Objectives[Number].SetState(Task.TaskState.Success);
            if (Objectives[Number].SubtasksFailure())
                Objectives[Number].SetState(Task.TaskState.Failed);

        }
        else
        {
            if(Objectives[Number].GetState() == Task.TaskState.Unstarted && newState != Task.TaskState.InProgress)
            {
                //you cannot succeed a task you didn't know existed. //this is kind of a janky quick fix, but whatev
                return;
            }

            if (newState != Task.TaskState.Unstarted && Objectives[Number].GetState() == Task.TaskState.Unstarted)
            {
                if (!(newState == Task.TaskState.InProgress && Objectives[Number].Hidden))
                    ChronologicalObjectives.Add(Objectives[Number]);
                    
            }

            Objectives[Number].SetState(newState);
        }
            


        //send any system updating events here

    }


    public void ResetBeats()
    {
        foreach (var beat in PlotLines)
        {
            //the global beats do no get reset normally
            if (beat.PlotName == "Global")
                continue;

            beat.ResetBeat();

        }
    }

    public void ResetSleep()
    {
        foreach (var beat in PlotLines)
        {
            //reset the daily grind
            if (beat.BeatName.ToLower() == "sleepy")
            {
                beat.ResetBeat();
                return;
            }
              
        }
    }

    public void ResetDay()
    {
        foreach (var beat in PlotLines)
        {
            //reset the daily grind
            if (beat.BeatName == "Daily")
            {
                foreach(var point in beat.Points)
                {
                    DegradeProgress(point.ProgressName);
                }
                //beat.ResetBeat();
                return;
            }


        }
    }

    public void ResetWeekly()
    {

        foreach (var beat in PlotLines)
        {
            //reset the weekly grind
            if (beat.BeatName == "Weekly")
            {
                beat.ResetBeat();
                return;
            }

        }

        ChronologicalObjectives.Clear();

        foreach (var task in Objectives)
        {
            task.Reset();

            if (task.GetState() != Task.TaskState.Unstarted)
            {
                if (!(task.GetState() == Task.TaskState.InProgress && task.Hidden))
                    ChronologicalObjectives.Add(task);
            }
                
        }

    }

}

[System.Serializable]
public class Beat
{
    
    public string PlotName = "";
    public string BeatName = "";

    public int BeatNumber; //number the beat is in the plotline

    public List<ProgressPoint> Points;
    

    //default Contructor
    public Beat ()
    {
        Points = new List<ProgressPoint>();
    }

    public Beat (Beat copy, ProgressSystem progress)
    {
        PlotName = copy.PlotName;
        BeatName = copy.BeatName;
        BeatNumber = copy.BeatNumber;

        Points = new List<ProgressPoint>();

        for (int i = 0; i < copy.Points.Count; ++i)
        {
            ProgressPoint point = new ProgressPoint(copy.Points[i]);

            Points.Add(point);
            progress.AddPoint(point.ProgressName, point);
        }
    }

    //main game constructor
    public Beat(JsonData beatData, ProgressSystem progress)
    {
        PlotName = (string)beatData["PlotName"];
        BeatName = (string)beatData["BeatName"];
        BeatNumber = (int)beatData["BeatNumber"];

        Points = new List<ProgressPoint>();

        for(int i = 0; i < beatData["Point"].Count; ++i)
        {
            string boolName = (string)beatData["Point"][i]["Name"];
            PointTypes type = (PointTypes)(int)beatData["Point"][i]["Type"];
            ProgressPoint point = new ProgressPoint(boolName, type);

            Points.Add(point);
            progress.AddPoint(boolName, point);
        }

    }

    //editor contructor
    public Beat(JsonData beatData)
    {
        PlotName = (string)beatData["PlotName"];
        BeatName = (string)beatData["BeatName"];
        BeatNumber = (int)beatData["BeatNumber"];

        Points = new List<ProgressPoint>();

        for (int i = 0; i < beatData["Point"].Count; ++i)
        {
            string boolName = (string)beatData["Point"][i]["Name"];
            PointTypes type = (PointTypes)(int)beatData["Point"][i]["Type"];
            ProgressPoint point = new ProgressPoint(boolName, type);
            Points.Add(point);
        }

    }


    //resets the bools associated with this beat, if beat is failed, or in progress
    public void ResetBeat()
    {

        foreach (var boo in Points)
        {
            Game.current.Progress.ResetProgress(boo.ProgressName);
        }
        
    }

    //reduces ints and floats in the beat
    public void DegradeBeat()
    {
        foreach (var boo in Points)
        {
            Game.current.Progress.ResetProgress(boo.ProgressName);
        }
        
    }

    public bool BeatComplete()
    {
        //if in progress, check bools
        foreach (var boo in Points)
        {
            if (boo.TypeID != PointTypes.Flag || boo.TypeID != PointTypes.None)
                continue;

            if(Game.current.Progress.CheckProgress(boo) == false)
            {
                return false;
            }
        }
        

        return true;
    }
    
    public void LoadBeat(ProgressSystem progress)
    {
        foreach (var point in Points)
            progress.AddPoint(point.ProgressName, point);
    }
    
}

[System.Serializable]
public class Task
{

    public enum TaskState
    {
        Unstarted,
        InProgress,
        Success,
        Failed
    }

    public int Number;

    public string Name;
    //public string Objective;
    public bool RemoveWeekly = true;
    public bool Hidden;
    public bool AllShow;
    public bool AllFail;
    public bool AllSuccess;

    [SerializeField]
    public List<Task> SubTasks;

    [SerializeField]
    private TaskState State;

    public Task(int number)
    {
        Number = number;
        Name = "";
        SubTasks = new List<Task>();
    }

    public Task(Task copy)
    {
        Number = copy.Number;
        Name = copy.Name;
        //Objective = (string)taskData["Objective"];
        SubTasks = new List<Task>();
        //MonoBehaviour.print(copy.SubTasks.Count);
        for (int i = 0; i < copy.SubTasks.Count; ++i)
        {
            var sub = new Task(copy.SubTasks[i].Number);
            sub.Name = copy.SubTasks[i].Name;
            sub.Hidden = copy.SubTasks[i].Hidden;
            sub.SetState(copy.SubTasks[i].State);
            SubTasks.Add(sub);
        }

        Hidden = copy.Hidden;
        AllShow = copy.AllShow;
        AllSuccess = copy.AllSuccess;
        AllFail = copy.AllFail;
        State = copy.State;
        RemoveWeekly = copy.RemoveWeekly;
    }

    public Task(JsonData taskData)
    {
        Number = (int)taskData["Number"];
        Name = (string)taskData["Name"];
        //Objective = (string)taskData["Objective"];
        SubTasks = new List<Task>();
        //MonoBehaviour.print((int)taskData["SubCount"]);
        for(int i = 0; i < (int)taskData["SubCount"]; ++i)
        {
            var sub = new Task((int)taskData["SubTasks"][i]["Number"]);
            sub.Name = (string)taskData["SubTasks"][i]["Name"];
            sub.Hidden = (bool)taskData["SubTasks"][i]["Hidden"];
            SubTasks.Add(sub);
        }

        Hidden = (bool)taskData["Hidden"];
        RemoveWeekly = (bool)taskData["Weekly"];

        AllShow = (bool)taskData["ShowAll"];
        AllSuccess = (bool)taskData["GoalAll"];
        AllFail = (bool)taskData["FailAll"];
        State = TaskState.Unstarted;
    }

    public TaskState GetState()
    {
        return State;
    }

    public void SetState(TaskState newState)
    {
        State = newState;

        if(State == TaskState.InProgress && AllShow)
        {
            foreach (Task sub in SubTasks)
                sub.SetState(State);
        }

        if(State == TaskState.Success)
        {
            //need to add reward
        }
        if (State == TaskState.Failed)
        {
            //maybe add a failure reward?
        }
    }

    public bool SubtasksComplete()
    {
        if (!AllSuccess)
            return false;

        foreach(var task in SubTasks)
        {
            if (task.GetState() != TaskState.Success)
                return false;
        }

        return true;
    }

    public bool SubtasksFailure()
    {
        if (!AllFail)
            return false;

        foreach (var task in SubTasks)
        {
            if (task.GetState() == TaskState.Failed)
                return true;
        }

        return false;
    }

    public void Reset()
    {
        if (!RemoveWeekly)
            return;

        foreach(var sub in SubTasks)
        {
            sub.SetState(TaskState.Unstarted);
        }

        State = TaskState.Unstarted;
    }

}