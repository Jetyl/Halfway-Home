using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class SceneStampNode : BaseNode
{
    public string Tag;
    public int Day;
    public int Hour;
    public int Length;

    public SceneStampNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, int NodeID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        
        ID = NodeID;
        NextID = -1;
        TypeID = NodeTypes.StampNode;


    }


    public SceneStampNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {

        TypeID = NodeTypes.StampNode;

        if (data.Keys.Contains("color"))
            ChangeColor((int)data["color"]);

        ID = (int)data["ID"];
        
        
        if (data.Keys.Contains("NextID"))
            NextID = (int)data["NextID"];

        title = (string)data["title"];

        Day = (int)data["Day"];
        Hour = (int)data["Hour"];
        Length = (int)data["Length"];

        Tag = (string)data["Tag"];

    }
    public override BaseNode Duplicate(int index)
    {

        Vector2 pos = new Vector2(rect.x + 25, rect.y + 25);
        
        SceneStampNode copy = new SceneStampNode(pos, rect.width, rect.height, style, selectedNodeStyle, inPoint.style, outPoint.style,
            inPoint.OnClickConnectionPoint, outPoint.OnClickConnectionPoint, OnRemoveNode, OnDuplicateNode, index);
        
        copy.Hour = Hour;
        copy.Day = Day;
        copy.Length = Length;
        copy.Tag = Tag;
        
        copy.ChangeColor(NodeColor);
        copy.ID = index;

        return copy;
    }

    public override void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), "Scene Time Lock Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(275, 20)), title);
        Tag = EditorGUI.TextField(new Rect(rect.position + new Vector2(25, 60), new Vector2(275, 20)), new GUIContent("Scene Tag"), Tag);

        Day = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(25, 85), new Vector2(275, 20)), new GUIContent("Day of the Week"), Day, 0, 7);
        Hour = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(25, 105), new Vector2(275, 20)), new GUIContent("Hour of the Day"), Hour, 0, 23);
        Length = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(25, 125), new Vector2(275, 20)), new GUIContent("Length of time Availble"), Length, 1, 24);

        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 145), new Vector2(275, 20)), "Set Scene from " + GetTime(Hour) + " to " + GetTime(Hour + Length));
        
    }


    string GetTime(int time)
    {
        string Txt = time + ":00";

        if (time > 24)
            time -= 24;


        if (time < 12)
        {
            if (time == 0)
                Txt = "12:00 AM";
            else
                Txt = time + ":00 AM";

        }
        else
            Txt = (time - 12) + ":00 PM";

        return Txt;

    }





}
