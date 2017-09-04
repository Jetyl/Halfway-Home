﻿using UnityEngine;
using UnityEditor;
using System.Text;
using LitJson;
using System.IO;
using System;
using System.Collections.Generic;

public class ConversationEditor : BaseNodeEditor
{
    [SerializeField]
    public JsonData ConversationData;

    string ConversationName;

    int NewID = 10000;

    StartNode StartPoint;


    [MenuItem("Window/a-0/Conversation Editor")]
    private static void OpenWindow()
    {
        ConversationEditor window = GetWindow<ConversationEditor>();
        window.titleContent = new GUIContent("Conversation Editor");
    }


    private void OnGUI()
    {

        _zoomArea.height = position.height;
        _zoomArea.width = position.width;

        ZoomedArea = EditorZoomArea.Begin(zoomScale, _zoomArea);

        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);

        DrawConnections();
        DrawNodes();

        DrawConnectionLine(Event.current);

        ProcessNodeEvents(Event.current);

        EditorZoomArea.End();

        ProcessEvents(Event.current);

        ConversationName = EditorGUILayout.TextField("Conversation Name", ConversationName);

        //expands the json data with another dream slot
        if (GUILayout.Button("New Conversation"))
        {
            //clar all nodes
            if (nodes != null)
                nodes.Clear();
            if (connections != null)
                connections.Clear();

            ConversationData = new JsonData();
            ConversationName = "";
        }

        TextAsset LoadConversation = EditorGUILayout.ObjectField(Resources.Load("Assets/Resources/Conversations/" + ConversationName), typeof(TextAsset), allowSceneObjects: true) as TextAsset;

        if (LoadConversation != null)
        {

            var pro = TextParser.ToJson(LoadConversation);

            if (pro != ConversationData)
            {
                ConversationName = LoadConversation.name;
                LoadInfo(pro);

            }
        }

        //expands the json data with another dream slot
        if (GUILayout.Button("Save Conversation"))
        {
            if (ConversationName != "")
                SaveItemInfo();

        }



        if (GUI.changed) Repaint();
    }

    protected override void OnClickAddNode(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BaseNode>();
        }

        nodes.Add(new ProgressNode(mousePosition, 200, 100, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
        NewID += 1;
    }

    protected void OnClickAddNode(Vector2 mousePosition, NodeTypes type)
    {
        if (nodes == null)
        {
            nodes = new List<BaseNode>();
        }

        switch(type)
        {
            case NodeTypes.StartNode:
                StartPoint = new StartNode(mousePosition, 200, 100, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
                nodes.Add(StartPoint);
                return;
            case NodeTypes.EndingNode:
                nodes.Add(new EndingNode(mousePosition, 200, 120, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.ProgressNode:
                nodes.Add(new ProgressNode(mousePosition, 200, 100, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.ChangeNode:
                nodes.Add(new ChangeNode(mousePosition, 200, 120, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.LineNode:
                nodes.Add(new LineNode(mousePosition, 400, 120, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.ChoiceNode:
                nodes.Add(new ChoiceNode(mousePosition, 200, 120, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.DelayNode:
                nodes.Add(new DelayNode(mousePosition, 120, 120, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.EventNode:
                nodes.Add(new EventNode(mousePosition, 200, 160, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.AnimateNode:
                nodes.Add(new AnimateNode(mousePosition, 200, 160, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.ImageNode:
                nodes.Add(new ImageNode(mousePosition, 200, 160, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.SoundNode:
                nodes.Add(new SoundNode(mousePosition, 200, 160, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.ThoughtNode:
                nodes.Add(new ThoughtNode(mousePosition, 200, 160, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.InteruptNode:
                nodes.Add(new InteruptNode(mousePosition, 300, 160, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.CarryOnNode:
                nodes.Add(new CarryOnNode(mousePosition, 300, 160, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.FailNode:
                nodes.Add(new FailNode(mousePosition, 200, 100, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            case NodeTypes.MultiProgressNode:
                nodes.Add(new ChainNode(mousePosition, 200, 120, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, NewID));
                break;
            default:
                break;
        }

        NewID += 1;
    }
    
    protected override void OnClickRemoveNode(BaseNode node)
    {
        base.OnClickRemoveNode(node);

        if (node == StartPoint)
            StartPoint = null;
    }

    protected override void CreateConnection()
    {
        if (connections == null)
        {
            connections = new List<Connection>();
        }

        connections.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));

        if (selectedOutPoint.type == ConnectionPointType.Branch)
        {
            
            switch (selectedOutPoint.node.TypeID)
            {
                
                case NodeTypes.ProgressNode:
                    ((ProgressNode)selectedOutPoint.node).FailID = selectedInPoint.node.ID;
                    break;

                case NodeTypes.ChoiceNode:
                    ((ChoiceNode)selectedOutPoint.node).ConnectNode((ChoiceConnectionPoint)selectedOutPoint, selectedInPoint.node.ID);
                    break;
                case NodeTypes.InteruptNode:
                    ((InteruptNode)selectedOutPoint.node).ConnectNode((InteruptConnectionPoint)selectedOutPoint, selectedInPoint.node.ID);
                    break;
                case NodeTypes.FailNode:
                    ((FailNode)selectedOutPoint.node).FailID = selectedInPoint.node.ID;
                    break;
                case NodeTypes.MultiProgressNode:
                    ((ChainNode)selectedOutPoint.node).ConnectNode((ChoiceConnectionPoint)selectedOutPoint, selectedInPoint.node.ID);
                    break;
                default:
                    break;
            }

        }
        else
        {

            switch (selectedOutPoint.node.TypeID)
            {
                case NodeTypes.EndingNode:
                    //should never get here, but still
                    break;
                case NodeTypes.ProgressNode:
                    ((ProgressNode)selectedOutPoint.node).PassID = selectedInPoint.node.ID;
                    break;
                default:
                    selectedOutPoint.node.NextID = selectedInPoint.node.ID;
                    break;
            }
            
        }


    }

    protected override void OnClickRemoveConnection(Connection connection)
    {
        if (connection.outPoint.type == ConnectionPointType.Branch)
        {
            
            switch (connection.outPoint.node.TypeID)
            {
                case NodeTypes.ProgressNode:
                    ((ProgressNode)connection.outPoint.node).FailID = -1;
                    break;

                case NodeTypes.ChoiceNode:
                    if(selectedOutPoint != null)
                        ((ChoiceNode)selectedOutPoint.node).ConnectNode((ChoiceConnectionPoint)selectedOutPoint, -1);

                    break;
                case NodeTypes.InteruptNode:
                    if (selectedOutPoint != null)
                        ((InteruptNode)selectedOutPoint.node).ConnectNode((InteruptConnectionPoint)selectedOutPoint, -1);
                    break;
                case NodeTypes.FailNode:
                    ((FailNode)connection.outPoint.node).FailID = -1;
                    break;
                case NodeTypes.MultiProgressNode:
                    if (selectedOutPoint != null)
                        ((ChainNode)selectedOutPoint.node).ConnectNode((ChoiceConnectionPoint)selectedOutPoint, -1);

                    break;
                default:
                    break;
            }


        }
        else
        {

            switch (connection.outPoint.node.TypeID)
            {
                case NodeTypes.EndingNode:
                    //shound never get here, but still
                    break;
                case NodeTypes.ProgressNode:
                    ((ProgressNode)connection.outPoint.node).PassID = -1;
                    break;
                default:
                    connection.outPoint.node.NextID = -1;
                    break;
            }

            

        }

        connections.Remove(connection);
    }
    
    protected override void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        if (StartPoint == null)
        {
            genericMenu.AddItem(new GUIContent("Add Start node"), false, () => OnClickAddNode(mousePosition, NodeTypes.StartNode));
        }
        else
        {
            genericMenu.AddItem(new GUIContent("Add End node"), false, () => OnClickAddNode(mousePosition, NodeTypes.EndingNode));
            genericMenu.AddItem(new GUIContent("Add Line node"), false, () => OnClickAddNode(mousePosition, NodeTypes.LineNode));
            genericMenu.AddItem(new GUIContent("Add Choice node"), false, () => OnClickAddNode(mousePosition, NodeTypes.ChoiceNode));
            genericMenu.AddItem(new GUIContent("Add Delay node"), false, () => OnClickAddNode(mousePosition, NodeTypes.DelayNode));
            genericMenu.AddItem(new GUIContent("Action/Add Call node"), false, () => OnClickAddNode(mousePosition, NodeTypes.EventNode));
            genericMenu.AddItem(new GUIContent("Action/Add Animate node"), false, () => OnClickAddNode(mousePosition, NodeTypes.AnimateNode));
            genericMenu.AddItem(new GUIContent("Action/Add Image node"), false, () => OnClickAddNode(mousePosition, NodeTypes.ImageNode));
            genericMenu.AddItem(new GUIContent("Action/Add Sound node"), false, () => OnClickAddNode(mousePosition, NodeTypes.SoundNode));
            genericMenu.AddItem(new GUIContent("Action/Add Thought node"), false, () => OnClickAddNode(mousePosition, NodeTypes.ThoughtNode));
            genericMenu.AddItem(new GUIContent("Progress/Add Progress node"), false, () => OnClickAddNode(mousePosition));
            genericMenu.AddItem(new GUIContent("Progress/Add Change node"), false, () => OnClickAddNode(mousePosition, NodeTypes.ChangeNode));
            genericMenu.AddItem(new GUIContent("Progress/Add Multi-Progress node"), false, () => OnClickAddNode(mousePosition, NodeTypes.MultiProgressNode));
            genericMenu.AddItem(new GUIContent("Interuption/Add Interupt node"), false, () => OnClickAddNode(mousePosition, NodeTypes.InteruptNode));
            genericMenu.AddItem(new GUIContent("Interuption/Add Carry On Node"), false, () => OnClickAddNode(mousePosition, NodeTypes.CarryOnNode));
            genericMenu.AddItem(new GUIContent("Interuption/Add Interupt Fail Node"), false, () => OnClickAddNode(mousePosition, NodeTypes.FailNode));
        }

        genericMenu.ShowAsContext();
    }

    public void LoadInfo(JsonData pro)
    {



        if (nodes == null)
        {
            nodes = new List<BaseNode>();
        }
        else
        {
            nodes.Clear();
        }

        //reset the new ID, so we don't have jumps in ID numbers
        NewID = 10000;

        //load the asset
        ConversationData = pro;

        for (int i = 0; i < ConversationData.Count; ++i)
        {

            double x = (double)ConversationData[i]["NodePositionX"];
            double y = (double)ConversationData[i]["NodePositionY"];

            Vector2 pos = new Vector2((float)x, (float)y);

            double width = (double)ConversationData[i]["width"];
            double height = (double)ConversationData[i]["height"];

            int j = (int)ConversationData[i]["TypeID"];
            NodeTypes ID = (NodeTypes)j;

            switch (ID)
            {
                case NodeTypes.StartNode:
                    StartPoint = new StartNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, (int)ConversationData[i]["NextID"], (string)ConversationData[i]["title"], (bool)ConversationData[i]["Disable"]);
                    nodes.Add(StartPoint);
                    break;
                case NodeTypes.EndingNode:
                    nodes.Add(new EndingNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, (int)ConversationData[i]["ID"], (int)ConversationData[i]["EndID"], (string)ConversationData[i]["title"], (bool)ConversationData[i]["Enable"]));
                    break;
                case NodeTypes.LineNode:
                    nodes.Add(new LineNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.ProgressNode:
                    nodes.Add(new ProgressNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.ChangeNode:
                    nodes.Add(new ChangeNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.ChoiceNode:
                    nodes.Add(new ChoiceNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.DelayNode:
                    nodes.Add(new DelayNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.EventNode:
                    nodes.Add(new EventNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.AnimateNode:
                    nodes.Add(new AnimateNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.ImageNode:
                    nodes.Add(new ImageNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.SoundNode:
                    nodes.Add(new SoundNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.ThoughtNode:
                    nodes.Add(new ThoughtNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.InteruptNode:
                    nodes.Add(new InteruptNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.CarryOnNode:
                    nodes.Add(new CarryOnNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.FailNode:
                    nodes.Add(new FailNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                case NodeTypes.MultiProgressNode:
                    nodes.Add(new ChainNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, ConversationData[i]));
                    break;
                default:
                    break;
            }
            
            if (NewID < (int)ConversationData[i]["ID"])
                NewID = (int)ConversationData[i]["ID"];

        }
        NewID += 1;


        if (connections == null)
        {
            connections = new List<Connection>();
        }
        else
        {
            connections.Clear();
        }
        //making the connections
        for (int i = 0; i < nodes.Count; ++i)
        {

            switch (nodes[i].TypeID)
            {
                case NodeTypes.EndingNode:
                    //no out point
                    break;
                case NodeTypes.ProgressNode:
                    if (((ProgressNode)nodes[i]).PassID != -1)
                    {
                        for (int j = 0; j < nodes.Count; ++j)
                        {
                            if ((nodes[j]).ID == ((ProgressNode)nodes[i]).PassID)
                            {
                                connections.Add(new Connection(nodes[j].inPoint, nodes[i].outPoint, OnClickRemoveConnection));
                            }
                        }
                    }

                    if (((ProgressNode)nodes[i]).FailID != -1)
                    {
                        for (int k = 0; k < nodes.Count; ++k)
                        {
                            if ((nodes[k]).ID == ((ProgressNode)nodes[i]).FailID)
                            {
                                connections.Add(new Connection(nodes[k].inPoint, ((ProgressNode)nodes[i]).BranchOutPoint, OnClickRemoveConnection));
                            }
                        }
                    }
                    break;
                case NodeTypes.ChoiceNode:
                    for (int k = 0; k < ((ChoiceNode)nodes[i]).NumOfChoices; ++k)
                    {
                        for (int j = 0; j < nodes.Count; ++j)
                        {
                            if ((nodes[j]).ID == ((ChoiceNode)nodes[i]).ChoiceIDs[k])
                            {
                                connections.Add(new Connection(nodes[j].inPoint, ((ChoiceNode)nodes[i]).ChoicePoints[k], OnClickRemoveConnection));
                            }
                        }
                    }
                    
                    break;
                case NodeTypes.InteruptNode:
                    for (int k = 0; k < ((InteruptNode)nodes[i]).NumOfInterupts; ++k)
                    {
                        for (int j = 0; j < nodes.Count; ++j)
                        {

                            if (nodes[j].ID == ((InteruptNode)nodes[i]).Interupts[k].DestinationID)
                            {
                                connections.Add(new Connection(nodes[j].inPoint, ((InteruptNode)nodes[i]).InteruptPoints[k], OnClickRemoveConnection));
                            }
                        }
                    }

                    for (int k = 0; k < nodes.Count; ++k)
                    {
                        if ((nodes[k]).ID == (nodes[i]).NextID)
                        {
                            connections.Add(new Connection(nodes[k].inPoint, (nodes[i]).outPoint, OnClickRemoveConnection));
                        }
                    }

                    break;
                case NodeTypes.FailNode:
                    if (nodes[i].NextID != -1)
                    {
                        for (int j = 0; j < nodes.Count; ++j)
                        {
                            if ((nodes[j]).ID == nodes[i].NextID)
                            {
                                connections.Add(new Connection(nodes[j].inPoint, nodes[i].outPoint, OnClickRemoveConnection));
                            }
                        }
                    }

                    if (((FailNode)nodes[i]).FailID != -1)
                    {
                        for (int k = 0; k < nodes.Count; ++k)
                        {
                            if ((nodes[k]).ID == ((FailNode)nodes[i]).FailID)
                            {
                                connections.Add(new Connection(nodes[k].inPoint, ((FailNode)nodes[i]).BranchOutPoint, OnClickRemoveConnection));
                            }
                        }
                    }
                    break;
                case NodeTypes.MultiProgressNode:
                    for (int k = 0; k < ((ChainNode)nodes[i]).EndingLength; ++k)
                    {
                        for (int j = 0; j < nodes.Count; ++j)
                        {
                            if ((nodes[j]).ID == ((ChainNode)nodes[i]).ChoiceIDs[k])
                            {
                                connections.Add(new Connection(nodes[j].inPoint, ((ChainNode)nodes[i]).ChoicePoints[k], OnClickRemoveConnection));
                            }
                        }
                    }

                    break;
                default:
                    for (int k = 0; k < nodes.Count; ++k)
                    {
                        if ((nodes[k]).ID == (nodes[i]).NextID)
                        {
                            connections.Add(new Connection(nodes[k].inPoint, (nodes[i]).outPoint, OnClickRemoveConnection));
                            
                        }
                    }
                    break;
            }
            
        }
    }


    public void SaveItemInfo()
    {
        string path = null;

#if UNITY_EDITOR
        path = "Assets/Resources/Conversations/" + ConversationName + ".json";
#endif

        StringBuilder sb = new StringBuilder();
        JsonWriter Jwriter = new JsonWriter(sb);
        Jwriter.PrettyPrint = true;
        Jwriter.IndentValue = 1;

        Jwriter.WriteArrayStart();

        foreach (var node in nodes)
        {
            Jwriter.WriteObjectStart();
            Jwriter.WritePropertyName("title");
            Jwriter.Write((node).title);
            Jwriter.WritePropertyName("ID");
            Jwriter.Write((node).ID);
            Jwriter.WritePropertyName("TypeID");
            Jwriter.Write((int)(node).TypeID);
            Jwriter.WritePropertyName("NodePositionX");
            Jwriter.Write((node).rect.position.x);
            Jwriter.WritePropertyName("NodePositionY");
            Jwriter.Write((node).rect.position.y);
            Jwriter.WritePropertyName("width");
            Jwriter.Write((node).rect.width);
            Jwriter.WritePropertyName("height");
            Jwriter.Write((node).rect.height);

            switch(node.TypeID)
            {
                case NodeTypes.StartNode:
                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((StartNode)node).NextID);
                    Jwriter.WritePropertyName("Disable");
                    Jwriter.Write(((StartNode)node).DisableUI);
                    break;
                case NodeTypes.EndingNode:
                    Jwriter.WritePropertyName("EndID");
                    Jwriter.Write(((EndingNode)node).EndingID);
                    Jwriter.WritePropertyName("Enable");
                    Jwriter.Write(((EndingNode)node).EnableUI);
                    break;
                case NodeTypes.ProgressNode:
                    Jwriter.WritePropertyName("PassID");
                    Jwriter.Write(((ProgressNode)node).PassID);
                    Jwriter.WritePropertyName("FailID");
                    Jwriter.Write(((ProgressNode)node).FailID);
                    Jwriter.WritePropertyName("TypeOfProgress");
                    Jwriter.Write((int)((ProgressNode)node).TypeOfProgress);

                    switch (((ProgressNode)node).TypeOfProgress)
                    {
                        case ProgressType.None:

                            break;
                        case ProgressType.ProgressPoint:
                            Jwriter.WritePropertyName("CheckToMatch");

                            Jwriter.WriteArrayStart();
                            Jwriter.WriteObjectStart();
                            Jwriter.WritePropertyName("Name");
                            Jwriter.Write(((ProgressNode)node).CheckPoint.ProgressName);
                            Jwriter.WritePropertyName("Type");
                            Jwriter.Write((int)((ProgressNode)node).CheckPoint.TypeID);

                            switch (((ProgressNode)node).CheckPoint.TypeID)
                            {
                                case PointTypes.Flag:
                                    Jwriter.WritePropertyName("MatchValue");
                                    Jwriter.Write(((ProgressNode)node).CheckPoint.BoolValue);
                                    break;
                                case PointTypes.Float:
                                    Jwriter.WritePropertyName("MatchValue");
                                    Jwriter.Write(((ProgressNode)node).CheckPoint.FloatValue);
                                    Jwriter.WritePropertyName("Compare");
                                    Jwriter.Write((int)((ProgressNode)node).CheckPoint.compare);
                                    break;
                                case PointTypes.Integer:
                                    Jwriter.WritePropertyName("MatchValue");
                                    Jwriter.Write(((ProgressNode)node).CheckPoint.IntValue);
                                    Jwriter.WritePropertyName("Compare");
                                    Jwriter.Write((int)((ProgressNode)node).CheckPoint.compare);
                                    break;
                                case PointTypes.String:
                                    Jwriter.WritePropertyName("MatchValue");
                                    Jwriter.Write(((ProgressNode)node).CheckPoint.StringValue);
                                    break;
                                default:
                                    break;
                            }

                            Jwriter.WriteObjectEnd();
                            Jwriter.WriteArrayEnd();
                            break;
                        case ProgressType.Inventory:
                            Jwriter.WritePropertyName("Slug");
                            if (((ProgressNode)node).InventoryMatch != null)
                            {
                                string txt = AssetDatabase.GetAssetPath(((ProgressNode)node).InventoryMatch);
                                txt = txt.Replace("Assets/Resources/Sprites/", "");
                                //removes the file extention off the string
                                txt = txt.Remove(txt.Length - 4);
                                Jwriter.Write(txt);
                            }
                            else
                            {
                                Jwriter.Write(null);
                            }
                            Jwriter.WritePropertyName("Current");
                            Jwriter.Write(((ProgressNode)node).Current);

                            break;
                        case ProgressType.MoodAmount:
                            Jwriter.WritePropertyName("MoodToMatch");
                            Jwriter.Write((int)((ProgressNode)node).MoodToMatch);
                            Jwriter.WritePropertyName("MoodValue");
                            Jwriter.Write(((ProgressNode)node).MoodValueToMatch);
                            Jwriter.WritePropertyName("Comparison");
                            Jwriter.Write((int)((ProgressNode)node).Compare);
                            break;
                        case ProgressType.MoodPercent:
                            Jwriter.WritePropertyName("MoodToMatch");
                            Jwriter.Write((int)((ProgressNode)node).MoodToMatch);
                            Jwriter.WritePropertyName("MoodValue");
                            Jwriter.Write(((ProgressNode)node).MoodValueToMatch);
                            Jwriter.WritePropertyName("Comparison");
                            Jwriter.Write((int)((ProgressNode)node).Compare);
                            break;
                        case ProgressType.PrimaryMood:
                            Jwriter.WritePropertyName("MoodToMatch");
                            Jwriter.Write((int)((ProgressNode)node).MoodToMatch);
                            break;
                        case ProgressType.Lucky:
                            Jwriter.WritePropertyName("LuckRange");
                            Jwriter.Write(((ProgressNode)node).LuckRange);
                            Jwriter.WritePropertyName("PercentFailure");
                            Jwriter.Write(((ProgressNode)node).LuckPercentToFail);
                            Jwriter.WritePropertyName("AffectLuck");
                            Jwriter.Write(((ProgressNode)node).AffectLuck);
                            break;
                        case ProgressType.PhoneData:
                            Jwriter.WritePropertyName("PhoneDataType");
                            Jwriter.Write((int)((ProgressNode)node).PhoneData);
                            Jwriter.WritePropertyName("NoteTitle");
                            Jwriter.Write(((ProgressNode)node).NoteTitle);

                            Jwriter.WritePropertyName("PhoneImageSlug");
                            if (((ProgressNode)node).Image != null)
                            {
                                string txt = AssetDatabase.GetAssetPath(((ProgressNode)node).Image);
                                txt = txt.Replace("Assets/Resources/Sprites/", "");
                                //removes the file extention off the string
                                txt = txt.Remove(txt.Length - 4);
                                Jwriter.Write(txt);
                            }
                            else
                            {
                                Jwriter.Write(null);
                            }

                            Jwriter.WritePropertyName("TaskNumber");
                            Jwriter.Write(((ProgressNode)node).TaskNumber);
                            Jwriter.WritePropertyName("TaskState");
                            Jwriter.Write((int)((ProgressNode)node).NewTaskState);
                            Jwriter.WritePropertyName("Battery");
                            Jwriter.Write(((ProgressNode)node).Battery);
                            Jwriter.WritePropertyName("Drain");
                            Jwriter.Write(((ProgressNode)node).Drain);
                            break;
                        case ProgressType.PlotBeat:

                            Jwriter.WritePropertyName("Beat");
                            Jwriter.Write((int)((ProgressNode)node).BeatState);

                            Jwriter.WritePropertyName("BeatName");
                            Jwriter.Write(((ProgressNode)node).BeatName);

                            break;
                        case ProgressType.Scene:
                            Jwriter.WritePropertyName("Scene");
                            Jwriter.Write(((ProgressNode)node).SceneToCheck.SceneName);

                            Jwriter.WritePropertyName("Previous");
                            Jwriter.Write(((ProgressNode)node).PreviousScene);

                            break;
                        case ProgressType.Date:

                            Jwriter.WritePropertyName("Date");
                            Jwriter.Write((int)((ProgressNode)node).Date);
                            break;
                        case ProgressType.Dream:
                            Jwriter.WritePropertyName("Dream");
                            Jwriter.Write(((ProgressNode)node).DreamName);

                            break;
                        default:
                            Debug.LogError("Unrecognized Option");
                            break;
                    }


                    break;

                case NodeTypes.ChangeNode:
                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((ChangeNode)node).NextID);
                    Jwriter.WritePropertyName("TypeOfProgress");
                    Jwriter.Write((int)((ChangeNode)node).TypeOfProgress);

                    switch (((ChangeNode)node).TypeOfProgress)
                    {
                        case ProgressType.None:
                            break;
                        case ProgressType.ProgressPoint:
                            Jwriter.WritePropertyName("CheckToMatch");

                            Jwriter.WriteArrayStart();
                            Jwriter.WriteObjectStart();
                            Jwriter.WritePropertyName("Name");
                            Jwriter.Write(((ChangeNode)node).CheckPoint.ProgressName);
                            Jwriter.WritePropertyName("Type");
                            Jwriter.Write((int)((ChangeNode)node).CheckPoint.TypeID);

                            switch (((ChangeNode)node).CheckPoint.TypeID)
                            {
                                case PointTypes.Flag:
                                    Jwriter.WritePropertyName("MatchValue");
                                    Jwriter.Write(((ChangeNode)node).CheckPoint.BoolValue);
                                    break;
                                case PointTypes.Float:
                                    Jwriter.WritePropertyName("MatchValue");
                                    Jwriter.Write(((ChangeNode)node).CheckPoint.FloatValue);
                                    Jwriter.WritePropertyName("Compare");
                                    Jwriter.Write((int)((ChangeNode)node).CheckPoint.compare);
                                    break;
                                case PointTypes.Integer:
                                    Jwriter.WritePropertyName("MatchValue");
                                    Jwriter.Write(((ChangeNode)node).CheckPoint.IntValue);
                                    Jwriter.WritePropertyName("Compare");
                                    Jwriter.Write((int)((ChangeNode)node).CheckPoint.compare);
                                    break;
                                case PointTypes.String:
                                    Jwriter.WritePropertyName("MatchValue");
                                    Jwriter.Write(((ChangeNode)node).CheckPoint.StringValue);
                                    break;
                                default:
                                    break;
                            }

                            Jwriter.WriteObjectEnd();
                            Jwriter.WriteArrayEnd();
                            break;
                        case ProgressType.Inventory:
                            Jwriter.WritePropertyName("InventoryItem");
                            Jwriter.Write(((ChangeNode)node).InventoryItemName);
                            break;
                        case ProgressType.MoodAmount:
                            Jwriter.WritePropertyName("MoodToMatch");
                            Jwriter.Write((int)((ChangeNode)node).MoodToMatch);
                            Jwriter.WritePropertyName("MoodValue");
                            Jwriter.Write(((ChangeNode)node).MoodValueToMatch);
                            break;
                        case ProgressType.PhoneData:
                            Jwriter.WritePropertyName("PhoneDataType");
                            Jwriter.Write((int)((ChangeNode)node).PhoneData);
                            Jwriter.WritePropertyName("NoteTitle");
                            Jwriter.Write(((ChangeNode)node).NoteTitle);

                            Jwriter.WritePropertyName("PhoneImageSlug");
                            if (((ChangeNode)node).Image != null)
                            {
                                string txt = AssetDatabase.GetAssetPath(((ChangeNode)node).Image);
                                txt = txt.Replace("Assets/Resources/Sprites/", "");
                                //removes the file extention off the string
                                txt = txt.Remove(txt.Length - 4);
                                Jwriter.Write(txt);
                            }
                            else
                            {
                                Jwriter.Write(null);
                            }

                            Jwriter.WritePropertyName("TaskNumber");
                            Jwriter.Write(((ChangeNode)node).TaskNumber);
                            Jwriter.WritePropertyName("TaskState");
                            Jwriter.Write((int)((ChangeNode)node).NewTaskState);

                            Jwriter.WritePropertyName("Battery");
                            Jwriter.Write(((ChangeNode)node).Battery);
                            Jwriter.WritePropertyName("Drain");
                            Jwriter.Write(((ChangeNode)node).Drain);

                            Jwriter.WritePropertyName("Notify");
                            Jwriter.Write(((ChangeNode)node).SendNoification);
                            break;
                        default:
                            Debug.LogError("Unrecognized Option");
                            break;
                    }

                    break;
                case NodeTypes.LineNode:
                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((LineNode)node).NextID);

                    Jwriter.WritePropertyName("Lines");
                    Jwriter.WriteArrayStart();
                    for(int i = 0; i < ((LineNode)node).NumOfLines; ++i)
                    {
                        Jwriter.WriteObjectStart();

                        Jwriter.WritePropertyName("Speaker");
                        if (((LineNode)node).Lines[i].Speaker != null)
                            Jwriter.Write(((LineNode)node).Lines[i].Speaker);
                        else
                            Jwriter.Write(null);
                        Jwriter.WritePropertyName("Line");
                        Jwriter.Write(((LineNode)node).Lines[i].Dialog);
                        Jwriter.WritePropertyName("Pace");
                        Jwriter.Write(((LineNode)node).Lines[i].Pace);

                        Jwriter.WriteObjectEnd();
                    }
                    Jwriter.WriteArrayEnd();

                    Jwriter.WritePropertyName("ImmediateNext");
                    Jwriter.Write(((LineNode)node).CallNextNodeImmediately);
                    Jwriter.WritePropertyName("DenyInput");
                    Jwriter.Write(((LineNode)node).DenyPlayerInput);
                    break;
                case NodeTypes.ChoiceNode:
                    
                    Jwriter.WritePropertyName("Choices");
                    Jwriter.WriteArrayStart();
                    for (int i = 0; i < ((ChoiceNode)node).NumOfChoices; ++i)
                    {
                        Jwriter.Write(((ChoiceNode)node).ChoiceData[i]);
                    }
                    Jwriter.WriteArrayEnd();

                    Jwriter.WritePropertyName("Destinations");
                    Jwriter.WriteArrayStart();
                    for (int i = 0; i < ((ChoiceNode)node).NumOfChoices; ++i)
                    {
                        Jwriter.Write(((ChoiceNode)node).ChoiceIDs[i]);
                    }
                    Jwriter.WriteArrayEnd();

                    break;
                case NodeTypes.DelayNode:
                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((DelayNode)node).NextID);

                    Jwriter.WritePropertyName("Delay");
                    Jwriter.Write(((DelayNode)node).TimeToDelay);
                    break;
                case NodeTypes.EventNode:
                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((EventNode)node).NextID);

                    Jwriter.WritePropertyName("Event");
                    Jwriter.Write(((EventNode)node).eventdata);

                    Jwriter.WritePropertyName("Target");
                    Jwriter.Write(((EventNode)node).TargetID);
                    break;
                case NodeTypes.AnimateNode:
                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((AnimateNode)node).NextID);

                    Jwriter.WritePropertyName("key");
                    Jwriter.Write(((AnimateNode)node).animationKey);

                    Jwriter.WritePropertyName("Name");
                    Jwriter.Write(((AnimateNode)node).ObjectName);

                    Jwriter.WritePropertyName("bool");
                    Jwriter.Write(((AnimateNode)node).BoolState);
                    break;
                case NodeTypes.ImageNode:
                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((ImageNode)node).NextID);

                    Jwriter.WritePropertyName("Slug");
                    if (((ImageNode)node).ImageToDisplay != null)
                    {
                        string txt = AssetDatabase.GetAssetPath(((ImageNode)node).ImageToDisplay);
                        txt = txt.Replace("Assets/Resources/Sprites/", "");
                        //removes the file extention off the string
                        txt = txt.Remove(txt.Length - 4);
                        Jwriter.Write(txt);
                    }
                    else
                    {
                        Jwriter.Write(null);
                    }


                    Jwriter.WritePropertyName("bool");
                    Jwriter.Write(((ImageNode)node).BoolState);
                    break;
                case NodeTypes.SoundNode:
                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((SoundNode)node).NextID);

                    Jwriter.WritePropertyName("Slug");
                    if (((SoundNode)node).sfx != null)
                    {
                        string txt = AssetDatabase.GetAssetPath(((SoundNode)node).sfx);
                        txt = txt.Replace("Assets/Resources/Sounds/", "");
                        //removes the file extention off the string
                        txt = txt.Remove(txt.Length - 4);
                        Jwriter.Write(txt);
                    }
                    else
                    {
                        Jwriter.Write(null);
                    }

                    Jwriter.WritePropertyName("bool");
                    Jwriter.Write(((SoundNode)node).loop);
                    Jwriter.WritePropertyName("stop");
                    Jwriter.Write(((SoundNode)node).Remove);
                    Jwriter.WritePropertyName("music");
                    Jwriter.Write(((SoundNode)node).Music);
                    break;
                case NodeTypes.ThoughtNode:
                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((ThoughtNode)node).NextID);

                    Jwriter.WritePropertyName("IdeaID");
                    Jwriter.Write(((ThoughtNode)node).IdeaID);

                    Jwriter.WritePropertyName("Description");
                    Jwriter.Write(((ThoughtNode)node).Description);

                    Jwriter.WritePropertyName("FurtherDetails");
                    Jwriter.Write(((ThoughtNode)node).FurtherDetails);


                    Jwriter.WritePropertyName("PlacedManually");
                    Jwriter.Write(((ThoughtNode)node).PlacedManually);


                    Jwriter.WritePropertyName("Position");
                    Jwriter.WriteArrayStart();

                    //Jwriter.WritePropertyName("X");
                    Jwriter.Write(((ThoughtNode)node).Position.x);
                    //Jwriter.WritePropertyName("Y");
                    Jwriter.Write(((ThoughtNode)node).Position.y);
                    //Jwriter.WritePropertyName("Z");
                    Jwriter.Write(((ThoughtNode)node).Position.z);

                    Jwriter.WriteArrayEnd();


                    break;
                case NodeTypes.InteruptNode:

                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((InteruptNode)node).NextID);

                    Jwriter.WritePropertyName("Interupts");
                    Jwriter.WriteArrayStart();
                    for (int i = 0; i < ((InteruptNode)node).NumOfInterupts; ++i)
                    {
                        Jwriter.WriteObjectStart();

                        Jwriter.WritePropertyName("Idea");
                        Jwriter.Write(((InteruptNode)node).Interupts[i].idea);

                        Jwriter.WritePropertyName("Slug");
                        if (((InteruptNode)node).Interupts[i].item != null)
                        {
                            string txt = AssetDatabase.GetAssetPath(((InteruptNode)node).Interupts[i].item);
                            txt = txt.Replace("Assets/Resources/Sprites/", "");
                            //removes the file extention off the string
                            txt = txt.Remove(txt.Length - 4);
                            Jwriter.Write(txt);
                        }
                        else
                        {
                            Jwriter.Write(null);
                        }

                        Jwriter.WritePropertyName("Destinations");
                        Jwriter.Write(((InteruptNode)node).Interupts[i].DestinationID);

                        Jwriter.WritePropertyName("InteruptType");
                        Jwriter.Write((int)((InteruptNode)node).Interupts[i].type);

                        Jwriter.WriteObjectEnd();
                    }
                    Jwriter.WriteArrayEnd();

                    break;
                case NodeTypes.CarryOnNode:
                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((CarryOnNode)node).NextID);

                    Jwriter.WritePropertyName("Interupts");
                    Jwriter.WriteArrayStart();
                    for (int i = 0; i < ((CarryOnNode)node).NumOfInterupts; ++i)
                    {
                        Jwriter.WriteObjectStart();

                        Jwriter.WritePropertyName("Idea");
                        Jwriter.Write(((CarryOnNode)node).Interupts[i].idea);

                        Jwriter.WritePropertyName("Slug");
                        if (((CarryOnNode)node).Interupts[i].item != null)
                        {
                            string txt = AssetDatabase.GetAssetPath(((CarryOnNode)node).Interupts[i].item);
                            txt = txt.Replace("Assets/Resources/Sprites/", "");
                            //removes the file extention off the string
                            txt = txt.Remove(txt.Length - 4);
                            Jwriter.Write(txt);
                        }
                        else
                        {
                            Jwriter.Write(null);
                        }

                        Jwriter.WritePropertyName("Destinations");
                        Jwriter.Write(((CarryOnNode)node).Interupts[i].DestinationID);

                        Jwriter.WritePropertyName("InteruptType");
                        Jwriter.Write((int)((CarryOnNode)node).Interupts[i].type);

                        Jwriter.WriteObjectEnd();
                    }
                    Jwriter.WriteArrayEnd();

                    break;

                case NodeTypes.FailNode:

                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((FailNode)node).NextID);
                    Jwriter.WritePropertyName("FailID");
                    Jwriter.Write(((FailNode)node).FailID);
                    break;
                case NodeTypes.MultiProgressNode:
                    Jwriter.WritePropertyName("Progress");
                    if (((ChainNode)node).Chain != null)
                    {
                        string txt = AssetDatabase.GetAssetPath(((ChainNode)node).Chain);
                        txt = txt.Replace("Assets/Resources/Progress/", "");
                        //removes the file extention off the string
                        txt = txt.Remove(txt.Length - 5);
                        Jwriter.Write(txt);
                    }
                    else
                    {
                        Jwriter.Write(null);
                    }

                    Jwriter.WritePropertyName("Ends");
                    Jwriter.WriteArrayStart();
                    for (int i = 0; i < ((ChainNode)node).EndingLength; ++i)
                    {
                        Jwriter.Write(((ChainNode)node).ChoiceIDs[i]);
                    }
                    Jwriter.WriteArrayEnd();

                    break;
                default:
                    break;
            }


            
            



            Jwriter.WriteObjectEnd();
        }

        Jwriter.WriteArrayEnd();


        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(sb);
            }
        }

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }


}
