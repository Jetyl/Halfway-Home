using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteruptConnectionPoint : ConnectionPoint
{

    
    public InteruptConnectionPoint(BaseNode node, ConnectionPointType type, GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint) : base(node, type, style, OnClickConnectionPoint)
    {
    }

    public override void Draw()
    {
        rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;

        switch (type)
        {
            case ConnectionPointType.In:
                rect.x = node.rect.x - rect.width + 8f;
                break;

            case ConnectionPointType.Out:
                rect.x = node.rect.x + node.rect.width - 8f;
                rect.y += 20f;
                GUI.color = Color.green;
                break;
            case ConnectionPointType.Branch:
                rect.x = node.rect.x + node.rect.width - 8f;
                rect.y -= 20f;
                GUI.color = Color.red;
                break;
        }

        if (GUI.Button(rect, "", style))
        {
            if (OnClickConnectionPoint != null)
            {
                OnClickConnectionPoint(this);
            }
        }
        GUI.color = Color.white;
    }


    public void Draw(int yPos)
    {
        rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;

        switch (type)
        {
            case ConnectionPointType.In:
                rect.x = node.rect.x - rect.width + 8f;
                break;

            case ConnectionPointType.Branch:
                rect.x = node.rect.x + node.rect.width - 8f;
                rect.y += yPos;
                GUI.color = Color.red;
                break;

        }

        if (GUI.Button(rect, "", style))
        {
            if (OnClickConnectionPoint != null)
            {
                OnClickConnectionPoint(this);
            }
        }

        GUI.color = Color.white;
    }



}
