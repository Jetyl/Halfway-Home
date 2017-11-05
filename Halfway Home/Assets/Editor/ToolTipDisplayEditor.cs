using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;

[CustomEditor(typeof(ToolTipDisplay))]
public class ToolTipDisplayEditor : Editor
{
    private ReorderableList list;

    bool wellbeing;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("Tips"),
                true, true, true, true);
        OrganizeLines();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty Wellbeing = serializedObject.FindProperty("Wellbeing");
        SerializedProperty WellnessStat = serializedObject.FindProperty("WellnessStat");
        SerializedProperty SocialStat = serializedObject.FindProperty("SocialStat");
        SerializedProperty ModifiedStatText = serializedObject.FindProperty("ModifiedStatText");
        SerializedProperty ModifedStatColor = serializedObject.FindProperty("ModifedStatColor");
        SerializedProperty Debug = serializedObject.FindProperty("Debug");


        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(Wellbeing, new GUIContent("Is it a Wellbeing Stat?"), true);

        if(Wellbeing.boolValue)
            EditorGUILayout.PropertyField(WellnessStat, new GUIContent("Wellness Stat"), true);
        else
        {
            EditorGUILayout.PropertyField(SocialStat, new GUIContent("Social Stat"), true);
            EditorGUILayout.PropertyField(ModifiedStatText, new GUIContent("Modified Stat Text"), true);
            EditorGUILayout.PropertyField(ModifedStatColor, new GUIContent("Modifed Stat Color"), true);
        }

        wellbeing = Wellbeing.boolValue;

        list.DoLayoutList();


        EditorGUILayout.PropertyField(Debug, new GUIContent("Debug Stats?"), true);

        serializedObject.ApplyModifiedProperties();
    }


    void OrganizeLines()
    {


        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Tips to display");
        };

        list.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        rect.height += EditorGUIUtility.singleLineHeight * 2.5f;
        list.elementHeight = EditorGUIUtility.singleLineHeight * 2.5f;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 180, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("color"), GUIContent.none);
        if(wellbeing)
        {
            element.FindPropertyRelative("Value").intValue = EditorGUI.IntSlider(
            new Rect(rect.x + 180, rect.y, rect.width - 180, EditorGUIUtility.singleLineHeight),
            GUIContent.none, element.FindPropertyRelative("Value").intValue, 0, 100);
        }
        else
        {
            element.FindPropertyRelative("Value").intValue = EditorGUI.IntSlider(
            new Rect(rect.x + 180, rect.y, rect.width - 180, EditorGUIUtility.singleLineHeight),
            GUIContent.none, element.FindPropertyRelative("Value").intValue, 0, 5);
        }
        EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 2, rect.width, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("info"), new GUIContent("Tip"));
    };


        // List.onChangedCallback
    }
}
