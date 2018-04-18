using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;
using UnityEditorInternal;

public class LineNode : BaseNode
{
    
    public int NumOfLines;
    public bool CallNextNodeImmediately;
    public bool DenyPlayerInput;
    private ReorderableList List;
    public List<Line> Lines;

    float size;


    public LineNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, int MyID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        ID = MyID;
        Lines = new List<Line>();
        TypeID = NodeTypes.LineNode;
        CallNextNodeImmediately = false;
        DenyPlayerInput = false;
        size = width;
        OrganizeLines();

    }

    public LineNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {

        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];

        NumOfLines = data["Lines"].Count;
        
        Lines = TextParser.ParseLines(data["Lines"]);
        
        CallNextNodeImmediately = (bool)data["ImmediateNext"];

        DenyPlayerInput = (bool)data["DenyInput"];

        TypeID = NodeTypes.LineNode;
        size = width;
        OrganizeLines();
    }


    public override void Draw()
    {

        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(size - 50, 20)), "Line Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(size - 50, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 65), new Vector2(size - 50, 20)), "ID: " + ID);
        //NumOfLines = EditorGUI.DelayedIntField(new Rect(rect.position + new Vector2(25, 85), new Vector2(150, 20)), NumOfLines);
        CallNextNodeImmediately = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 85), new Vector2(size - 50, 20)), "Call Next Node Immediately", CallNextNodeImmediately);
        DenyPlayerInput = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 110), new Vector2(size - 50, 20)), "Deny Player Input", DenyPlayerInput);
        //EditorGUI.PropertyField(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 20)), (SerializedProperty)Lines, true);
        size = EditorGUI.Slider(new Rect(rect.position + new Vector2(25, 125), new Vector2(300, 20)), size, 350, 1000);

        List.DoList(new Rect(rect.position + new Vector2(25, 150), new Vector2(size - 50, 20)));

        NumOfLines = Lines.Count;
        rect.size = new Vector2(size, 165 + List.GetHeight());

        

    }

    

    void OrganizeLines()
    {
        List = new ReorderableList(Lines, typeof(Line), true, true, true, true);

        List.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Lines");
        };

        List.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = (Line)List.list[index];
        rect.y += 2;
        element.Speaker = EditorGUI.TextField(new Rect(rect.x, rect.y, 80, EditorGUIUtility.singleLineHeight),
            element.Speaker);
        element.Dialog = EditorGUI.TextField(new Rect(rect.x + 80, rect.y, rect.width - 80 - 30, EditorGUIUtility.singleLineHeight),
            element.Dialog);
        element.Pace = EditorGUI.FloatField(new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
            element.Pace);
        List.list[index] = element;
    };


       // List.onChangedCallback
    }


}
