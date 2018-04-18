using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class ChainNode : BaseNode
{

    public List<ChoiceConnectionPoint> ChoicePoints;
    public List<int> ChoiceIDs;
    public List<string> EndTitle;

    public TextAsset Chain;

    JsonData data;
    public int EndingLength;

    GUIStyle OutPointStyle;

    Action<ConnectionPoint> ClickOutPoint;

    //a null node
    public ChainNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, int NodeID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        outPoint = null;

        ChoicePoints = new List<ChoiceConnectionPoint>();
        ChoiceIDs = new List<int>();
        ID = NodeID;

        TypeID = NodeTypes.MultiProgressNode;

        OutPointStyle = outPointStyle;
        ClickOutPoint = OnClickOutPoint;
    }


    public ChainNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        outPoint = null;
        TypeID = NodeTypes.MultiProgressNode;
        OutPointStyle = outPointStyle;
        ClickOutPoint = OnClickOutPoint;

        ChoicePoints = new List<ChoiceConnectionPoint>();
        ChoiceIDs = new List<int>();


        if (data.Keys.Contains("color"))
            ChangeColor((int)data["color"]);

        ID = (int)data["ID"];

        title = (string)data["title"];


        int NumOfChoices = data["Ends"].Count;

        Chain = Resources.Load("Progress/" + (string)data["Progress"]) as TextAsset;


        for (int i = 0; i < NumOfChoices; ++i)
        {
            ChoiceIDs.Add((int)data["Ends"][i]);
            ChoicePoints.Add(new ChoiceConnectionPoint(this, ConnectionPointType.Branch, OutPointStyle, ClickOutPoint));
        }

        ResizeChoices();
        
    }


    public override void Draw()
    {
        inPoint.Draw();
        for (int i = 0; i < ChoiceIDs.Count; ++i)
        {
            rect.size = new Vector2(200, 125 + (25 * i));
            float pos = 90 + (25 * i);
            pos = (pos / 2) - 5;
            ChoicePoints[i].Draw((int)pos);
        }
        //outPoint.Draw();
        GUI.Box(rect, "", style);
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 40), new Vector2(150, 20)), "ID: " + ID);
        Chain = EditorGUI.ObjectField(new Rect(rect.position + new Vector2(25, 60), new Vector2(150, 20)), Chain, typeof(TextAsset), allowSceneObjects: true) as TextAsset;

        ResizeChoices();

        for (int i = 0; i < ChoiceIDs.Count; ++i)
        {
            int pos = 90 + (25 * i);
            EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, pos), new Vector2(150, 20)), "End " + i + ": " + EndTitle[i]);
            rect.size = new Vector2(200, 125 + (25 * i));
        }
        if (EndingLength == 0)
        {
            rect.size = new Vector2(200, 100);
        }
    }


    void ResizeChoices()
    {

        if (Chain != null)
        {
            data = TextParser.ToJson(Chain);
            EndTitle = new List<string>();
            if (data != null)
            {
                EndingLength = 0;
                for (int i = 0; i < data.Count; ++i)
                {
                    if ((int)data[i]["TypeID"] == -1)
                    {
                        EndingLength += 1;

                        EndTitle.Add((string)data[i]["title"]);

                    }
                }
            }

        }
        else
        {
            EndingLength = 0;
        }


        while (ChoicePoints.Count > EndingLength)
        {
            ChoicePoints.RemoveAt(ChoicePoints.Count - 1);
        }
        while (ChoicePoints.Count < EndingLength)
        {
            ChoicePoints.Add(new ChoiceConnectionPoint(this, ConnectionPointType.Branch, OutPointStyle, ClickOutPoint));
        }

        while (ChoiceIDs.Count > EndingLength)
        {
            ChoiceIDs.RemoveAt(ChoiceIDs.Count - 1);
        }
        while (ChoiceIDs.Count < EndingLength)
        {
            ChoiceIDs.Add(-1);
        }
        

    }

    public void ConnectNode(ChoiceConnectionPoint connect, int ID)
    {

        for (int i = 0; i < ChoicePoints.Count; ++i)
        {
            if (ChoicePoints[i] == connect)
            {
                ChoiceIDs[i] = ID;
            }
        }


    }


}
