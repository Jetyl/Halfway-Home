using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;

namespace HalfwayHome
{

  [CustomEditor(typeof(MapAccessTime))]
  public class MapAccessTimeEditor : Editor
  {
    private ReorderableList list;

    bool ShowTimesClosed;
    bool ShowModules;

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

      SerializedProperty ManualAccess = serializedObject.FindProperty("ManualAccess");
      SerializedProperty LimitedDailyAccess = serializedObject.FindProperty("LimitedDailyAccess");
      SerializedProperty AccessPoint = serializedObject.FindProperty("AccessPoint");
      SerializedProperty TimesCanVisit = serializedObject.FindProperty("VisitMulitplier");
      SerializedProperty HourVisited = serializedObject.FindProperty("HourVisited");
      SerializedProperty DayVisited = serializedObject.FindProperty("DayVisited");

      SerializedProperty FatigueCloseLimit = serializedObject.FindProperty("FatigueCloseLimit");
      SerializedProperty StressCloseLimit = serializedObject.FindProperty("StressCloseLimit");
      SerializedProperty DepressionCloseLimit = serializedObject.FindProperty("DepressionCloseLimit");

      
      SerializedProperty FatigueCloseTooltip = serializedObject.FindProperty("FatigueCloseTooltip");
      SerializedProperty StressCloseTooltip = serializedObject.FindProperty("StressCloseTooltip");
      SerializedProperty DepressionCloseTooltip = serializedObject.FindProperty("DepressionCloseTooltip");
            
      SerializedProperty Modules = serializedObject.FindProperty("Modularity");
      
      SerializedProperty DynamicTooltip = serializedObject.FindProperty("DynamicTooltip");
      SerializedProperty DailyAccessTooltip = serializedObject.FindProperty("DailyAccessTooltip");


      EditorGUILayout.Space();

      EditorGUILayout.IntSlider(FatigueCloseLimit, 0, 100, "Fatigue Percent Close");      
      EditorGUILayout.PropertyField(FatigueCloseTooltip, new GUIContent("Fatigue Closed Tooltip"), true);
      EditorGUILayout.IntSlider(StressCloseLimit, 0, 100, "Stress Percent Close");
      EditorGUILayout.PropertyField(StressCloseTooltip, new GUIContent("Stress Closed Tooltip"), true);
      EditorGUILayout.IntSlider(DepressionCloseLimit, 0, 100, "Depression Percent Close");
      EditorGUILayout.PropertyField(DepressionCloseTooltip, new GUIContent("Depression Closed Tooltip"), true);
            
      EditorGUILayout.PropertyField(Modules, new GUIContent("Modules Stat closes"), true);
        
      EditorGUILayout.Space();

      EditorGUILayout.PropertyField(ManualAccess, new GUIContent("Manual Turn Off Flag"), true);
      EditorGUILayout.PropertyField(DynamicTooltip, new GUIContent("Manual Turn Tooltip Key"), true);
      EditorGUILayout.Space();

      EditorGUILayout.PropertyField(LimitedDailyAccess, new GUIContent("Limited Daily Access?"), true);
      if (LimitedDailyAccess.boolValue == true)
      {
        EditorGUILayout.PropertyField(DailyAccessTooltip, new GUIContent("Close Time Tooltip"), true);
        EditorGUILayout.PropertyField(TimesCanVisit, new GUIContent("Times Can Visit"), true);

        EditorGUILayout.PropertyField(AccessPoint, new GUIContent("Progress: Times visisted"), true);
        EditorGUILayout.PropertyField(HourVisited, new GUIContent("Progress: hour last visisted"), true);
        EditorGUILayout.PropertyField(DayVisited, new GUIContent("Progress: day lasr visisted"), true);
      }

      ShowTimesClosed = EditorGUILayout.Foldout(ShowTimesClosed, "Show times Closed");

      if (ShowTimesClosed)
        list.DoLayoutList();


      serializedObject.ApplyModifiedProperties();
    }

    string GetTime(int time)
    {
        while(time > 24)
        {
            time -= 24;
        }

        string Txt = "";

            if (time < 12)
            {
                if (time == 0)
                    Txt = "12:00 AM";
                else
                    Txt = time + ":00 AM";

            }
            else
            {
                if (time == 12)
                    Txt = "12:00 PM";
                else
                    Txt = (time - 12) + ":00 PM";
            }

            return Txt;
          
    }

    void OrganizeLines()
    {


      list.drawHeaderCallback = (Rect rect) =>
      {
        EditorGUI.LabelField(rect, "Times Closed");
      };

      list.drawElementCallback =
  (Rect rect, int index, bool isActive, bool isFocused) =>
  {
    var element = list.serializedProperty.GetArrayElementAtIndex(index);
    rect.y += 2;
    rect.height += EditorGUIUtility.singleLineHeight * 6;
      list.elementHeight = EditorGUIUtility.singleLineHeight * 7;

      string day = "Day: " + element.FindPropertyRelative("Day").intValue;
      string start = " from: " + GetTime(element.FindPropertyRelative("starttime").intValue);
      string end = " to: " + GetTime(element.FindPropertyRelative("endTime").intValue);
      

      EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
          day + start + end);
    element.FindPropertyRelative("ProgressLocked").boolValue = EditorGUI.ToggleLeft(new Rect(rect.x + 250, rect.y, rect.width - 250, EditorGUIUtility.singleLineHeight), "Progress Locked",
          element.FindPropertyRelative("ProgressLocked").boolValue);
    element.FindPropertyRelative("Day").intValue = EditorGUI.IntSlider(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 2, rect.width, EditorGUIUtility.singleLineHeight), "Day",
          element.FindPropertyRelative("Day").intValue, 0, 7);
    element.FindPropertyRelative("starttime").intValue = EditorGUI.IntSlider(new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight * 2) + 4, rect.width, EditorGUIUtility.singleLineHeight), "Start",
          element.FindPropertyRelative("starttime").intValue, 0, 23);
    element.FindPropertyRelative("endTime").intValue = EditorGUI.IntSlider(new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight * 3) + 6, rect.width, EditorGUIUtility.singleLineHeight), "End",
          element.FindPropertyRelative("endTime").intValue, element.FindPropertyRelative("starttime").intValue, 23);

    if (element.FindPropertyRelative("ProgressLocked").boolValue == true)
    {
      rect.height += EditorGUIUtility.singleLineHeight * 6;
      //list.elementHeight = EditorGUIUtility.singleLineHeight * 6;
      element.FindPropertyRelative("ProgressKey").stringValue = EditorGUI.TextField(new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight * 4) + 8, rect.width, EditorGUIUtility.singleLineHeight), "Progress Flag",
            element.FindPropertyRelative("ProgressKey").stringValue);
    }

      element.FindPropertyRelative("ToolTipInfo").stringValue = EditorGUI.TextField(
          new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight * 5) + 10,
          rect.width, EditorGUIUtility.singleLineHeight),
          "Reason Closed: ", element.FindPropertyRelative("ToolTipInfo").stringValue);

    rect = new Rect(rect.x, rect.y, rect.width, rect.height);
  };


      // List.onChangedCallback
    }
  }

}