using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;

[CustomEditor(typeof(AutoType))]
public class AutoTypeEditor : Editor
{
    private ReorderableList list;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("ExtraDelays"),
                true, true, true, true);
        OrganizeLines();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty DefaultPauseSpeed = serializedObject.FindProperty("DefaultPauseSpeed");
        SerializedProperty sound = serializedObject.FindProperty("sound");


        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(DefaultPauseSpeed, new GUIContent("Default Pause Speed"), true);
        EditorGUILayout.PropertyField(sound, new GUIContent("Sound Effect"), true);
        
        list.DoLayoutList();


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
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 180, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("chracter"), GUIContent.none);
        element.FindPropertyRelative("DelayMultiplier").floatValue = EditorGUI.Slider(
            new Rect(rect.x + 180, rect.y, rect.width - 180, EditorGUIUtility.singleLineHeight),
            GUIContent.none, element.FindPropertyRelative("DelayMultiplier").floatValue, 0.5f, 10);
    };


        // List.onChangedCallback
    }
}
