using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class ThoughtNode : BaseNode
{

    public string IdeaID;

    public string Description;

    public string FurtherDetails;
    
    public Vector3 Position;

    public bool PlacedManually;

    public ThoughtNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int MyID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        ID = MyID;
        TypeID = NodeTypes.ThoughtNode;
        Position = new Vector3();
    }

    public ThoughtNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {

        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];

        TypeID = NodeTypes.ThoughtNode;

        IdeaID = (string)data["IdeaID"];
        if(data["Description"] != null)
            Description = (string)data["Description"];
        if(data["FurtherDetails"] != null)
            FurtherDetails = (string)data["FurtherDetails"];


        PlacedManually = (bool)data["PlacedManually"];

        Position = new Vector3();
        Position.x = (float)(double)data["Position"][0];
        Position.y = (float)(double)data["Position"][1];
        Position.z = (float)(double)data["Position"][2];


    }


    public override void Draw()
    {

        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, EditorGUIUtility.singleLineHeight)), "Thought Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(150, EditorGUIUtility.singleLineHeight)), title);

        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 60), new Vector2(40, EditorGUIUtility.singleLineHeight)), "Idea");
        IdeaID = EditorGUI.TextField(new Rect(rect.position + new Vector2(25 + 40, 60), new Vector2(110, EditorGUIUtility.singleLineHeight)), IdeaID);

        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 85), new Vector2(40, EditorGUIUtility.singleLineHeight)), "Desc.");
        Description = EditorGUI.TextField(new Rect(rect.position + new Vector2(25 + 40, 85), new Vector2(110, EditorGUIUtility.singleLineHeight)), Description);

        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 110), new Vector2(40, EditorGUIUtility.singleLineHeight)), "Detail");
        FurtherDetails = EditorGUI.TextField(new Rect(rect.position + new Vector2(25 + 40, 110), new Vector2(110, EditorGUIUtility.singleLineHeight)), FurtherDetails);

        PlacedManually = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 130), new Vector2(150, 20)), new GUIContent("Place Manually"), PlacedManually);

        if(PlacedManually)
        {
            rect.size = new Vector2(200, 200);
            Position = EditorGUI.Vector3Field(new Rect(rect.position + new Vector2(25, 150), new Vector2(150, 20)), new GUIContent("Screen Position"), Position);
        }
        else
        {
            rect.size = new Vector2(200, 160);
        }

    }

}

