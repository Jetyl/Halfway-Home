using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class ImageNode : BaseNode
{
    
    public Sprite ImageToDisplay;
    public bool BoolState;


    public ImageNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int MyID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        ID = MyID;
        TypeID = NodeTypes.ImageNode;
    }

    public ImageNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {

        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];
        

        if (data["Slug"] != null)
        {
            ImageToDisplay = Resources.Load("Sprites/" + (string)data["Slug"]) as Sprite;
        }
        BoolState = (bool)data["bool"];

        TypeID = NodeTypes.ImageNode;

    }


    public override void Draw()
    {

        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), "Image Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(150, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 65), new Vector2(150, 20)), "ID: " + ID);

        BoolState = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 85), new Vector2(150, 20)), "Click To Remove", BoolState);

        ImageToDisplay = EditorGUI.ObjectField(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 100)), ImageToDisplay, typeof(Sprite), allowSceneObjects: true) as Sprite;



    }

}
