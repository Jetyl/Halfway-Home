using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class EndingNode : BaseNode
{

    public int EndingID;
    public bool EnableUI;

    public EndingNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int MyID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        outPoint = null;
        ID = MyID;
        EndingID = 0;
        TypeID = NodeTypes.EndingNode;
        EnableUI = false;
    }

    public EndingNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int MyID, int endID, string Title, bool Enable, int color) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        title = Title;
        outPoint = null;
        ID = MyID;
        EndingID = endID;
        TypeID = NodeTypes.EndingNode;
        EnableUI = Enable;
    }


    public override void Draw()
    {
        
        inPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), "End Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(150, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 55), new Vector2(150, 20)), "ID: " + ID);
        EndingID = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(25, 70), new Vector2(150, 20)), EndingID, 0, 17);
        EnableUI = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 90), new Vector2(150, 20)), "Enable UI", EnableUI);

    }



}
