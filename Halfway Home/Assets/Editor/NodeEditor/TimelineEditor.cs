using UnityEngine;
using UnityEditor;
using System.Text;
using LitJson;
using System.IO;
using System;
using System.Collections.Generic;

public class TimelineEditor : BaseNodeEditor
{
    [SerializeField]
    public JsonData ConversationData;

    string ConversationName;

    int NewID = 10000;

    StartNode StartPoint;


    [MenuItem("Window/Halfway Home/Timeline Editor")]
    private static void OpenWindow()
    {
        TimelineEditor window = GetWindow<TimelineEditor>();
        window.titleContent = new GUIContent("Timeline Editor");
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

        ConversationName = EditorGUILayout.TextField("Timeline Name", ConversationName);

        //expands the json data with another dream slot
        if (GUILayout.Button("New Timeline"))
        {
            //clar all nodes
            if (nodes != null)
                nodes.Clear();
            if (connections != null)
                connections.Clear();

            ConversationData = new JsonData();
            ConversationName = "";
        }

        TextAsset LoadConversation = EditorGUILayout.ObjectField(Resources.Load("Assets/Resources/Timeline/" + ConversationName), typeof(TextAsset), allowSceneObjects: true) as TextAsset;

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
        if (GUILayout.Button("Save Timeline"))
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

        nodes.Add(new ProgressNode(mousePosition, 200, 100, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
        NewID += 1;
    }

    protected void OnClickAddNode(Vector2 mousePosition, NodeTypes type)
    {
        if (nodes == null)
        {
            nodes = new List<BaseNode>();
        }

        switch (type)
        {
            case NodeTypes.StartNode:
                StartPoint = new StartNode(mousePosition, 200, 100, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode);
                nodes.Add(StartPoint);
                return;
            case NodeTypes.EndingNode:
                nodes.Add(new EndingNode(mousePosition, 200, 120, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode,  NewID));
                break;
            case NodeTypes.ProgressNode:
                nodes.Add(new ProgressNode(mousePosition, 200, 100, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
                break;
            case NodeTypes.ChangeNode:
                nodes.Add(new ChangeNode(mousePosition, 200, 120, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
                break;
            case NodeTypes.MultiProgressNode:
                nodes.Add(new ChainNode(mousePosition, 200, 120, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
                break;
            case NodeTypes.MapNode:
                nodes.Add(new MapNode(mousePosition, 350, 200, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
                break;
            case NodeTypes.ToMapNode:
                nodes.Add(new ToMapNode(mousePosition, 200, 100, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
                break;
            case NodeTypes.InkNode:
                nodes.Add(new InkNode(mousePosition, 200, 120, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
                break;
            case NodeTypes.LoadNode:
                nodes.Add(new LoadNode(mousePosition, 200, 100, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
                break;
            case NodeTypes.LoopNode:
                nodes.Add(new LoopNode(mousePosition, 325, 150, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
                break;
            case NodeTypes.CheatNode:
                nodes.Add(new CheatNode(mousePosition, 200, 120, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
                break;
            case NodeTypes.RoomNode:
                nodes.Add(new RoomNode(mousePosition, 300, 150, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
                break;
            case NodeTypes.SoundNode:
                nodes.Add(new SoundNode(mousePosition, 300, 150, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
                break;
            case NodeTypes.StampNode:
                nodes.Add(new SceneStampNode(mousePosition, 325, 185, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, NewID));
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
            genericMenu.AddItem(new GUIContent("Add Load node"), false, () => OnClickAddNode(mousePosition, NodeTypes.LoadNode));
            genericMenu.AddItem(new GUIContent("Add Loop node"), false, () => OnClickAddNode(mousePosition, NodeTypes.LoopNode));
            
            genericMenu.AddItem(new GUIContent("Map/Add Map node"), false, () => OnClickAddNode(mousePosition, NodeTypes.MapNode));
            genericMenu.AddItem(new GUIContent("Map/Add Return node"), false, () => OnClickAddNode(mousePosition, NodeTypes.ToMapNode));
            genericMenu.AddItem(new GUIContent("Map/Add Scene Lock node"), false, () => OnClickAddNode(mousePosition, NodeTypes.StampNode));

            genericMenu.AddItem(new GUIContent("Display/Add Set Room node"), false, () => OnClickAddNode(mousePosition, NodeTypes.RoomNode));
            genericMenu.AddItem(new GUIContent("Display/Add Ink node"), false, () => OnClickAddNode(mousePosition, NodeTypes.InkNode));
            genericMenu.AddItem(new GUIContent("Display/Add Sound node"), false, () => OnClickAddNode(mousePosition, NodeTypes.SoundNode));
            
            genericMenu.AddItem(new GUIContent("Progress/Add Progress node"), false, () => OnClickAddNode(mousePosition));
            genericMenu.AddItem(new GUIContent("Progress/Add Change node"), false, () => OnClickAddNode(mousePosition, NodeTypes.ChangeNode));
            genericMenu.AddItem(new GUIContent("Progress/Add Multi-Progress node"), false, () => OnClickAddNode(mousePosition, NodeTypes.MultiProgressNode));


            genericMenu.AddItem(new GUIContent("Add Cheat node"), false, () => OnClickAddNode(mousePosition, NodeTypes.CheatNode));


        }

        genericMenu.ShowAsContext();
    }


    protected override void OnClickDuplicateNode<T>(T node)
    {
        nodes.Add(node.Duplicate(NewID));
        NewID += 1;
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

            int col = (int)ConversationData[i]["color"];

            int j = (int)ConversationData[i]["TypeID"];
            NodeTypes ID = (NodeTypes)j;

            switch (ID)
            {
                case NodeTypes.StartNode:
                    StartPoint = new StartNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode,(int)ConversationData[i]["NextID"], (string)ConversationData[i]["title"], (bool)ConversationData[i]["Disable"], col);
                    nodes.Add(StartPoint);
                    break;
                case NodeTypes.EndingNode:
                    nodes.Add(new EndingNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode,(int)ConversationData[i]["ID"], (int)ConversationData[i]["EndID"], (string)ConversationData[i]["title"], (bool)ConversationData[i]["Enable"], col));
                    break;
                case NodeTypes.ProgressNode:
                    nodes.Add(new ProgressNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, ConversationData[i]));
                    break;
                case NodeTypes.ChangeNode:
                    nodes.Add(new ChangeNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, ConversationData[i]));
                    break;
                case NodeTypes.MultiProgressNode:
                    nodes.Add(new ChainNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, ConversationData[i]));
                    break;
                case NodeTypes.MapNode:
                    nodes.Add(new MapNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, ConversationData[i]));
                    break;
                case NodeTypes.ToMapNode:
                    nodes.Add(new ToMapNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, ConversationData[i]));
                    break;
                case NodeTypes.InkNode:
                    nodes.Add(new InkNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, ConversationData[i]));
                    break;
                case NodeTypes.LoadNode:
                    nodes.Add(new LoadNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, ConversationData[i]));
                    break;
                case NodeTypes.LoopNode:
                    nodes.Add(new LoopNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, ConversationData[i]));
                    break;
                case NodeTypes.CheatNode:
                    nodes.Add(new CheatNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, ConversationData[i]));
                    break;
                case NodeTypes.RoomNode:
                    nodes.Add(new RoomNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, ConversationData[i]));
                    break;
                case NodeTypes.SoundNode:
                    nodes.Add(new SoundNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, ConversationData[i]));
                    break;
                case NodeTypes.StampNode:
                    nodes.Add(new SceneStampNode(pos, (float)width, (float)height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode, ConversationData[i]));
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
        path = "Assets/Resources/Timeline/" + ConversationName + ".json";
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
            Jwriter.WritePropertyName("color");
            Jwriter.Write((node).NodeColor);

            switch (node.TypeID)
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
                        case ProgressType.Objective:

                            Jwriter.WritePropertyName("TaskNumber");
                            Jwriter.Write(((ProgressNode)node).TaskID);
                            Jwriter.WritePropertyName("TaskState");
                            Jwriter.Write((int)((ProgressNode)node).NewTaskState);
                            break;

                        case ProgressType.CG:
                            Jwriter.WritePropertyName("ImageSlug");
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
                        case ProgressType.Objective:

                            Jwriter.WritePropertyName("TaskNumber");
                            Jwriter.Write(((ChangeNode)node).TaskID);
                            Jwriter.WritePropertyName("TaskState");
                            Jwriter.Write((int)((ChangeNode)node).NewTaskState);
                            break;

                        case ProgressType.CG:
                            Jwriter.WritePropertyName("ImageSlug");
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
                            break;
                        default:
                            Debug.LogError("Unrecognized Option");
                            break;
                    }

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
                case NodeTypes.MapNode:


                    Jwriter.WritePropertyName("Tag");
                    Jwriter.Write(((MapNode)node).Tag);

                    Jwriter.WritePropertyName("Day");
                    Jwriter.Write(((MapNode)node).Day);
                    Jwriter.WritePropertyName("Hour");
                    Jwriter.Write(((MapNode)node).Hour);
                    Jwriter.WritePropertyName("Length");
                    Jwriter.Write(((MapNode)node).Length);

                    Jwriter.WritePropertyName("Room");
                    Jwriter.Write((int)((MapNode)node).Locale);

                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((MapNode)node).NextID);


                    Jwriter.WritePropertyName("Locks");
                    Jwriter.WriteArrayStart();

                    for(int i = 0; i < ((MapNode)node).Locks.Count; ++i)
                    {

                        Jwriter.WriteObjectStart();
                        Jwriter.WritePropertyName("Name");
                        Jwriter.Write(((MapNode)node).Locks[i].ProgressName);
                        Jwriter.WritePropertyName("Type");
                        Jwriter.Write((int)((MapNode)node).Locks[i].TypeID);

                        switch (((MapNode)node).Locks[i].TypeID)
                        {
                            case PointTypes.Flag:
                                Jwriter.WritePropertyName("MatchValue");
                                Jwriter.Write(((MapNode)node).Locks[i].BoolValue);
                                break;
                            case PointTypes.Float:
                                Jwriter.WritePropertyName("MatchValue");
                                Jwriter.Write(((MapNode)node).Locks[i].FloatValue);
                                Jwriter.WritePropertyName("Compare");
                                Jwriter.Write((int)((MapNode)node).Locks[i].compare);
                                break;
                            case PointTypes.Integer:
                                Jwriter.WritePropertyName("MatchValue");
                                Jwriter.Write(((MapNode)node).Locks[i].IntValue);
                                Jwriter.WritePropertyName("Compare");
                                Jwriter.Write((int)((MapNode)node).Locks[i].compare);
                                break;
                            case PointTypes.String:
                                Jwriter.WritePropertyName("MatchValue");
                                Jwriter.Write(((MapNode)node).Locks[i].StringValue);
                                break;
                            default:
                                break;
                        }

                        Jwriter.WriteObjectEnd();
                    }

                    Jwriter.WriteArrayEnd();
                    
                    Jwriter.WritePropertyName("Characters");
                    Jwriter.WriteArrayStart();

                    for (int i = 0; i < ((MapNode)node).PeoplePresent.Count; ++i)
                    {
                        Jwriter.Write(((MapNode)node).PeoplePresent[i]);
                    }           

                    Jwriter.WriteArrayEnd();
                    
                    break;
                case NodeTypes.ToMapNode:

                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((ToMapNode)node).NextID);
                    break;
                case NodeTypes.InkNode:

                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((InkNode)node).NextID);
                    Jwriter.WritePropertyName("Story");
                    if (((InkNode)node).InkFile != null)
                    {
                        string txt = AssetDatabase.GetAssetPath(((InkNode)node).InkFile);
                        
                        //txt = Path.GetFileNameWithoutExtension(txt);
                        txt = txt.Replace("Assets/Resources/", "");
                        //removes the file extention off the string
                        //txt = txt.Remove(txt.Length - 5);
                        txt = txt.Substring(0, txt.LastIndexOf('.'));
                        Jwriter.Write(txt);
                    }
                    else
                    {
                        Jwriter.Write(null);
                    }
                    break;

                case NodeTypes.LoadNode:

                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((LoadNode)node).NextID);
                    break;
                case NodeTypes.LoopNode:

                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((LoopNode)node).NextID);
                    Jwriter.WritePropertyName("Day");
                    Jwriter.Write(((LoopNode)node).Day);
                    Jwriter.WritePropertyName("Hour");
                    Jwriter.Write(((LoopNode)node).Hour);
                    break;
                case NodeTypes.CheatNode:

                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((CheatNode)node).NextID);
                    Jwriter.WritePropertyName("Code");
                    Jwriter.Write(((CheatNode)node).CheatCode);
                    break;
                case NodeTypes.RoomNode:

                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((RoomNode)node).NextID);
                    Jwriter.WritePropertyName("Room");
                    Jwriter.Write((int)((RoomNode)node).Locale);
                    Jwriter.WritePropertyName("Transition");
                    Jwriter.Write((int)((RoomNode)node).transition);
                    break;
                case NodeTypes.SoundNode:

                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((SoundNode)node).NextID);
                    Jwriter.WritePropertyName("Sound");
                    Jwriter.Write(((SoundNode)node).SoundFile);
                    Jwriter.WritePropertyName("Layer");
                    Jwriter.Write((int)((SoundNode)node).Layer);
                    break;
                case NodeTypes.StampNode:
                    
                    Jwriter.WritePropertyName("NextID");
                    Jwriter.Write(((SceneStampNode)node).NextID);
                    Jwriter.WritePropertyName("Tag");
                    Jwriter.Write(((SceneStampNode)node).Tag);

                    Jwriter.WritePropertyName("Day");
                    Jwriter.Write(((SceneStampNode)node).Day);
                    Jwriter.WritePropertyName("Hour");
                    Jwriter.Write(((SceneStampNode)node).Hour);
                    Jwriter.WritePropertyName("Length");
                    Jwriter.Write(((SceneStampNode)node).Length);
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
