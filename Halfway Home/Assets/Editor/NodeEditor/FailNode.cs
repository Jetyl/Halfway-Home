using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class FailNode : BaseNode
{

    public ConnectionPoint BranchOutPoint;
    public int FailID;

    public FailNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int MyID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        ID = MyID;
        TypeID = NodeTypes.FailNode;

        inPoint = new ProgressionConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ProgressionConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        BranchOutPoint = new ProgressionConnectionPoint(this, ConnectionPointType.Branch, outPointStyle, OnClickOutPoint);


    }

    public FailNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {

        ID = (int)data["ID"];

        NextID = (int)data["NextID"];
        FailID = (int)data["FailID"];

        title = (string)data["title"];


        inPoint = new ProgressionConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ProgressionConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        BranchOutPoint = new ProgressionConnectionPoint(this, ConnectionPointType.Branch, outPointStyle, OnClickOutPoint);


        TypeID = NodeTypes.FailNode;

    }


    public override void Draw()
    {

        inPoint.Draw();
        outPoint.Draw();
        BranchOutPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), "Interupt Fail Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(150, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 65), new Vector2(150, 20)), "ID: " + ID);
        
    }

}
