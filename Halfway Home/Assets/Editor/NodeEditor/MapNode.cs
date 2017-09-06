using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using LitJson;

public class MapNode : BaseNode
{

    public Room Locale;
    public int Day;
    public int Hour;
    public int Length;

    public MapNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int NodeID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        inPoint = null;
        ID = NodeID;
        NextID = -1;
        TypeID = NodeTypes.MapNode;
        
    }

    public MapNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        title = (string)data["title"];
        inPoint = null;
        ID = (int)data["ID"];
        NextID = (int)data["NextID"]; 
        TypeID = NodeTypes.MapNode;
    }


    public override void Draw()
    {

        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(300, 20)), "Map Choice Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 40), new Vector2(300, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 60), new Vector2(150, 20)), "ID: " + ID);

        Locale = (Room)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 80), new Vector2(300, 20)), new GUIContent("Room Location"), Locale);
        Day = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(25, 105), new Vector2(300, 20)), new GUIContent("Day of the Week"), Day, 1, 7);
        Hour = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(25, 130), new Vector2(300, 20)), new GUIContent("Hour of the Day"), Hour, 1, 24);
        Length = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(25, 155), new Vector2(300, 20)), new GUIContent("Length of time Availble"), Length, 1, 24);



    }


}
