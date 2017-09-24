using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;

[CustomEditor(typeof(MapAccessTime))]
public class MapAccessTimeEditor : Editor
{
    private ReorderableList list;

    bool ShowTimesClosed;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("ClosedTimeContainer"),
                true, true, true, true);
        OrganizeLines();
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty LimitedDailyAccess = serializedObject.FindProperty("LimitedDailyAccess");
        SerializedProperty AccessPoint = serializedObject.FindProperty("AccessPoint");
        SerializedProperty TimesCanVisit = serializedObject.FindProperty("TimesCanVisit");
        

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(LimitedDailyAccess, new GUIContent("Limited Daily Access?"), true);
        if(LimitedDailyAccess.boolValue == true)
        {
            EditorGUILayout.PropertyField(AccessPoint, new GUIContent("Access Point"), true);
            EditorGUILayout.PropertyField(TimesCanVisit, new GUIContent("Times Can Visit"), true);
        }

        ShowTimesClosed = EditorGUILayout.Foldout(ShowTimesClosed, "Show times Closed");

        if(ShowTimesClosed)
            list.DoLayoutList();


        serializedObject.ApplyModifiedProperties();
    }


    void OrganizeLines()
    {


        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Times Closed");
        };

        list.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        rect.height += EditorGUIUtility.singleLineHeight * 5;
        list.elementHeight = EditorGUIUtility.singleLineHeight * 5;
        EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
            "Day: " + element.FindPropertyRelative("Day").intValue
            + " from: " + element.FindPropertyRelative("starttime").intValue + 
            " to: " + element.FindPropertyRelative("endTime").intValue);
        element.FindPropertyRelative("Day").intValue = EditorGUI.IntSlider(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 2, rect.width, EditorGUIUtility.singleLineHeight), "Day",
            element.FindPropertyRelative("Day").intValue, 0, 7);
        element.FindPropertyRelative("starttime").intValue = EditorGUI.IntSlider(new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight *2) + 4, rect.width, EditorGUIUtility.singleLineHeight), "Start",
            element.FindPropertyRelative("starttime").intValue, 0, 23);
        element.FindPropertyRelative("endTime").intValue = EditorGUI.IntSlider(new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight * 3) + 6, rect.width, EditorGUIUtility.singleLineHeight), "End",
            element.FindPropertyRelative("endTime").intValue, element.FindPropertyRelative("starttime").intValue, 23);
        rect = new Rect(rect.x, rect.y, rect.width, rect.height);
    };


        // List.onChangedCallback
    }
}
