using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class ChangeNode : BaseNode
{

    public ProgressPoint CheckPoint;

    public string InventoryItemName;

    public ProgressType TypeOfProgress;

    public Feelings MoodToMatch;

    public float MoodValueToMatch;

    public PhoneDataTypes PhoneData;
    public bool SendNoification;

    public string NoteTitle;

    public Sprite Image;

    public int TaskNumber;

    public Task.TaskState NewTaskState;

    public float Battery;
    public float Drain;
    
    public ChangeNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int NodeID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        
        TypeOfProgress = ProgressType.None;
        ID = NodeID;
        NextID = -1;
        TypeID = NodeTypes.ChangeNode;
        CheckPoint = new ProgressPoint();
        MoodToMatch = Feelings.Hollow;
        MoodValueToMatch = 0;
        InventoryItemName = "";
        PhoneData = PhoneDataTypes.None;
        NoteTitle = "";
        Image = null;
        TaskNumber = 0;
        NewTaskState = Task.TaskState.Unstarted;
    }


    public ChangeNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        
        TypeID = NodeTypes.ChangeNode;

        int ty = (int)data["TypeOfProgress"];
        TypeOfProgress = (ProgressType)ty;

        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];

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



        if (data.Keys.Contains("PhoneImageSlug") && data["PhoneImageSlug"] != null)
        {
            Image = Resources.Load<Sprite>("Sprites/" + (string)data["PhoneImageSlug"]);

        }

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
            Battery = (float)(double)data["Battery"];
        else
            Battery = 0;

        if (data.Keys.Contains("Drain"))
            Drain = (float)(double)data["Drain"];
        else
            Drain = 0;




        if (data.Keys.Contains("InventoryItem"))
            InventoryItemName = (string)data["InventoryItem"];
        else
            InventoryItemName = "";




        if (data.Keys.Contains("Notify"))
            SendNoification = (bool)data["Notify"];
        else
            SendNoification = false;



        

        
    }


    public override void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
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
                rect.size = new Vector2(200, 125);
                InventoryItemName = EditorGUI.TextField(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 20)), InventoryItemName);
                break;
            case ProgressType.MoodAmount:
                rect.size = new Vector2(200, 155);
                MoodToMatch = (Feelings)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 20)), MoodToMatch);
                MoodValueToMatch = EditorGUI.FloatField(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 20)), MoodValueToMatch);
                break;
            case ProgressType.MoodPercent:
                TypeOfProgress = ProgressType.MoodAmount;
                break;
            case ProgressType.PrimaryMood:
                TypeOfProgress = ProgressType.MoodAmount;
                break;
            case ProgressType.Lucky:
                TypeOfProgress = ProgressType.None;
                break;
            case ProgressType.PhoneData:
                rect.size = new Vector2(200, 125);
                PhoneData = (PhoneDataTypes)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 20)), PhoneData);
                PhoneDataDisplay();
                break;
            case ProgressType.PlotBeat:
                TypeOfProgress = ProgressType.None;
                break;
            case ProgressType.Scene:
                TypeOfProgress = ProgressType.None;
                break;
            case ProgressType.Date:
                TypeOfProgress = ProgressType.None;
                break;
            case ProgressType.Dream:
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
                rect.size = new Vector2(200, 175);
                CheckPoint.FloatValue = EditorGUI.FloatField(new Rect(rect.position + new Vector2(25, 135), new Vector2(150, 20)), CheckPoint.FloatValue);
                
                break;
            case PointTypes.Integer:
                rect.size = new Vector2(200, 175);
                CheckPoint.IntValue = EditorGUI.IntField(new Rect(rect.position + new Vector2(25, 135), new Vector2(150, 20)), CheckPoint.IntValue);
                
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
                rect.size = new Vector2(200, 170);
                NoteTitle = EditorGUI.TextField(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 20)), NoteTitle);
                SendNoification = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 140), new Vector2(150, 15)), "Send Notification Event", SendNoification);
                break;
            case PhoneDataTypes.Pic:
                rect.size = new Vector2(200, 200);
                Image = EditorGUI.ObjectField(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 50)), Image, typeof(Sprite), allowSceneObjects: true) as Sprite;
                SendNoification = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 170), new Vector2(150, 15)), "Send Notification Event", SendNoification);
                break;
            case PhoneDataTypes.Task:
                rect.size = new Vector2(200, 190);
                TaskNumber = EditorGUI.IntField(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 20)), TaskNumber);
                NewTaskState = (Task.TaskState)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 140), new Vector2(150, 20)), NewTaskState);
                SendNoification = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 160), new Vector2(150, 15)), "Send Notification Event", SendNoification);
                break;
            case PhoneDataTypes.Battery:
                rect.size = new Vector2(200, 170);
                Battery = EditorGUI.Slider(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 20)), Battery, -1, 1);
                SendNoification = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 160), new Vector2(150, 15)), "Send Notification Event", SendNoification);
                break;
            case PhoneDataTypes.Drain:
                rect.size = new Vector2(200, 170);
                Drain = EditorGUI.FloatField(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 20)), Drain);
                SendNoification = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 160), new Vector2(150, 15)), "Send Notification Event", SendNoification);
                break;
            default:
                Debug.LogError("Unrecognized Option");
                break;
        }
    }


}
