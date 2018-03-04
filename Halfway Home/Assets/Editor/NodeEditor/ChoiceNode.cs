using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class ChoiceNode : BaseNode
{
    
    public List<ChoiceConnectionPoint> ChoicePoints;
    public List<int> ChoiceIDs;
    public List<string> ChoiceData;

    public int NumOfChoices;

    GUIStyle OutPointStyle;

    Action<ConnectionPoint> ClickOutPoint;

    //a null node
    public ChoiceNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, int NodeID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        outPoint = null;

        ChoicePoints = new List<ChoiceConnectionPoint>();
        ChoiceIDs = new List<int>();
        ChoiceData = new List<string>();
        ID = NodeID;

        TypeID = NodeTypes.ChoiceNode;

        OutPointStyle = outPointStyle;
        ClickOutPoint = OnClickOutPoint;
    }


    public ChoiceNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        outPoint = null;
        TypeID = NodeTypes.ChoiceNode;
        OutPointStyle = outPointStyle;
        ClickOutPoint = OnClickOutPoint;

        ChoicePoints = new List<ChoiceConnectionPoint>();
        ChoiceIDs = new List<int>();
        ChoiceData = new List<string>();

        ID = (int)data["ID"];
        
        title = (string)data["title"];


        NumOfChoices = data["Choices"].Count;
        

        for (int i = 0; i < NumOfChoices; ++i)
        {
            ChoiceData.Add((string)data["Choices"][i]);
            ChoiceIDs.Add((int)data["Destinations"][i]);
            ChoicePoints.Add(new ChoiceConnectionPoint(this, ConnectionPointType.Branch, OutPointStyle, ClickOutPoint));
        }



    }


    public override void Draw()
    {
        inPoint.Draw();
        for (int i = 0; i < NumOfChoices; ++i)
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
        NumOfChoices = EditorGUI.DelayedIntField(new Rect(rect.position + new Vector2(25, 65), new Vector2(50, 20)), NumOfChoices);

        ResizeChoices();

        for(int i = 0; i < NumOfChoices; ++ i)
        {
            int pos = 90 + (25 * i);
            ChoiceData[i] = GUI.TextField(new Rect(rect.position + new Vector2(25, pos), new Vector2(150, 20)), ChoiceData[i]);
            rect.size = new Vector2(200, 125 + (25 * i));
            //ChoicePoints[i].Draw(((pos/2) - 5));
        }
        if (NumOfChoices == 0)
        {
            rect.size = new Vector2(200, 100);
        }
    }


    void ResizeChoices()
    {

        while (ChoicePoints.Count > NumOfChoices)
        {
            ChoicePoints.RemoveAt(ChoicePoints.Count - 1);
        }
        while (ChoicePoints.Count < NumOfChoices)
        {
            ChoicePoints.Add(new ChoiceConnectionPoint(this, ConnectionPointType.Branch, OutPointStyle, ClickOutPoint));
        }

        while (ChoiceIDs.Count > NumOfChoices)
        {
            ChoiceIDs.RemoveAt(ChoiceIDs.Count - 1);
        }
        while (ChoiceIDs.Count < NumOfChoices)
        {
            ChoiceIDs.Add(-1);
        }

        while (ChoiceData.Count > NumOfChoices)
        {
            ChoiceData.RemoveAt(ChoiceIDs.Count - 1);
        }
        while (ChoiceData.Count < NumOfChoices)
        {
            ChoiceData.Add("");
        }


    }

    public void ConnectNode(ChoiceConnectionPoint connect, int ID)
    {

        for (int i = 0; i < ChoicePoints.Count; ++i)
        {
            if(ChoicePoints[i] == connect)
            {
                ChoiceIDs[i] = ID;
            }
        }


    }


}
