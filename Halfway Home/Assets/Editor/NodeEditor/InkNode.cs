using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class InkNode : BaseNode
{

    public TextAsset InkFile;

    public InkNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int NodeID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {

        
        ID = NodeID;
        NextID = -1;
        TypeID = NodeTypes.InkNode;

        
    }


    public InkNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {

        TypeID = NodeTypes.InkNode;

        int ty = (int)data["TypeOfProgress"];
        
        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];

        



    }


    public override void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), "Ink Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(150, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 60), new Vector2(150, 20)), "ID: " + ID);
        InkFile = EditorGUI.ObjectField(new Rect(rect.position + new Vector2(25, 85), new Vector2(150, 17)), InkFile, typeof(TextAsset), allowSceneObjects: true) as TextAsset;

        
    }


    




}
