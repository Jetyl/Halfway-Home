using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;

[CustomEditor(typeof(AutoType))]
public class AutoTypeEditor : Editor
{
    private ReorderableList list;
    private ReorderableList listtoo;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("ExtraDelays"),
                true, true, true, true);
        listtoo = new ReorderableList(serializedObject,
                serializedObject.FindProperty("DelayAllowances"),
                true, true, true, true);
        OrganizeLines();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty DefaultPauseSpeed = serializedObject.FindProperty("DefaultPauseSpeed");
        SerializedProperty ScrollEvent = serializedObject.FindProperty("ScrollEvent");


        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(DefaultPauseSpeed, new GUIContent("Characters Per Second"), true);
        EditorGUILayout.PropertyField(ScrollEvent, new GUIContent("Sound Effect"), true);
        
        list.DoLayoutList();
        EditorGUILayout.Space();
        listtoo.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }


    void OrganizeLines()
    {


        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Extra delay characters");
        };

        list.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 80, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("chracter"), GUIContent.none);
        element.FindPropertyRelative("DelayMultiplier").floatValue = EditorGUI.Slider(
            new Rect(rect.x + 100, rect.y, rect.width - 100, EditorGUIUtility.singleLineHeight),
            GUIContent.none, element.FindPropertyRelative("DelayMultiplier").floatValue, 0.5f, 10);
    };

        listtoo.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Delay Only Before These Characters");
        };

        listtoo.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = listtoo.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
            element, GUIContent.none);
    };
        // List.onChangedCallback
    }
}
