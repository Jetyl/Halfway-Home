using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;
using LitJson;

[CustomEditor(typeof(FontDisplay))]
public class FontDisplayEditor : Editor
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

        SerializedProperty Speakers = serializedObject.FindProperty("Speakers");
        SerializedProperty Characters = serializedObject.FindProperty("Characters");
        SerializedProperty Colors = serializedObject.FindProperty("Fonts");

        SerializedProperty Box = serializedObject.FindProperty("NoSpeakerFont");
        SerializedProperty Random = serializedObject.FindProperty("RandomSpeakerFont");


        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(Box, new GUIContent("Description Font"));

        EditorGUILayout.Space();

        Characters.arraySize = AmountOfCharacters + 1;
        Colors.arraySize = AmountOfCharacters + 1;

        Characters.GetArrayElementAtIndex(0).stringValue = "#PlayerName";

        EditorGUILayout.PropertyField(Colors.GetArrayElementAtIndex(0), new GUIContent("Player Font"));

        for (int i = 0; i < AmountOfCharacters; ++i)
        {
            Characters.GetArrayElementAtIndex(i + 1).stringValue = Names[i];
            EditorGUILayout.PropertyField(Colors.GetArrayElementAtIndex(i + 1), new GUIContent(Names[i] + " Font"));

        }

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(Random, new GUIContent("Random Person Font"));

        serializedObject.ApplyModifiedProperties();
    }


}
