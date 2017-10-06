using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;

[CustomEditor(typeof(GameStartUp))]
public class GameStartUpEditor : Editor
{
    private ReorderableList list;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("DebugValues"),
                true, true, true, true);
        OrganizeLines();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty Timeline = serializedObject.FindProperty("Timeline");
        SerializedProperty StartDay = serializedObject.FindProperty("StartDay");
        SerializedProperty StartHour = serializedObject.FindProperty("StartHour");


        SerializedProperty DebugMode = serializedObject.FindProperty("DebugMode");
        SerializedProperty DebugDay = serializedObject.FindProperty("DebugDay");
        SerializedProperty DebugHour = serializedObject.FindProperty("DebugHour");
        SerializedProperty DebugWeek = serializedObject.FindProperty("DebugWeek");

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(Timeline, new GUIContent("Timeline"), true);
        EditorGUILayout.IntSlider(StartDay, 1, 7, new GUIContent("Start Day"));
        EditorGUILayout.IntSlider(StartHour, 1, 7, new GUIContent("Start Hour"));

        EditorGUILayout.PropertyField(DebugMode, new GUIContent("Timeline"), true);

        if(DebugMode.boolValue == true)
        {

            EditorGUILayout.IntSlider(DebugDay, 1, 7, new GUIContent("Debug Day"));
            EditorGUILayout.IntSlider(DebugHour, 1, 7, new GUIContent("Debug Hour"));
            EditorGUILayout.IntSlider(DebugWeek, 1, 7, new GUIContent("debug Week"));
            list.DoLayoutList();
        }

        //list.DoLayoutList();


        serializedObject.ApplyModifiedProperties();
    }


    void OrganizeLines()
    {


        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Debug Values");
        };

        list.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        if((PointTypes)element.FindPropertyRelative("TypeID").enumValueIndex != PointTypes.None)
        {
            rect.height += EditorGUIUtility.singleLineHeight * 2.2f;
            list.elementHeight = EditorGUIUtility.singleLineHeight * 2.2f;
        }
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 2),
            element, GUIContent.none);
    };


        // List.onChangedCallback
    }
}
