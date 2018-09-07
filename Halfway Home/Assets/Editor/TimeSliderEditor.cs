using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;
using System;
using HalfwayHome;

[CustomEditor(typeof(TimeSlider))]
public class TimeSliderEditor : Editor
{

    Room CurrentRoom;

    List<RoomStrings> RoomNames;

    private void OnEnable()
    {
        RoomNames = new List<RoomStrings>();

        SerializedProperty RoomStrings = serializedObject.FindProperty("RoomStrings");

        for (var i = 0; i < Enum.GetValues(typeof(Room)).Length; ++i)
        {
            var rom = new RoomStrings();
            rom.location = (Room)i;
            rom.text = "";
            RoomNames.Add(rom);
        }

        for(int i = 0; i < RoomStrings.arraySize; ++i)
        {
            Room lol = (Room)RoomStrings.GetArrayElementAtIndex(i).FindPropertyRelative("location").enumValueIndex;
            string txt = RoomStrings.GetArrayElementAtIndex(i).FindPropertyRelative("text").stringValue;
            
            for(int j = 0; j < RoomNames.Count; ++j)
            {
                var deet = RoomNames[j];
                if (deet.location == lol)
                    deet.text = txt;
                RoomNames[j] = deet;
            }

        }


    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        SerializedProperty DepressionDialator = serializedObject.FindProperty("DepressionDialator");
        SerializedProperty RoomText = serializedObject.FindProperty("RoomText");
        SerializedProperty RoomDesciption = serializedObject.FindProperty("RoomDesciption");
        SerializedProperty TimeText = serializedObject.FindProperty("TimeText");
        SerializedProperty TimeDescription = serializedObject.FindProperty("TimeDescription");
        SerializedProperty DepressionEffectorColor = serializedObject.FindProperty("DepressionEffectorColor");
        SerializedProperty SceneText = serializedObject.FindProperty("SceneText");
        SerializedProperty SceneDescription = serializedObject.FindProperty("SceneDescription");
        SerializedProperty UnknownSceneTag = serializedObject.FindProperty("UnknownSceneTag");
        SerializedProperty UnknownSceneColor = serializedObject.FindProperty("UnknownSceneColor");
        SerializedProperty VisitedSceneColor = serializedObject.FindProperty("VisitedSceneColor");
        SerializedProperty RoomStrings = serializedObject.FindProperty("RoomStrings");

        EditorGUILayout.Space();


        EditorGUILayout.PropertyField(RoomText, new GUIContent("Room Text"), true);
        EditorGUILayout.PropertyField(RoomDesciption, new GUIContent("Room Description"), true);
        
        CurrentRoom = (Room)EditorGUILayout.EnumPopup(new GUIContent("Room"), CurrentRoom);

        RoomStrings.arraySize = Enum.GetValues(typeof(Room)).Length;
        
        for (int i = 0; i < RoomNames.Count; ++i)
        {
            var name = RoomNames[i];
            if (CurrentRoom == name.location)
            {
                name.text = EditorGUILayout.TextField("Room Name:", name.text);
                
                RoomStrings.GetArrayElementAtIndex(i).FindPropertyRelative("location").enumValueIndex = (int)name.location;
                RoomStrings.GetArrayElementAtIndex(i).FindPropertyRelative("text").stringValue = name.text;
            }

            RoomNames[i] = name;
        }


        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(TimeText, new GUIContent("Time Text"), true);
        EditorGUILayout.PropertyField(TimeDescription, new GUIContent("Time Description"), true);

        EditorGUILayout.PropertyField(DepressionDialator, new GUIContent("Depression Dialator"), true);
        EditorGUILayout.PropertyField(DepressionEffectorColor, new GUIContent("Depression Effect Color"), true);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(SceneText, new GUIContent("Scene Text"), true);
        EditorGUILayout.PropertyField(SceneDescription, new GUIContent("Scene Description"), true);
        EditorGUILayout.PropertyField(UnknownSceneTag, new GUIContent("Unknown Scene Tag"), true);
        EditorGUILayout.PropertyField(UnknownSceneColor, new GUIContent("Unknown Scene Color"), true);
        EditorGUILayout.PropertyField(VisitedSceneColor, new GUIContent("Visited Scene Color"), true);


        serializedObject.ApplyModifiedProperties();
    }
}
