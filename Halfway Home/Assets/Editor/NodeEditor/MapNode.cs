using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using LitJson;
using UnityEditorInternal;

public class MapNode : BaseNode
{

    string[] EventNames;

    public Room Locale;
    public int Day;
    public int Hour;
    public int Length;
    public List<string> PeoplePresent;
    public List<ProgressPoint> Locks;
    private ReorderableList ListOfLocks;
    private ReorderableList ListOfPeople;

    bool Opened;
    bool People; 

    public MapNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, int NodeID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        //inPoint = null;
        ID = NodeID;
        NextID = -1;
        TypeID = NodeTypes.MapNode;

        PeoplePresent = new List<string>();
        Locks = new List<ProgressPoint>();

        OrganizeLines();
    }

    public MapNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        title = (string)data["title"];
        //inPoint = null;
        ID = (int)data["ID"];
        NextID = (int)data["NextID"]; 
        TypeID = NodeTypes.MapNode;
        PeoplePresent = new List<string>();
        Locks = new List<ProgressPoint>();


        if (data.Keys.Contains("color"))
            ChangeColor((int)data["color"]);

        Day = (int)data["Day"];
        Hour = (int)data["Hour"];
        Length = (int)data["Length"];

        Locale = (Room)(int)data["Room"];


        if (data.Keys.Contains("Locks"))
        {
            for(int i = 0; i < data["Locks"].Count; ++i)
            {
                Locks.Add(new ProgressPoint(data["Locks"][i]));
            }
        }

        if (data.Keys.Contains("Characters"))
        {
            for (int i = 0; i < data["Characters"].Count; ++i)
            {
                PeoplePresent.Add((string)data["Characters"][i]);
            }
        }


        OrganizeLines();

    }

    public override BaseNode Duplicate(int index)
    {

        Vector2 pos = new Vector2(rect.x + 25, rect.y + 25);

        Debug.Log(inPoint);

        MapNode copy = new MapNode(pos, rect.width, rect.height, style, selectedNodeStyle, inPoint.style, outPoint.style,
            inPoint.OnClickConnectionPoint, outPoint.OnClickConnectionPoint, OnRemoveNode, OnDuplicateNode, index);

        copy.Locale = Locale;
        copy.Hour = Hour;
        copy.Day = Day;
        copy.Length = Length;
        copy.Locks = Locks.ConvertAll(book => new ProgressPoint(book));
        copy.PeoplePresent = new List<string>(PeoplePresent);

        
        copy.OrganizeLines();

        copy.ChangeColor(NodeColor);
        copy.ID = index;

        return copy;
    }

    public override void Draw()
    {

        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(300, 20)), "Map Choice Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 40), new Vector2(300, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 60), new Vector2(150, 20)), "ID: " + ID);

        Locale = (Room)EditorGUI.EnumPopup(new Rect(rect.position + new Vector2(25, 80), new Vector2(300, 20)), new GUIContent("Room Location"), Locale);
        Day = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(25, 105), new Vector2(300, 20)), new GUIContent("Day of the Week"), Day, 0, 7);
        Hour = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(25, 130), new Vector2(300, 20)), new GUIContent("Hour of the Day"), Hour, 0, 23);
        Length = EditorGUI.IntSlider(new Rect(rect.position + new Vector2(25, 155), new Vector2(300, 20)), new GUIContent("Length of time Availble"), Length, 1, 24);

        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 175), new Vector2(300, 20)), "Avalible from " + GetTime(Hour) + " to " + GetTime(Hour + Length));

        Opened = EditorGUI.Foldout(new Rect(rect.position + new Vector2(25, 190), new Vector2(150, 20)), Opened, "Scene Locks");
        
        People = EditorGUI.Foldout(new Rect(rect.position + new Vector2(175, 190), new Vector2(150, 20)), People, "People Present");
        

        Vector2 Size = new Vector2(350, 225);
        if (Opened)
        {
            ListOfLocks.DoList(new Rect(rect.position + new Vector2(25, 210), new Vector2(300, 20)));
            
            if (ListOfLocks.count > 0)
                Size.y += (20 * ListOfLocks.count) + 40;
            else
                Size.y += 60;

            People = false;
        }
        
        if (People)
        {
            ListOfPeople.DoList(new Rect(rect.position + new Vector2(25, 210), new Vector2(300, 20)));

            if (ListOfPeople.count > 0)
                Size.y += (20 * ListOfPeople.count) + 40;
            else
                Size.y += 60;

        }

        rect.size = Size;
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


    void OrganizeLines()
    {
        ListOfLocks = new ReorderableList(Locks, typeof(ProgressPoint), true, true, true, true);

        ListOfLocks.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Locks");
        };

        ListOfLocks.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = (ProgressPoint)ListOfLocks.list[index];
        rect.y += 2;
        element.TypeID = (PointTypes)EditorGUI.EnumPopup(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), element.TypeID);
        element.ProgressName = EditorGUI.TextField(new Rect(rect.x + 60, rect.y, rect.width - 60 - 60, EditorGUIUtility.singleLineHeight),
            element.ProgressName);

        switch (element.TypeID)
        {
            case PointTypes.Flag:
                element.BoolValue = EditorGUI.Toggle(new Rect(rect.x + rect.width - 60, rect.y, 60, EditorGUIUtility.singleLineHeight),
                    element.BoolValue);
                break;
            case PointTypes.Float:
                element.FloatValue = EditorGUI.FloatField(new Rect(rect.x + rect.width - 60, rect.y, 30, EditorGUIUtility.singleLineHeight),
                    element.FloatValue);
                element.compare = (ValueCompare)EditorGUI.EnumPopup(new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
                    element.compare);

                break;
            case PointTypes.Integer:
                element.IntValue = EditorGUI.IntField(new Rect(rect.x + rect.width - 60, rect.y, 30, EditorGUIUtility.singleLineHeight),
                    element.IntValue);
                element.compare = (ValueCompare)EditorGUI.EnumPopup(new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
                    element.compare);

                break;
            case PointTypes.String:
                element.StringValue = EditorGUI.TextField(new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
                    element.StringValue);
                break;
            default:
                break;
        }
        
        ListOfLocks.list[index] = element;
    };

        List<string> eventNames = new List<string>();

        var list = TextParser.ToJson("Characters");

        foreach (JsonData element in list)
        {

            eventNames.Add((string)element["Name"]);

        }

        EventNames = eventNames.ToArray();

        ListOfPeople = new ReorderableList(PeoplePresent, typeof(string), true, true, true, true);

        ListOfPeople.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "People");
        };

        ListOfPeople.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = (string)ListOfPeople.list[index];
        rect.y += 2;

        //getting positions in array for pop-up
        var point = Array.IndexOf(EventNames, element);

        if (point == -1)
        {
            point = 0;
        }

        element = EventNames[EditorGUI.Popup(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), "", point, EventNames)];

        ListOfPeople.list[index] = element;
    };

        ListOfPeople.onAddCallback =
   (ReorderableList List) => {

       ListOfPeople.list.Add("");
   };

        // List.onChangedCallback
    }


}
