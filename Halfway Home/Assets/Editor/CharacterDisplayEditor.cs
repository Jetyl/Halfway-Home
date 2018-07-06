using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;
using System;

[CustomEditor(typeof(CharacterDisplay))]
public class CharacterDisplayEditor : Editor
{
    private ReorderableList list;

    bool showDistance;

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
        SerializedProperty Distances = serializedObject.FindProperty("Distances");
        SerializedProperty FlipOnLeft = serializedObject.FindProperty("FlipOnLeft");

        SerializedProperty visual = serializedObject.FindProperty("visual");
        SerializedProperty BackSprite = serializedObject.FindProperty("BackSprite");
        SerializedProperty SpriteSwitchSpeed = serializedObject.FindProperty("SpriteSwitchSpeed");

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(Character, new GUIContent("Character"), true);
        EditorGUILayout.PropertyField(visual, new GUIContent("Main Sprite"), true);
        EditorGUILayout.PropertyField(BackSprite, new GUIContent("Back Sprite"), true);
        EditorGUILayout.PropertyField(SpriteSwitchSpeed, new GUIContent("Sprite Switch Speed"), true);




        list.DoLayoutList();

        FlipOnLeft.boolValue = EditorGUILayout.ToggleLeft("Flip on Left side", FlipOnLeft.boolValue);

        showDistance = EditorGUILayout.Foldout(showDistance, "Distances");

        if (showDistance)
        {
            //EditorGUILayout.LabelField((Distances == null) + ":");
            Distances.arraySize = Enum.GetValues(typeof(StageDistance)).Length;
            for (var i = 1; i < Enum.GetValues(typeof(StageDistance)).Length; ++i)
            {
                EditorGUILayout.LabelField((StageDistance)i + ":");
                Distances.GetArrayElementAtIndex(i).FindPropertyRelative("Scale").floatValue = EditorGUILayout.Slider("Scale",
                    Distances.GetArrayElementAtIndex(i).FindPropertyRelative("Scale").floatValue, 0.1f, 5);
                Distances.GetArrayElementAtIndex(i).FindPropertyRelative("Offset").floatValue = EditorGUILayout.FloatField("Offset",
                    Distances.GetArrayElementAtIndex(i).FindPropertyRelative("Offset").floatValue);
            }
        }


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
