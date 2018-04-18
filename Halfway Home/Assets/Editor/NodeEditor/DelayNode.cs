using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class DelayNode : BaseNode
{
    
    public float TimeToDelay;

    public DelayNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, int MyID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        ID = MyID;
        TimeToDelay = 0;
        TypeID = NodeTypes.DelayNode;
        

    }

    public DelayNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {

        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];

        TimeToDelay = (float)(double)data["Delay"];
        
        TypeID = NodeTypes.DelayNode;
        
    }


    public override void Draw()
    {

        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(75, 20)), "Delay Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(75, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 65), new Vector2(75, 20)), "ID: " + ID);
        TimeToDelay = EditorGUI.FloatField(new Rect(rect.position + new Vector2(25, 85), new Vector2(75, 20)), TimeToDelay);
        

    }

    

}