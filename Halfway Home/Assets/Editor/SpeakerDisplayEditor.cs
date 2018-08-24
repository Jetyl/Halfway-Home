using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;
using LitJson;

//[CustomEditor(typeof(SpeakerDisplay))]
public class SpeakerDisplayEditor : Editor
{

    int AmountOfCharacters;

    List<string> Names;

    private void OnEnable()
    {
        Names = new List<string>();

        var list = TextParser.ToJson("Characters");

        foreach (JsonData element in list)
        {

            Names.Add((string)element["Name"]);

        }

        AmountOfCharacters = Names.Count;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //SerializedProperty Speakers = serializedObject.FindProperty("Speakers");
        SerializedProperty Characters = serializedObject.FindProperty("Characters");
        SerializedProperty Colors = serializedObject.FindProperty("Colors");

        SerializedProperty Box = serializedObject.FindProperty("Box");


        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(Box, new GUIContent("Speaker Box"));

        Characters.arraySize = AmountOfCharacters + 1;
        Colors.arraySize = AmountOfCharacters + 1;

        Characters.GetArrayElementAtIndex(0).stringValue = "#PlayerName";
        Colors.GetArrayElementAtIndex(0).colorValue = EditorGUILayout.ColorField("Player", Colors.GetArrayElementAtIndex(0).colorValue); 

        for(int i = 0; i < AmountOfCharacters; ++i)
        {
            Characters.GetArrayElementAtIndex(i + 1).stringValue = Names[i];
            Colors.GetArrayElementAtIndex(i + 1).colorValue = EditorGUILayout.ColorField(Names[i], Colors.GetArrayElementAtIndex(i + 1).colorValue);

        }



        serializedObject.ApplyModifiedProperties();
    }

    
}
