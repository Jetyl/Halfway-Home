using UnityEngine;
using System.Collections.Generic;
using System;
using LitJson;

[System.Serializable]
public class ProgressSystem
{


    private Dictionary<string, ProgressPoint> ProgressBook;
    private List<Beat> PlotLines;
    

    public ProgressSystem()
    {
        ProgressBook = new Dictionary<string, ProgressPoint>();
        PlotLines = new List<Beat>();
        

        var jBeat = TextParser.ToJson("PlotBeats");

        for (int ima = 0; ima < jBeat.Count; ++ima)
        {
            PlotLines.Add(new Beat(jBeat[ima], this));
        }

        //updates plots on initialize
        UpdatePlotLines();

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
                ProgressBook[_key].StringValue = change.StringValue;
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

        
        ProgressBook[_key].FloatValue += change;
        


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


    public void AddBool (string key, ProgressPoint value)
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

    public bool Contains(string key)
    {
        if (ProgressBook.ContainsKey(key))
            return true;

        return false;
    }

    //updates the plotlines, checkng off beats if they need to be checked off
    void UpdatePlotLines()
    {
        foreach(var beat in PlotLines)
        {
            //if the Beat is Global, we do not care about it
            if (beat.PlotName == "Global")
                continue;



            if (beat.BeatComplete())
            {
                
                string id = beat.PlotName;
                int num = beat.BeatNumber;

                foreach (var beat2 in PlotLines)
                {
                    string id2 = beat.PlotName;
                    int num2 = beat.BeatNumber;
                    //if it is in the same line, and is the next step
                    if(id == id2 && num + 1 == num2)
                    {
                        beat2.StartBeat();//starts beat (does nothing if already started)

                    }
                    
                }
            }


        }
    }

    public bool CheckBeatState(string beatName, Beat.BeatState state)
    {
        foreach (var beat in PlotLines)
        {
            if (beat.BeatName == beatName)
            {
                return beat.GetState() == state;
            }
              
        }
        return false;
    }

    public void BeatFailed(string beatName)
    {
        foreach (var beat in PlotLines)
        {
            if (beat.BeatName == beatName)
                beat.BeatFailed();

        }
    }

    public void BeatFailed(string plotName, int beatNumber)
    {
        foreach (var beat in PlotLines)
        {
            if (beat.PlotName == plotName && beat.BeatNumber == beatNumber)
                beat.BeatFailed();

        }
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

    public void ResetDaily()
    {
        foreach (var beat in PlotLines)
        {
            //reset the daily grind
            if (beat.BeatName == "Daily")
            {
                beat.ResetBeat();
                return;
            }
                

        }
    }
}

[System.Serializable]
public class Beat
{

    public enum BeatState
    {
        Unstarted,
        InProgress,
        Success,
        Failed
    }

    public string PlotName;
    public string BeatName;

    public int BeatNumber; //number the beat is in the plotline

    public List<ProgressPoint> Points;
    

    private BeatState State;

    //default Contructor
    public Beat ()
    {
        Points = new List<ProgressPoint>();
        State = BeatState.Unstarted;
    }

    //main game constructor
    public Beat(JsonData beatData, ProgressSystem progress)
    {
        PlotName = (string)beatData["PlotName"];
        BeatName = (string)beatData["BeatName"];
        BeatNumber = (int)beatData["BeatNumber"];
        State = BeatState.Unstarted;

        Points = new List<ProgressPoint>();

        for(int i = 0; i < beatData["Point"].Count; ++i)
        {
            string boolName = (string)beatData["Point"][i]["Name"];
            PointTypes type = (PointTypes)(int)beatData["Point"][i]["Type"];
            ProgressPoint point = new ProgressPoint(boolName, type);

            Points.Add(point);
            progress.AddBool(boolName, point);
        }

    }

    //editor contructor
    public Beat(JsonData beatData)
    {
        PlotName = (string)beatData["PlotName"];
        BeatName = (string)beatData["BeatName"];
        BeatNumber = (int)beatData["BeatNumber"];
        State = BeatState.Unstarted;

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
        //only reset progress to failed, and inprogress beats
        if (State == BeatState.Success || State == BeatState.Unstarted)
            return;

        foreach (var boo in Points)
        {
            Game.current.Progress.ResetProgress(boo.ProgressName);
        }

        State = BeatState.InProgress;
    }

    public bool BeatComplete()
    {
        if (State == BeatState.Success)
            return true;

        if (State == BeatState.Failed || State == BeatState.Unstarted)
            return false;

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

        State = BeatState.Success;

        return true;
    }

    public void StartBeat()
    {
        if (State == BeatState.Unstarted)
            State = BeatState.InProgress;
    }

    public void BeatFailed()
    {
        State = BeatState.Failed;
    }

    public BeatState GetState()
    {
        return State;
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
    public int Set;

    public string Name;
    public string Objective;

    public bool Hidden;

    public string ConnectedBeat;

    private TaskState State;

    public Task(int number)
    {
        Number = number;
        Name = "";
        ConnectedBeat = "Independent Interactions";
    }

    public Task(JsonData taskData)
    {
        Number = (int)taskData["Number"];
        Name = (string)taskData["Name"];
        Objective = (string)taskData["Objective"];
        Set = (int)taskData["Set"];
        Hidden = (bool)taskData["Hidden"];
        ConnectedBeat = (string)taskData["Beat"];
        State = TaskState.Unstarted;
    }

    public TaskState GetState()
    {
        return State;
    }

    public void SetState(TaskState newState)
    {
        State = newState;

        if(State == TaskState.Success)
        {
            //need to add reward
        }
        if (State == TaskState.Failed)
        {
            //maybe add a failure reward?
        }
    }

    

}