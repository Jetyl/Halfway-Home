using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public class BaseNode
{
    public Rect rect;
    public string title;
    public int ID;
    public int NextID = -1;
    public NodeTypes TypeID;
    public bool isDragged;
    public bool isSelected;

    public int NodeColor = 1;

    public ConnectionPoint inPoint;
    public ConnectionPoint outPoint;

    public GUIStyle style;
    public GUIStyle defaultNodeStyle;
    public GUIStyle selectedNodeStyle;
    public Action<BaseNode> OnRemoveNode;
    public Action<BaseNode> OnDuplicateNode;

    public BaseNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode) 
    {
        rect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;
        inPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        defaultNodeStyle = new GUIStyle(nodeStyle);
        selectedNodeStyle = new GUIStyle(selectedStyle);
        ChangeColor(NodeColor);
        OnRemoveNode = OnClickRemoveNode;
        OnDuplicateNode = OnClickDuplicateNode;
        title = "";
    }

    public virtual BaseNode Duplicate(int index)
    {
        Vector2 pos = new Vector2(rect.x + 25, rect.y + 25);

        BaseNode copy = new BaseNode(pos, rect.width, rect.height, style, selectedNodeStyle, inPoint.style, outPoint.style,
            inPoint.OnClickConnectionPoint, outPoint.OnClickConnectionPoint, OnRemoveNode, OnDuplicateNode);

        copy.ChangeColor(NodeColor);
        copy.ID = index;

        return copy;

    }

    public void Drag(Vector2 delta)
    {
        rect.position += delta;
    }

    public virtual void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, title, style);
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 15), new Vector2(150, 20)), title);
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (rect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;
                        isSelected = true;
                        style = selectedNodeStyle;
                    }
                    else
                    {
                        GUI.changed = true;
                        isSelected = false;
                        style = defaultNodeStyle;
                    }
                }
                if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.AddItem(new GUIContent("Duplicate node"), false, OnClickDuplicate);


        genericMenu.AddItem(new GUIContent("Change Color/Grey"), false, () => ChangeColor(0));
        genericMenu.AddItem(new GUIContent("Change Color/Cyan"), false, () => ChangeColor(1));
        genericMenu.AddItem(new GUIContent("Change Color/Seafoam"), false, () => ChangeColor(2));
        genericMenu.AddItem(new GUIContent("Change Color/Green"), false, () => ChangeColor(3));
        genericMenu.AddItem(new GUIContent("Change Color/Yellow"), false, () => ChangeColor(4));
        genericMenu.AddItem(new GUIContent("Change Color/Orange"), false, () => ChangeColor(5));
        genericMenu.AddItem(new GUIContent("Change Color/Red"), false, () => ChangeColor(6));

        genericMenu.ShowAsContext();
    }


    protected void ChangeColor(int number)
    {
        NodeColor = number;
        defaultNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node" + number +".png") as Texture2D;
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node" + number + " on.png") as Texture2D;
    }

    private void OnClickRemoveNode()
    {
        if (OnRemoveNode != null)
        {
            OnRemoveNode(this);
        }
    }


    private void OnClickDuplicate()
    {
        if (OnDuplicateNode != null)
        {
            OnDuplicateNode(this);
        }
    }

}

