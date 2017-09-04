using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class AnimateNode : BaseNode
{
    
    public GameObject Obj;
    public string ObjectName;
    public string animationKey;
    public bool BoolState;


    public AnimateNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int MyID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        ID = MyID;
        TypeID = NodeTypes.AnimateNode;
        ObjectName = "";
        animationKey = "";
    }

    public AnimateNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {

        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];

        
        if (data["Name"] != null)
        {
            ObjectName = (string)data["Name"];


            Obj = GameObject.Find(ObjectName);
        }

        animationKey = (string)data["key"];


        BoolState = (bool)data["bool"];

        TypeID = NodeTypes.AnimateNode;

    }


    public override void Draw()
    {

        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), "Animate Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(150, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 65), new Vector2(150, 20)), "ID: " + ID);

        animationKey = GUI.TextField(new Rect(rect.position + new Vector2(25, 85), new Vector2(80, 20)), animationKey);
        BoolState = EditorGUI.Toggle(new Rect(rect.position + new Vector2(25 + 85, 85), new Vector2(80, 20)), BoolState);

        Obj = EditorGUI.ObjectField(new Rect(rect.position + new Vector2(25, 110), new Vector2(150, 15)), Obj, typeof(GameObject), true) as GameObject;
        //DrawEvent();
        if (Obj != null)
            ObjectName = GetRootName(Obj);

        GUI.Label(new Rect(rect.position + new Vector2(25, 130), new Vector2(150, 20)), ObjectName);

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

