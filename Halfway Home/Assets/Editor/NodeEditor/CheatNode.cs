using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using LitJson;

public class CheatNode : BaseNode
{

    public string CheatCode;

    public CheatNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, int id) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        //inPoint = null;
        ID = id;
        NextID = -1;
        TypeID = NodeTypes.CheatNode;
        CheatCode = "";
        
    }

    public CheatNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];
        TypeID = NodeTypes.CheatNode;

        CheatCode = (string)data["Code"];

        if (data.Keys.Contains("color"))
            ChangeColor((int)data["color"]);

    }


    public override void Draw()
    {

        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), "Cheat Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 30), new Vector2(150, 20)), title);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 60), new Vector2(150, 20)), "Cheat Code");
        CheatCode = GUI.TextField(new Rect(rect.position + new Vector2(25, 75), new Vector2(150, 20)), CheatCode);
    }


}
