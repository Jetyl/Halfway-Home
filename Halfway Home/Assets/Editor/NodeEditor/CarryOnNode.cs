using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;
using UnityEditorInternal;

public class CarryOnNode : BaseNode
{
    
    public List<ConvInteruprt> Interupts;

    public int NumOfInterupts;

    public CarryOnNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, int MyID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        ID = MyID;
        Interupts = new List<ConvInteruprt>();
        TypeID = NodeTypes.CarryOnNode;

        

    }

    public CarryOnNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {

        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];
        

        Interupts = new List<ConvInteruprt>();
        
        //Interupts = TextParser.ParseLines(data["Lines"]);

        NumOfInterupts = data["Interupts"].Count;


        for (int i = 0; i < NumOfInterupts; ++i)
        {
            var injection = new ConvInteruprt();
            injection.idea = (string)data["Interupts"][i]["Idea"];

            if (data["Interupts"][i]["Slug"] != null)
            {
                injection.item = Resources.Load("Sprites/" + (string)data["Interupts"][i]["Slug"]) as Sprite;
            }
            else
            {
                injection.item = null;
            }
            injection.DestinationID = (int)data["Interupts"][i]["Destinations"];
            injection.type = ((InteruptionTypes)(int)data["Interupts"][i]["InteruptType"]);
            Interupts.Add(injection);
        }

        TypeID = NodeTypes.CarryOnNode;

    }


    public override void Draw()
    {

        inPoint.Draw();
        outPoint.Draw();

        rect.size = new Vector2(300, 140 + (30 * NumOfInterupts));

        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(250, 20)), "Carry On Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(250, 20)), title);
        GUI.Label(new Rect(rect.position + new Vector2(25, 65), new Vector2(250, 20)), "ID: " + ID);
        //NumOfLines = EditorGUI.DelayedIntField(new Rect(rect.position + new Vector2(25, 85), new Vector2(150, 20)), NumOfLines);
        //EditorGUI.PropertyField(new Rect(rect.position + new Vector2(25, 115), new Vector2(150, 20)), (SerializedProperty)Lines, true);


        NumOfInterupts = EditorGUI.DelayedIntField(new Rect(rect.position + new Vector2(25, 85), new Vector2(50, EditorGUIUtility.singleLineHeight)), NumOfInterupts);

        ResizeChoices();

        for (int i = 0; i < NumOfInterupts; ++i)
        {
            int pos = 110 + (25 * i);
            var element = Interupts[i];
            element.type = (InteruptionTypes)EditorGUI.EnumPopup(new Rect(rect.position.x + 25, rect.position.y + pos, 80, EditorGUIUtility.singleLineHeight),
                element.type);
            switch (element.type)
            {
                case InteruptionTypes.Item:
                    element.item = EditorGUI.ObjectField(new Rect(rect.position.x + 25 + 80, rect.position.y + pos, 250 - 80 - 50, EditorGUIUtility.singleLineHeight),
                element.item, typeof(Sprite), allowSceneObjects: true) as Sprite;
                    break;
                case InteruptionTypes.Idea:
                    element.idea = EditorGUI.TextField(new Rect(rect.position.x + 25 + 80, rect.position.y + pos, 250 - 80 - 50, EditorGUIUtility.singleLineHeight),
                element.idea);
                    break;
                case InteruptionTypes.Inventory:
                    element.item = EditorGUI.ObjectField(new Rect(rect.position.x + 25 + 80, rect.position.y + pos, 250 - 80 - 50, EditorGUIUtility.singleLineHeight),
                element.item, typeof(Sprite), allowSceneObjects: true) as Sprite;
                    break;
                case InteruptionTypes.Phone:
                    break;
            }

            EditorGUI.LabelField(new Rect(rect.position.x + 25 + 80 + 125, rect.position.y + pos, 50, EditorGUIUtility.singleLineHeight),
                "ID: " + element.DestinationID);
            Interupts[i] = element;
            //rect.size = new Vector2(200, 125 + (25 * i));
            //ChoicePoints[i].Draw(((pos/2) - 5));
        }
        if (NumOfInterupts == 0)
        {
            rect.size = new Vector2(300, 120);
        }


        //List.DoList(new Rect(rect.position + new Vector2(25, 110), new Vector2(250, 20)));

        //rect.size = new Vector2(300, 125 + List.GetHeight());



    }

    void ResizeChoices()
    {

        if (NumOfInterupts < 0)
            NumOfInterupts = 0;
        

        while (Interupts.Count > NumOfInterupts)
        {
            Interupts.RemoveAt(Interupts.Count - 1);
        }
        while (Interupts.Count < NumOfInterupts)
        {
            Interupts.Add(new ConvInteruprt());
        }



    }
    
}
