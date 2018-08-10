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

    public ValueCompare Compare;

    public string TaskID;

    public string NoteTitle;

    public Sprite Image;

    public int TaskNumber;

    public Task.TaskState NewTaskState;

    public int Battery;

    public float Drain;

    public string BeatName;
    
    public bool PreviousScene;

    public DayOfWeek Date;

    public string DreamName;

    //a null node
    public ProgressNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, int NodeID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
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
        
        Current = false;
        
        NoteTitle = "";
        Image = null;
        TaskNumber = 0;
        NewTaskState = Task.TaskState.Unstarted;
        BeatName = "";
        DreamName = "";
    }


    public ProgressNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        inPoint = new ProgressionConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ProgressionConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        BranchOutPoint = new ProgressionConnectionPoint(this, ConnectionPointType.Branch, outPointStyle, OnClickOutPoint);


        if (data.Keys.Contains("color"))
            ChangeColor((int)data["color"]);

        TypeID = NodeTypes.ProgressNode;

        if (data.Keys.Contains("TypeOfProgress"))
        {
            int ty = (int)data["TypeOfProgress"];
            TypeOfProgress = (ProgressType)ty;
        }
        else
            TypeOfProgress = ProgressType.ProgressPoint;
        

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

        

        if (data.Keys.Contains("Comparison"))
        {
            int com = (int)data["Comparison"];
            Compare = (ValueCompare)com;
        }
        else
            Compare = ValueCompare.EqualTo;

        

        
        if(data.Keys.Contains("Slug") && data["Slug"] != null)
        {
            InventoryMatch = Resources.Load("Sprites/" + (string)data["Slug"]) as Texture2D;
        }

        

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
            case ProgressType.Objective:
                rect.size = new Vector2(200, 190);
                TaskID = EditorGUI.TextField(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 20)), TaskID);
                NewTaskState = (Task.TaskState)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 140), new Vector2(150, 20)), NewTaskState);
                //SendNoification = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 160), new Vector2(150, 15)), "Send Notification Event", SendNoification);
                break;
            case ProgressType.CG:
                rect.size = new Vector2(200, 200);
                Image = EditorGUI.ObjectField(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 50)), Image, typeof(Sprite), allowSceneObjects: true) as Sprite;
                //SendNoification = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 170), new Vector2(150, 15)), "Send Notification Event", SendNoification);
                break;
            case ProgressType.Wellbeing:
                TypeOfProgress = ProgressType.None;
                break;
            case ProgressType.Socials:
                TypeOfProgress = ProgressType.None;
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

    

}
