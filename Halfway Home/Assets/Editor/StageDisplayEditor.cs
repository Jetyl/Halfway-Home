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

    List<bool> BGFoldouts;
    List<bool> SPFoldouts;

    private void OnEnable()
    {
        BGFoldouts = new List<bool>();

        for(int i= 0; i < Enum.GetValues(typeof(Room)).Length; ++i)
        {
            BGFoldouts.Add(false);
        }

        int amount = serializedObject.FindProperty("SpecialBackdrops").arraySize;

        SPFoldouts = new List<bool>();

        for (int i = 0; i < amount; ++i)
        {
            SPFoldouts.Add(false);
        }


    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty Backdrop = serializedObject.FindProperty("Backdrop");
        SerializedProperty SpecialBackdrop = serializedObject.FindProperty("SpecialBackdrops");
        SerializedProperty FrontCurtain = serializedObject.FindProperty("FrontCurtain");
        SerializedProperty BackCuratin = serializedObject.FindProperty("BackCuratin");

        SerializedProperty WipeCurtain = serializedObject.FindProperty("WipeCurtain");
        SerializedProperty EyeCurtain = serializedObject.FindProperty("EyeCurtain");
        SerializedProperty CurtainDefault = serializedObject.FindProperty("CurtainDefault");

        SerializedProperty StartingRoom = serializedObject.FindProperty("StartingRoom");
        SerializedProperty BackgroundFadeTime = serializedObject.FindProperty("BackgroundFadeTime");


        SerializedProperty DayTimeStart = serializedObject.FindProperty("DayTimeStart");
        SerializedProperty DayTimeEnd = serializedObject.FindProperty("DayTimeEnd");

        EditorGUILayout.Space();

        showbackdrops = EditorGUILayout.Foldout(showbackdrops, new GUIContent("Backdrops"));

        if(showbackdrops)
        {
            Backdrop.arraySize = Enum.GetValues(typeof(Room)).Length;

            for (var i = 0; i < Backdrop.arraySize; ++i)
            {
                BGFoldouts[i] = EditorGUILayout.Foldout(BGFoldouts[i], (Room)i + " Info");

                if(BGFoldouts[i])
                {
                    Backdrop.GetArrayElementAtIndex(i).FindPropertyRelative("ID").enumValueIndex = i;

                    var back = Backdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Backdrops");
                    back.arraySize = 2;
                    var ambience = Backdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Ambience");
                    ambience.arraySize = 2;

                    EditorGUILayout.PropertyField(back.GetArrayElementAtIndex(0),
                        new GUIContent((Room)i + " Backdrop, day"), true);
                    EditorGUILayout.PropertyField(ambience.GetArrayElementAtIndex(0),
                        new GUIContent((Room)i + " Ambience, day"), true);
                    EditorGUILayout.PropertyField(back.GetArrayElementAtIndex(1),
                        new GUIContent((Room)i + " Backdrop, night"), true);
                    EditorGUILayout.PropertyField(ambience.GetArrayElementAtIndex(1),
                        new GUIContent((Room)i + " Ambience, night"), true);

                    EditorGUILayout.PropertyField(Backdrop.GetArrayElementAtIndex(i).FindPropertyRelative("MusicTrack"),
                        new GUIContent((Room)i + " Music"), true);
                    
                    EditorGUILayout.PropertyField(Backdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Bank"),
                        new GUIContent((Room)i + " Bank"), true);
                }

                
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
                SPFoldouts[i] = EditorGUILayout.Foldout(SPFoldouts[i], SpecialBackdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Tag").stringValue + " Info");

                if(SPFoldouts[i])
                {
                    SpecialBackdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Tag").stringValue = EditorGUILayout.TextField("Backdrop Tag",
                    SpecialBackdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Tag").stringValue);

                    var spec = SpecialBackdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Backdrops");
                    spec.arraySize = 1;

                    EditorGUILayout.PropertyField(spec.GetArrayElementAtIndex(0),
                        new GUIContent(SpecialBackdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Tag").stringValue + " Backdrop"), true);

                    EditorGUILayout.PropertyField(SpecialBackdrop.GetArrayElementAtIndex(i).FindPropertyRelative("MusicTrack"),
                        new GUIContent((Room)i + " Music"), true);
                    var amb = SpecialBackdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Ambience");
                    amb.arraySize = 1;
                    EditorGUILayout.PropertyField(amb.GetArrayElementAtIndex(0),
                        new GUIContent((Room)i + " Ambience"), true);
                    EditorGUILayout.PropertyField(SpecialBackdrop.GetArrayElementAtIndex(i).FindPropertyRelative("Bank"),
                        new GUIContent((Room)i + " Bank"), true);
                }

                
            }

            EditorGUILayout.Space();
            //EditorGUILayout.PropertyField(Backdrop, new GUIContent("Backdrop"), true);
        }

        EditorGUILayout.PropertyField(StartingRoom, new GUIContent("Starting Room"), true);
        EditorGUILayout.PropertyField(BackgroundFadeTime, new GUIContent("Background Fade Time"), true);

        EditorGUILayout.PropertyField(FrontCurtain, new GUIContent("Front Curtain"), true);
        EditorGUILayout.PropertyField(BackCuratin, new GUIContent("BackCuratin"), true);

        EditorGUILayout.PropertyField(WipeCurtain, new GUIContent("Wipe Transition Curtain"), true);
        EditorGUILayout.PropertyField(EyeCurtain, new GUIContent("Eye Transition Curtain"), true);
        EditorGUILayout.PropertyField(CurtainDefault, new GUIContent("Curtain Default Sprite"), true);


        EditorGUILayout.PropertyField(DayTimeStart, new GUIContent("Daytime Start:"), true);
        EditorGUILayout.PropertyField(DayTimeEnd, new GUIContent("Daytime End:"), true);

        serializedObject.ApplyModifiedProperties();
    }
    
}
