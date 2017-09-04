using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class StartNode : BaseNode
{

    public bool DisableUI = false;

    public StartNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        inPoint = null;
        ID = 0;
        NextID = -1;
        TypeID = NodeTypes.StartNode;
        DisableUI = false;
    }

    public StartNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int nextID, string Title, bool Disable) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        title = Title;
        inPoint = null;
        ID = 0;
        NextID = nextID;
        TypeID = NodeTypes.StartNode;
        DisableUI = Disable;
    }


    public override void Draw()
    {
        
        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), "Start Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 40), new Vector2(150, 20)), title);
        DisableUI = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 70), new Vector2(150, 20)), "Disable UI", DisableUI);
    }


}
