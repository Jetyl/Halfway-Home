using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;
using System;

[CustomEditor(typeof(StageDisplay))]
public class StageDisplayEditor : Editor
{
    private ReorderableList list;

    bool showbackdrops;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("CastList"),
                true, true, true, true);
        OrganizeLines();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty Backdrop = serializedObject.FindProperty("Backdrop");
        SerializedProperty FrontCurtain = serializedObject.FindProperty("FrontCurtain");
        SerializedProperty BackCuratin = serializedObject.FindProperty("BackCuratin");

        SerializedProperty LeftSpot = serializedObject.FindProperty("LeftSpot");
        SerializedProperty RightSpot = serializedObject.FindProperty("RightSpot");
        SerializedProperty Varience = serializedObject.FindProperty("Varience");

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

        EditorGUILayout.PropertyField(StartingRoom, new GUIContent("Starting Room"), true);
        EditorGUILayout.PropertyField(BackgroundFadeTime, new GUIContent("Background Fade Time"), true);

        EditorGUILayout.PropertyField(FrontCurtain, new GUIContent("Front Curtain"), true);
        EditorGUILayout.PropertyField(BackCuratin, new GUIContent("BackCuratin"), true);

        EditorGUILayout.PropertyField(LeftSpot, new GUIContent("Left Spot"), true);
        EditorGUILayout.PropertyField(RightSpot, new GUIContent("Right Spot"), true);
        EditorGUILayout.PropertyField(Varience, new GUIContent("Varience"), true);



        list.DoLayoutList();
        

        serializedObject.ApplyModifiedProperties();
    }


    void OrganizeLines()
    {


        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Cast List");
        };

        list.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 280, EditorGUIUtility.singleLineHeight),
            element, GUIContent.none);
        EditorGUI.PropertyField(new Rect(rect.x + 280, rect.y, rect.width - 280, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("Actor"), GUIContent.none);
    };


        // List.onChangedCallback
    }
}
