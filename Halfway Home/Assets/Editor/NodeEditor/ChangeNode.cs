﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class ChangeNode : BaseNode
{

    public ProgressType TypeOfProgress;

    public ProgressPoint CheckPoint;

    public string InventoryItemName;
    
    public bool SendNoification;

    public string NoteTitle;

    public Sprite Image;

    public string TaskID;

    public Task.TaskState NewTaskState;

    public float Battery;
    public float Drain;
    
    public ChangeNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, int NodeID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        
        ID = NodeID;
        NextID = -1;
        TypeID = NodeTypes.ChangeNode;
        TypeOfProgress = ProgressType.None;
        CheckPoint = new ProgressPoint();
        
        NoteTitle = "";
        Image = null;
        TaskID = "0.0";
        NewTaskState = Task.TaskState.Unstarted;
    }


    public ChangeNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        
        TypeID = NodeTypes.ChangeNode;
        
        if (data.Keys.Contains("color"))
            ChangeColor((int)data["color"]);


        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];

        if(data.Keys.Contains("TypeOfProgress"))
        {
            int ty = (int)data["TypeOfProgress"];
            TypeOfProgress = (ProgressType)ty;
        }
        else
            TypeOfProgress = ProgressType.ProgressPoint;

        if (data.Keys.Contains("CheckToMatch"))
            CheckPoint = new ProgressPoint(data["CheckToMatch"][0]);
        else
            CheckPoint = new ProgressPoint();
        
        if (data.Keys.Contains("NoteTitle"))
            NoteTitle = (string)data["NoteTitle"];
        else
            NoteTitle = "";
        
        if (data.Keys.Contains("ImageSlug") && data["ImageSlug"] != null)
        {
            Image = Resources.Load<Sprite>("Sprites/" + (string)data["ImageSlug"]);

        }

        if (data.Keys.Contains("TaskNumber"))
            TaskID = (string)data["TaskNumber"];
        else
            TaskID = "0";

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

    


}
