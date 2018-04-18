using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class RoomNode : BaseNode
{

    public Room Locale;
    public TransitionTypes transition;

    public RoomNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, int NodeID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        
        ID = NodeID;
        NextID = -1;
        TypeID = NodeTypes.RoomNode;


    }


    public RoomNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {

        TypeID = NodeTypes.RoomNode;


        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];


        if (data.Keys.Contains("color"))
            ChangeColor((int)data["color"]);
        
        Locale = (Room)(int)data["Room"];
        transition = (TransitionTypes)(int)data["Transition"];



    }


    public override void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(250, 20)), "Set Room Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(250, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 60), new Vector2(250, 20)), "ID: " + ID);

        Locale = (Room)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 85), new Vector2(250, 20)), new GUIContent("Room Location"), Locale);

        transition = (TransitionTypes)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 105), new Vector2(250, 20)), new GUIContent("Screen Strantion"), transition);

        //InkFile = EditorGUI.ObjectField(new Rect(rect.position + new Vector2(25, 85), new Vector2(150, 17)), InkFile, typeof(TextAsset), allowSceneObjects: true) as TextAsset;


    }







}
