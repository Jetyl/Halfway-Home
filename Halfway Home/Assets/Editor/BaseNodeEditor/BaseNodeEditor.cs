using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class BaseNodeEditor : EditorWindow
{
    protected List<BaseNode> nodes;
    protected List<Connection> connections;

    protected GUIStyle nodeStyle;
    protected GUIStyle selectedNodeStyle;
    protected GUIStyle inPointStyle;
    protected GUIStyle outPointStyle;

    protected ConnectionPoint selectedInPoint;
    protected ConnectionPoint selectedOutPoint;

    protected Vector2 offset;
    protected Vector2 drag;

    protected float zoomScale = 1.0f;
    protected Rect _zoomArea = new Rect(0, 0, Screen.width * 2.5f, Screen.height * 2);
    protected Rect ZoomedArea;

    [MenuItem("Window/a-0/Base Editor")]
    private static void OpenWindow()
    {
        BaseNodeEditor window = GetWindow<BaseNodeEditor>();
        window.titleContent = new GUIContent("test node editor");
    }

    private void OnEnable()
    {
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);

        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);
        
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
        //reset the matrix
        EditorZoomArea.End();

        ProcessEvents(Event.current);

        if (GUI.changed) Repaint();
    }

    protected void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(ZoomedArea.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(ZoomedArea.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);
        
        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, ZoomedArea.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(ZoomedArea.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    protected void DrawNodes()
    {
        if (nodes != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Draw();
            }
        }
    }

    protected void DrawConnections()
    {
        if (connections != null)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                connections[i].Draw();
            }
        }
    }

    protected void ProcessEvents(Event e)
    {
        drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    ClearConnectionSelection();
                }

                if (e.button == 1)
                {
                    float ScaledX = e.mousePosition.x * (ZoomedArea.width / position.width);
                    float ScaledY = e.mousePosition.y * (ZoomedArea.height / position.height);
                    //ScaledY -= 21;
                    Vector2 scalePos = new Vector2(ScaledX, ScaledY);

                    ProcessContextMenu(scalePos);
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    OnDrag(e.delta * (1/zoomScale));
                }
                break;
            case EventType.ScrollWheel:
                var zoomDelta = 0.1f;
                var oldZoom = zoomScale;
                zoomDelta = e.delta.y < 0 ? zoomDelta : -zoomDelta;
                zoomScale += zoomDelta;
                zoomScale = Mathf.Clamp(zoomScale, 0.1f, 1f);
                
                
                if (zoomScale != oldZoom)
                {
                    Rect nextRect = _zoomArea.ScaleSizeBy(1.0f / zoomScale, _zoomArea.TopLeft());
                    float GrowthX = nextRect.width - ZoomedArea.width;
                    float GrowthY = nextRect.height - ZoomedArea.height;
                    var ZoomDir = new Vector2(GrowthX/2, GrowthY/2);
                    OnDrag(ZoomDir);

                }
                e.Use();
                break;
        }
    }

    protected void ProcessNodeEvents(Event e)
    {
        if (nodes != null)
        {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                bool guiChanged = nodes[i].ProcessEvents(e);

                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }
    }
    
    protected void DrawConnectionLine(Event e)
    {
        if (selectedInPoint != null && selectedOutPoint == null)
        {
            Handles.DrawBezier(
                selectedInPoint.rect.center,
                e.mousePosition,
                selectedInPoint.rect.center + Vector2.left * 100f,
                e.mousePosition - Vector2.left * 100f,
                Color.white,
                null,
                5f
            );

            GUI.changed = true;
        }

        if (selectedOutPoint != null && selectedInPoint == null)
        {
            Handles.DrawBezier(
                selectedOutPoint.rect.center,
                e.mousePosition,
                selectedOutPoint.rect.center - Vector2.left * 100f,
                e.mousePosition + Vector2.left * 100f,
                Color.white,
                null,
                5f
            );

            GUI.changed = true;
        }
    }

    protected virtual void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add node"), false, () => OnClickAddNode(mousePosition));
        genericMenu.ShowAsContext();
    }

    private void OnDrag(Vector2 delta)
    {
        drag = delta;

        if (nodes != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Drag(delta);
            }
        }

        GUI.changed = true;
    }

    virtual protected void OnClickAddNode(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<BaseNode>();
        }

        nodes.Add(new BaseNode(mousePosition, 200, 50, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode));
    }

    protected void OnClickInPoint(ConnectionPoint inPoint)
    {
        selectedInPoint = inPoint;

        if (selectedOutPoint != null)
        {
            //is not on same node, and nodes are free to connect
            if (selectedOutPoint.node != selectedInPoint.node && CanMakeConnection())
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    protected void OnClickOutPoint(ConnectionPoint outPoint)
    {
        selectedOutPoint = outPoint;

        
        if (selectedInPoint != null)
        {
            //is not on same node, and nodes are free to connect
            if (selectedOutPoint.node != selectedInPoint.node && CanMakeConnection())
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
        else if (CanMakeConnection() == false)
        {
            ClearConnectionSelection();
        }
            
    }

    protected virtual void OnClickRemoveNode(BaseNode node)
    {
        if (connections != null)
        {
            List<Connection> connectionsToRemove = new List<Connection>();

            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].inPoint == node.inPoint || connections[i].outPoint == node.outPoint)
                {
                    connectionsToRemove.Add(connections[i]);
                }
            }

            for (int i = 0; i < connectionsToRemove.Count; i++)
            {
                connections.Remove(connectionsToRemove[i]);
            }

            connectionsToRemove = null;
        }

        nodes.Remove(node);
    }

    protected virtual void OnClickRemoveConnection(Connection connection)
    {
        connections.Remove(connection);
    }

    protected virtual void CreateConnection()
    {
        if (connections == null)
        {
            connections = new List<Connection>();
        }

        connections.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
    }

    private void ClearConnectionSelection()
    {
        selectedInPoint = null;
        selectedOutPoint = null;
    }

    //if the outgoing connection node points to something else already
    private bool CanMakeConnection()
    {

        if (connections != null)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].outPoint == selectedOutPoint)
                {
                    return false;
                }
            }
        }


        return true;
    }

}


// Helper Rect extension methods
public static class RectExtensions
{
    public static Vector2 TopLeft(this Rect rect)
    {
        return new Vector2(rect.xMin, rect.yMin);
    }
    public static Rect ScaleSizeBy(this Rect rect, float scale)
    {
        return rect.ScaleSizeBy(scale, rect.center);
    }
    public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
    {
        Rect result = rect;
        result.x -= pivotPoint.x;
        result.y -= pivotPoint.y;
        result.xMin *= scale;
        result.xMax *= scale;
        result.yMin *= scale;
        result.yMax *= scale;
        result.x += pivotPoint.x;
        result.y += pivotPoint.y;
        return result;
    }
    public static Rect ScaleSizeBy(this Rect rect, Vector2 scale)
    {
        return rect.ScaleSizeBy(scale, rect.center);
    }
    public static Rect ScaleSizeBy(this Rect rect, Vector2 scale, Vector2 pivotPoint)
    {
        Rect result = rect;
        result.x -= pivotPoint.x;
        result.y -= pivotPoint.y;
        result.xMin *= scale.x;
        result.xMax *= scale.x;
        result.yMin *= scale.y;
        result.yMax *= scale.y;
        result.x += pivotPoint.x;
        result.y += pivotPoint.y;
        return result;
    }
}

public class EditorZoomArea
{
    private const float kEditorWindowTabHeight = 21.0f;
    private static Matrix4x4 _prevGuiMatrix;

    public static Rect Begin(float zoomScale, Rect screenCoordsArea)
    {
        GUI.EndGroup();        // End the group Unity begins automatically for an 
                               //EditorWindow to clip out the window tab. This allows us to 
                               //draw outside of the size of the EditorWindow.

        Rect clippedArea = screenCoordsArea.ScaleSizeBy(1.0f / zoomScale, screenCoordsArea.TopLeft());
        clippedArea.y += kEditorWindowTabHeight;
        GUI.BeginGroup(clippedArea);

        _prevGuiMatrix = GUI.matrix;
        Vector2 vanishingPoint = new Vector2(Screen.width/2, Screen.height/2);
        Matrix4x4 translation = Matrix4x4.TRS(clippedArea.TopLeft(), Quaternion.identity, Vector3.one);
        Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));
        GUI.matrix = translation * scale * translation.inverse * GUI.matrix;

        return clippedArea;
    }

    public static void End()
    {
        GUI.matrix = _prevGuiMatrix;
        GUI.EndGroup();
        GUI.BeginGroup(new Rect(0.0f, kEditorWindowTabHeight, Screen.width, Screen.height));
    }
}