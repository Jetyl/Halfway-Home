using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class ProgressNode : BaseNode
{


    public ConnectionPoint BranchOutPoint;


    public int PassID;
    public int FailID;

    public ProgressPoint CheckPoint;

    public Texture2D InventoryMatch;

    public bool Current;

    public ProgressType TypeOfProgress;

    public Feelings MoodToMatch;

    public float MoodValueToMatch;

    public ValueCompare Compare;

    public int LuckRange;

    public float LuckPercentToFail;

    public bool AffectLuck;

    public PhoneDataTypes PhoneData;

    public string NoteTitle;

    public Sprite Image;

    public int TaskNumber;

    public Task.TaskState NewTaskState;

    public int Battery;

    public float Drain;

    public string BeatName;
    public Beat.BeatState BeatState;

    public SceneList SceneToCheck;
    public bool PreviousScene;

    public DayOfWeek Date;

    public string DreamName;

    //a null node
    public ProgressNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int NodeID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        inPoint = new ProgressionConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ProgressionConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        BranchOutPoint = new ProgressionConnectionPoint(this, ConnectionPointType.Branch, outPointStyle, OnClickOutPoint);

        TypeOfProgress = ProgressType.None;
        ID = NodeID;
        PassID = -1;
        FailID = -1;
        TypeID = NodeTypes.ProgressNode;
        CheckPoint = new ProgressPoint();
        MoodToMatch = Feelings.Hollow;
        MoodValueToMatch = 0;
        InventoryMatch = null;
        Current = false;
        LuckRange = 0;
        LuckPercentToFail = 0;
        PhoneData = PhoneDataTypes.None;
        NoteTitle = "";
        Image = null;
        TaskNumber = 0;
        NewTaskState = Task.TaskState.Unstarted;
        BeatName = "";
        BeatState = Beat.BeatState.Unstarted;
        SceneToCheck = new SceneList();
        DreamName = "";
    }


    public ProgressNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        inPoint = new ProgressionConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ProgressionConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        BranchOutPoint = new ProgressionConnectionPoint(this, ConnectionPointType.Branch, outPointStyle, OnClickOutPoint);

        TypeID = NodeTypes.ProgressNode;

        int ty = (int)data["TypeOfProgress"];
        TypeOfProgress = (ProgressType)ty;

        ID = (int)data["ID"];

        PassID = (int)data["PassID"];
        FailID = (int)data["FailID"];

        title = (string)data["title"];

        //CheckToMatch = (string)data["CheckToMatch"];
        //MatchValue = (bool)data["MatchValue"];

        if (data.Keys.Contains("CheckToMatch"))
            CheckPoint = new ProgressPoint(data["CheckToMatch"][0]);
        else
            CheckPoint = new ProgressPoint();

        if (data.Keys.Contains("MoodToMatch"))
        {
            int feel = (int)data["MoodToMatch"];
            MoodToMatch = (Feelings)feel;
        }
        else
            MoodToMatch = Feelings.Hollow;

        if (data.Keys.Contains("MoodValue"))
            MoodValueToMatch = (float)((double)data["MoodValue"]);
        else
            MoodValueToMatch = 0;

        if (data.Keys.Contains("Comparison"))
        {
            int com = (int)data["Comparison"];
            Compare = (ValueCompare)com;
        }
        else
            Compare = ValueCompare.EqualTo;

        if (data.Keys.Contains("LuckRange"))
            LuckRange = (int)data["LuckRange"];
        else
            LuckRange = 0;

        if (data.Keys.Contains("PercentFailure"))
            LuckPercentToFail = (float)((double)data["PercentFailure"]);
        else
            LuckPercentToFail = 0;

        if (data.Keys.Contains("AffectLuck"))
            AffectLuck = (bool)data["AffectLuck"];
        else
            AffectLuck = false;

        
        if(data.Keys.Contains("Slug") && data["Slug"] != null)
        {
            InventoryMatch = Resources.Load("Sprites/" + (string)data["Slug"]) as Texture2D;
        }

        if (data.Keys.Contains("PhoneDataType"))
        {
            int dat = (int)data["PhoneDataType"];
            PhoneData = (PhoneDataTypes)dat;
        }
        else
            PhoneData = PhoneDataTypes.None;


        if (data.Keys.Contains("NoteTitle"))
            NoteTitle = (string)data["NoteTitle"];
        else
            NoteTitle = "";

        

        if (data.Keys.Contains("PhoneImageSlug") &&  data["PhoneImageSlug"] != null)
        {
            Image = Resources.Load<Sprite>("Sprites/" + (string)data["PhoneImageSlug"]);

        }

        if (data.Keys.Contains("Current"))
            Current = (bool)data["Current"];
        else
            Current = false;

        if (data.Keys.Contains("TaskNumber"))
            TaskNumber = (int)data["TaskNumber"];
        else
            TaskNumber = 0;

        if (data.Keys.Contains("TaskState"))
        {
            int state = (int)data["TaskState"];
            NewTaskState = (Task.TaskState)state;
        }
        else
            NewTaskState = Task.TaskState.Unstarted;

        if (data.Keys.Contains("Battery"))
            Battery = (int)data["Battery"];
        else
            Battery = 0;

        if (data.Keys.Contains("Drain"))
            Drain = (float)(double)data["Drain"];
        else
            Drain = 0;
        
        if (data.Keys.Contains("BeatName"))
            BeatName = (string)data["BeatName"];
        else
            BeatName = "";

        if (data.Keys.Contains("Beat"))
            BeatState = (Beat.BeatState)(int)data["Beat"];

        if (data.Keys.Contains("Scene"))
            SceneToCheck = new SceneList((string)data["Scene"]);
        else
            SceneToCheck = new SceneList();

        if (data.Keys.Contains("Previous"))
            PreviousScene = (bool)data["Previous"];
        else
            PreviousScene = false;

        if (data.Keys.Contains("Date"))
        {
            int date = (int)data["Date"];
            Date = (DayOfWeek)date;
        }
        else
            Date = DayOfWeek.Monday;

        if (data.Keys.Contains("Dream"))
            DreamName = (string)data["Dream"];
        else
            DreamName = "";

    }


    public override void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
        BranchOutPoint.Draw();
        GUI.Box(rect, "", style);
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 40), new Vector2(150, 20)), "ID: " + ID);
        TypeOfProgress = (ProgressType)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 65), new Vector2(150, 20)), TypeOfProgress);
        AddDisplay();
    }

    void AddDisplay()
    {
        switch (TypeOfProgress)
        {
            case ProgressType.None:
                rect.size = new Vector2(200, 100);
                break;
            case ProgressType.ProgressPoint:
                rect.size = new Vector2(200, 155);
                CheckPoint.ProgressName = EditorGUI.TextField(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 20)), CheckPoint.ProgressName);
                CheckPoint.TypeID = (PointTypes)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 20)), CheckPoint.TypeID);
                ProgressPointDisplay();
                break;
            case ProgressType.Inventory:
                rect.size = new Vector2(200, 230);
                InventoryMatch = EditorGUI.ObjectField(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 100)), InventoryMatch, typeof(Texture2D), allowSceneObjects: true) as Texture2D;
                Current = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 200), new Vector2(150, 20)), new GUIContent("Currently in Inventory"), Current);

                break;
            case ProgressType.MoodAmount:
                rect.size = new Vector2(200, 180);
                MoodToMatch = (Feelings)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 20)), MoodToMatch);
                Compare = (ValueCompare)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 20)), Compare);
                MoodValueToMatch = EditorGUI.FloatField(new Rect(rect.position + new Vector2(25, 140), new Vector2(150, 20)), MoodValueToMatch);
                break;
            case ProgressType.MoodPercent:
                rect.size = new Vector2(200, 180);
                MoodToMatch = (Feelings)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 20)), MoodToMatch);
                Compare = (ValueCompare)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 20)), Compare);
                MoodValueToMatch = EditorGUI.FloatField(new Rect(rect.position + new Vector2(25, 140), new Vector2(150, 20)), MoodValueToMatch);
                break;
            case ProgressType.PrimaryMood:
                rect.size = new Vector2(200, 125);
                MoodToMatch = (Feelings)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 20)), MoodToMatch);
                break;
            case ProgressType.Lucky:
                rect.size = new Vector2(200, 180);
                EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 90), new Vector2(100, 20)), new GUIContent("Luck Range"));
                LuckRange = EditorGUI.IntField(new Rect(rect.position + new Vector2(125, 90), new Vector2(50, 20)), LuckRange);
                EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 115), new Vector2(100, 20)), new GUIContent("Luck Fail %"));
                LuckPercentToFail = EditorGUI.FloatField(new Rect(rect.position + new Vector2(125, 115), new Vector2(50, 20)), LuckPercentToFail);
                AffectLuck = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 140), new Vector2(150, 20)), new GUIContent("Affects Luck?"), AffectLuck);
                break;
            case ProgressType.PhoneData:
                rect.size = new Vector2(200, 125);
                PhoneData = (PhoneDataTypes)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 20)), PhoneData);
                PhoneDataDisplay();
                break;
            case ProgressType.PlotBeat:
                rect.size = new Vector2(200, 125);
                BeatName = EditorGUI.TextField(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 20)), BeatName);

                BeatState = (Beat.BeatState)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 110), new Vector2(150, 20)), BeatState);
                break;
            case ProgressType.Scene:
                

                SceneToCheck = EditorGUI.TextField(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 20)), SceneToCheck);
                PreviousScene = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 110), new Vector2(150, 20)), new GUIContent("Previous Scene?"), PreviousScene);
                break;
            case ProgressType.Date:
                Date = (DayOfWeek)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 110), new Vector2(150, 20)), Date);
                break;
            case ProgressType.Dream:
                DreamName = EditorGUI.TextField(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 20)), DreamName);
                break;
            default:
                Debug.LogError("Unrecognized Option");
                break;
        }
    }


    void ProgressPointDisplay()
    {
        switch (CheckPoint.TypeID)
        {
            case PointTypes.Flag:
                rect.size = new Vector2(200, 170);
                CheckPoint.BoolValue = EditorGUI.Toggle(new Rect(rect.position + new Vector2(25, 135), new Vector2(150, 20)), CheckPoint.BoolValue);
                break;
            case PointTypes.Float:
                rect.size = new Vector2(200, 195);
                CheckPoint.FloatValue = EditorGUI.FloatField(new Rect(rect.position + new Vector2(25, 135), new Vector2(150, 20)), CheckPoint.FloatValue);
                CheckPoint.compare = (ValueCompare)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 160), new Vector2(150, 20)), CheckPoint.compare);

                break;
            case PointTypes.Integer:
                rect.size = new Vector2(200, 195);
                CheckPoint.IntValue = EditorGUI.IntField(new Rect(rect.position + new Vector2(25, 135), new Vector2(150, 20)), CheckPoint.IntValue);
                CheckPoint.compare = (ValueCompare)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 160), new Vector2(150, 20)), CheckPoint.compare);

                break;
            case PointTypes.String:
                rect.size = new Vector2(200, 175);
                CheckPoint.StringValue = EditorGUI.TextField(new Rect(rect.position + new Vector2(25, 135), new Vector2(150, 20)), CheckPoint.StringValue);
                break;
            default:
                break;
        }
    }


    void PhoneDataDisplay()
    {
        switch (PhoneData)
        {
            case PhoneDataTypes.None:
                break;
            case PhoneDataTypes.Note:
                rect.size = new Vector2(200, 165);
                EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 110), new Vector2(35, 20)), new GUIContent("Title:"));
                NoteTitle = EditorGUI.TextField(new Rect(rect.position + new Vector2(25, 125), new Vector2(150, 20)), NoteTitle);
                break;
            case PhoneDataTypes.Pic:
                rect.size = new Vector2(200, 240);
                Image = EditorGUI.ObjectField(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 100)), Image, typeof(Sprite), allowSceneObjects: true) as Sprite;
                break;
            case PhoneDataTypes.Task:
                rect.size = new Vector2(200, 180);
                EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 115), new Vector2(100, 20)), new GUIContent("Task Number:"));

                TaskNumber = EditorGUI.IntField(new Rect(rect.position + new Vector2(125, 115), new Vector2(50, 20)), TaskNumber);
                NewTaskState = (Task.TaskState)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 140), new Vector2(150, 20)), NewTaskState);

                break;
            case PhoneDataTypes.Battery:
                rect.size = new Vector2(200, 180);
                EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 115), new Vector2(100, 20)), new GUIContent("Battery Percent:"));
                Battery = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(125, 115), new Vector2(50, 20)), Battery, 0, 100);
                Compare = (ValueCompare)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 140), new Vector2(150, 20)), Compare);

                break;
            case PhoneDataTypes.Drain:
                rect.size = new Vector2(200, 180);
                EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 115), new Vector2(100, 20)), new GUIContent("Battery Drain:"));

                Drain = EditorGUI.FloatField(new Rect(rect.position + new Vector2(125, 115), new Vector2(50, 20)), Drain);

                Compare = (ValueCompare)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 140), new Vector2(150, 20)), Compare);

                break;
            default:
                Debug.LogError("Unrecognized Option");
                break;
        }
    }


}
