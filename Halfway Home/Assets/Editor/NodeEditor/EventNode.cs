using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class EventNode : BaseNode
{
    
    public Events eventdata;

    public GameObject target;

    public string TargetID;


    public EventNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int MyID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        ID = MyID;
        TypeID = NodeTypes.EventNode;
        eventdata = "";

    }

    public EventNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {

        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];
        
        eventdata = (string)data["Event"];

        if(data["Target"] != null)
        {
            TargetID = (string)data["Target"];

            target = GameObject.Find(TargetID);
        }


        TypeID = NodeTypes.EventNode;

    }


    public override void Draw()
    {

        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), "Call Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(150, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 65), new Vector2(150, 20)), "ID: " + ID);

        eventdata = GUI.TextField(new Rect(rect.position + new Vector2(25, 85), new Vector2(150, 20)), eventdata);
        target = EditorGUI.ObjectField(new Rect(rect.position + new Vector2(25, 110), new Vector2(150, 15)), target, typeof(GameObject), true) as GameObject;
        //DrawEvent();
        if(target != null)
            TargetID = GetRootName(target);

        GUI.Label(new Rect(rect.position + new Vector2(25, 130), new Vector2(150, 20)), TargetID);

    }



    string GetRootName(GameObject object_)
    {

        string path = "/" + object_.name;
        while (object_.transform.parent != null)
        {
            object_ = object_.transform.parent.gameObject;
            path = "/" + object_.name + path;
        }
        return path;

    }

}

