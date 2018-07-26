using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEditor;

public class SoundNode : BaseNode
{
    
    public AudioClip sfx;
    public bool loop;
    public bool Remove;
    public bool Music;

    public string SoundFile;
    public AudioManager.AudioEvent.SoundType Layer;


    public SoundNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, int MyID) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {
        ID = MyID;
        TypeID = NodeTypes.SoundNode;
        SoundFile = "";
    }

    public SoundNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<BaseNode> OnClickRemoveNode, Action<BaseNode> OnClickDuplicateNode, JsonData data) : base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickDuplicateNode)
    {

        ID = (int)data["ID"];

        NextID = (int)data["NextID"];

        title = (string)data["title"];
        
        //if (data["Slug"] != null)
        //{
        //    sfx = Resources.Load("Sounds/" + (string)data["Slug"]) as AudioClip;
        //}
        //loop = (bool)data["bool"];
        //Remove = (bool)data["stop"];
        //
        TypeID = NodeTypes.SoundNode;
        
        if (data.Keys.Contains("color"))
            ChangeColor((int)data["color"]);

        if (data["Sound"] != null)
            SoundFile = (string)data["Sound"];
        Layer = (AudioManager.AudioEvent.SoundType)(int)data["Layer"];


    }


    public override void Draw()
    {

        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, "", style);
        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 15), new Vector2(250, 20)), "Sound Node");
        title = GUI.TextField(new Rect(rect.position + new Vector2(25, 35), new Vector2(250, 20)), title);
        //GUI.Label(new Rect(rect.position + new Vector2(25, 65), new Vector2(250, 20)), "ID: " + ID);


        EditorGUI.LabelField(new Rect(rect.position + new Vector2(25, 60), new Vector2(250, 20)), "Sound Tag");
        SoundFile = GUI.TextField(new Rect(rect.position + new Vector2(25, 75), new Vector2(250, 20)), SoundFile);


        Layer = (AudioManager.AudioEvent.SoundType)EditorGUI.EnumPopup(
            new Rect(rect.position + new Vector2(25, 105), new Vector2(250, 20)), 
            new GUIContent("Sound Layer"), Layer);

        //loop = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 85), new Vector2(50, 20)), "Loop", loop);
        //Remove = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(75, 85), new Vector2(100, 20)), "Remove", Remove);
        //Music = EditorGUI.ToggleLeft(new Rect(rect.position + new Vector2(25, 105), new Vector2(150, 20)), "Music", Music);
        //
        //sfx = EditorGUI.ObjectField(new Rect(rect.position + new Vector2(25, 125), new Vector2(150, 15)), sfx, typeof(AudioClip), allowSceneObjects: true) as AudioClip;

    }

}
