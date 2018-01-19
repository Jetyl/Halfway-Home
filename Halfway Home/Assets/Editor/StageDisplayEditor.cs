using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;
using System;

[CustomEditor(typeof(StageDisplay))]
public class StageDisplayEditor : Editor
{

    bool showbackdrops;
    bool showSPbackdrops;

    private void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty Backdrop = serializedObject.FindProperty("Backdrop");
        SerializedProperty SpecialBackdrop = serializedObject.FindProperty("SpecialBackdrops");
        SerializedProperty FrontCurtain = serializedObject.FindProperty("FrontCurtain");
        SerializedProperty BackCuratin = serializedObject.FindProperty("BackCuratin");
        

        SerializedProperty StartingRoom = serializedObject.FindProperty("StartingRoom");
        SerializedProperty BackgroundFadeTime = serializedObject.FindProperty("BackgroundFadeTime");

        EditorGUILayout.Space();

        showbackdrops = EditorGUILayout.Foldout(showbackdrops, new GUIContent("Backdrops"));

        if(showbackdrops)
        {
            Backdrop.arraySize = Enum.GetValues(typeof(Room)).Length;

            for (var i = 0; i < Backdrop.arraySize; ++i)
            {
                Backdrop.GetArrayElementAtIndex(i).FindPropertyRelative("ID").enumValueIndex = i;

                EditorGUILayout.PropertyField(Backdrop.GetArrayElementAtIndex(i).
                    FindPropertyRelative("Backdrop"), new GUIContent( (Room)i + " Backdrop"), true);
            }

            EditorGUILayout.Space();
            //EditorGUILayout.PropertyField(Backdrop, new GUIContent("Backdrop"), true);
        }

        showSPbackdrops = EditorGUILayout.Foldout(showSPbackdrops, new GUIContent("Special Backdrops"));

        if (showSPbackdrops)
        {
            SpecialBackdrop.arraySize = EditorGUILayout.DelayedIntField("Amount", SpecialBackdrop.arraySize);

            for (var i = 0; i < SpecialBackdrop.arraySize; ++i)
            {
                SpecialBackdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Tag").stringValue = EditorGUILayout.TextField("Backdrop Tag",
                    SpecialBackdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Tag").stringValue);

                EditorGUILayout.PropertyField(SpecialBackdrop.GetArrayElementAtIndex(i).
                    FindPropertyRelative("Backdrop"), new GUIContent(SpecialBackdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Tag").stringValue + " Backdrop"), true);
            }

            EditorGUILayout.Space();
            //EditorGUILayout.PropertyField(Backdrop, new GUIContent("Backdrop"), true);
        }

        EditorGUILayout.PropertyField(StartingRoom, new GUIContent("Starting Room"), true);
        EditorGUILayout.PropertyField(BackgroundFadeTime, new GUIContent("Background Fade Time"), true);

        EditorGUILayout.PropertyField(FrontCurtain, new GUIContent("Front Curtain"), true);
        EditorGUILayout.PropertyField(BackCuratin, new GUIContent("BackCuratin"), true);

        
        

        serializedObject.ApplyModifiedProperties();
    }
    
}
