using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;

[CustomEditor(typeof(CharacterDisplay))]
public class CharacterDisplayEditor : Editor
{
    private ReorderableList list;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("Poses"),
                true, true, true, true);
        OrganizeLines();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty Character = serializedObject.FindProperty("Character");


        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(Character, new GUIContent("Character"), true);
        

        

        list.DoLayoutList();


        serializedObject.ApplyModifiedProperties();
    }


    void OrganizeLines()
    {


        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Pose List");
        };

        list.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 180, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("Name"), GUIContent.none);
        EditorGUI.PropertyField(new Rect(rect.x + 180, rect.y, rect.width - 180, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("Visual"), GUIContent.none);
    };


        // List.onChangedCallback
    }
}
