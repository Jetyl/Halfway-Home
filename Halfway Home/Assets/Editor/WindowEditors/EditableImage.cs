using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using LitJson;
using UnityEditor;

public class EditableImage
{
    public string Name;

    public Sprite Image;
    public int Set;

    public string Caption;

    public EditableImage()
    {
        Name = "";
        Image = null;
        Set = 0;
        Caption = "";
    }


    public EditableImage(JsonData data)
    {
        Name = (string)data["Name"];
        string slug = null;

        if (data["Slug"] != null)
        {
            slug = (string)data["Slug"];
            Image = Resources.Load<Sprite>("Sprites/" + slug);
        }

        Set = (int)data["Set"];

        Caption = (string)data["Caption"];
    }

    public void Draw()
    {
        Name = EditorGUILayout.TextField("Item Name", Name);

        Image = EditorGUILayout.ObjectField(Image, typeof(Sprite), allowSceneObjects: true) as Sprite;

        Set = EditorGUILayout.IntField("Set Number", Set);

        Caption = EditorGUILayout.TextField("Caption", Caption);

    }



}
