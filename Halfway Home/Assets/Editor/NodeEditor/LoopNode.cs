using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class LoopNode : BaseNode
{

    public int Day;
    public int Hour;

    public LoopNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int NodeID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {


        ID = NodeID;
        NextID = -1;
        TypeID = NodeTypes.LoopNode;


    }


    public LoopNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {

        TypeID = NodeTypes.LoopNode;
        
        if (data.Keys.Contains("color"))
            ChangeColor((int)data["color"]);

        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];

        Day = (int)data["Day"];
        Hour = (int)data["Hour"];
    }


    public override void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), "Recover Restart Required Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(275, 20)), title);
        Day = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(25, 60), new Vector2(275, 20)), new GUIContent("Day of the Week"), Day, 0, 7);
        Hour = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(25, 85), new Vector2(275, 20)), new GUIContent("Hour of the Day"), Hour, 0, 23);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 110), new Vector2(275, 20)), "Setting Time to " + GetTime(Hour) + " On Day " + Day);

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
