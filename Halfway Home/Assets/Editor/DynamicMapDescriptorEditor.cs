using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;
using System;

namespace HalfwayHome
{

    [CustomEditor(typeof(DynamicMapDescriptor))]
    public class DynamicMapDescriptorEditor : Editor
    {
        private Dictionary<Room, ReorderableList> listList;

        Room CheckRoom;

        bool ShowTimesClosed;
        

        private void OnEnable()
        {
            listList = new Dictionary<Room, ReorderableList>();

            for (var i = 0; i < Enum.GetValues(typeof(Room)).Length; ++i)
            {
                var list = new ReorderableList(serializedObject,
                    serializedObject.FindProperty("Descriptors").GetArrayElementAtIndex(i).FindPropertyRelative("Conditions"),
                    true, true, true, true);

                listList.Add((Room)i, list);

            }

            
            OrganizeLines();

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty RoomText = serializedObject.FindProperty("RoomText");
            SerializedProperty EffectText = serializedObject.FindProperty("EffectText");

            
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(RoomText, new GUIContent("Room Text"), true);
            EditorGUILayout.PropertyField(EffectText, new GUIContent("Effect Text"), true);
            

            ShowTimesClosed = EditorGUILayout.Foldout(ShowTimesClosed, "Show Tool Tip Info");

            if (ShowTimesClosed)
            {
                CheckRoom = (Room)EditorGUILayout.EnumPopup("Room to check:", CheckRoom);

                SerializedProperty name = serializedObject.FindProperty("Descriptors").GetArrayElementAtIndex((int)CheckRoom).FindPropertyRelative("name");
                SerializedProperty roomba = serializedObject.FindProperty("Descriptors").GetArrayElementAtIndex((int)CheckRoom).FindPropertyRelative("Room");
                roomba.enumValueIndex = (int)CheckRoom;
                
                SerializedProperty defaultCondition = serializedObject.FindProperty("Descriptors").GetArrayElementAtIndex((int)CheckRoom).FindPropertyRelative("DefaultCondition");

                EditorGUILayout.PropertyField(name, new GUIContent("Room Name"), true);
                EditorGUILayout.PropertyField(defaultCondition.FindPropertyRelative("DescriptorText"), new GUIContent("Default ToolTip"), true);

                listList[CheckRoom].DoLayoutList();
            }


            serializedObject.ApplyModifiedProperties();
        }
        

        void OrganizeLines()
        {

            foreach(var list in listList.Values)
            {
                list.drawHeaderCallback = (Rect rect) =>
                {
                    EditorGUI.LabelField(rect, "Tool Tip Info");
                };

                list.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                rect.height += EditorGUIUtility.singleLineHeight * 3;
                
                    

                element.FindPropertyRelative("DescriptorText").stringValue = EditorGUI.TextField(
                    new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight),
                    "Descriptor Text", element.FindPropertyRelative("DescriptorText").stringValue);

                element.FindPropertyRelative("Conditions").arraySize = EditorGUI.DelayedIntField(
                    new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight) + 8, rect.width, EditorGUIUtility.singleLineHeight),
                    "Conditions", element.FindPropertyRelative("Conditions").arraySize);

                int j = 0;

                for(int i = 0; i < element.FindPropertyRelative("Conditions").arraySize; ++i)
                {
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight * 3) + 
                        (EditorGUIUtility.singleLineHeight * i * 2) + j, rect.width, EditorGUIUtility.singleLineHeight),
                      element.FindPropertyRelative("Conditions").GetArrayElementAtIndex(i), new GUIContent("Condition"));
                    j += 20;
                }
                
                
                
                rect = new Rect(rect.x, rect.y, rect.width, rect.height);
            };

                list.elementHeightCallback = (index) =>
                {
                    var element = list.serializedProperty.GetArrayElementAtIndex(index);
                    return EditorGUIUtility.singleLineHeight * (4 + (element.FindPropertyRelative("Conditions").arraySize * 3.25f));
                };


            }


            


            // List.onChangedCallback
        }
    }

}