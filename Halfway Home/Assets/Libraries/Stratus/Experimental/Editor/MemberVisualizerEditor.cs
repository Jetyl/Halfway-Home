using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Stratus
{
  [CustomEditor(typeof(MemberVisualizer))]
  public class MemberVisualizerEditor : BaseEditor<MemberVisualizer>
  {
    private SerializedProperty renderSettingsList => propertyMap["renderSettingsList"];

    protected override void PostEnable()
    {
      propertyConstraints.Add(renderSettingsList, ()=>DontDraw);
      drawFunctions.Add(DrawRenderingSettings);
    }    

    private void DrawRenderingSettings()
    {
      EditorGUILayout.LabelField("Rendering Settings", StratusEditorStyles.header);
      //foreach (var gameObject in declaredTarget.sel)
      //{
      //  EditorGUILayout.LabelField(gameObject.n)
      //}
      for (int i = 0; i < renderSettingsList.arraySize; ++i)
      {
        SerializedProperty rs = renderSettingsList.GetArrayElementAtIndex(i);
        EditorGUILayout.LabelField(declaredTarget.renderSettingsList[i].gameObject.name, labelStyle);
        rs.serializedObject.Update();

        SerializedProperty offsetProperty = rs.FindPropertyRelative("offset");
        SerializedProperty fontProperty = rs.FindPropertyRelative("fontSize");

        EditorGUI.BeginChangeCheck();
        {
          EditorGUILayout.PropertyField(offsetProperty);
          EditorGUILayout.PropertyField(fontProperty);
        }
        if (EditorGUI.EndChangeCheck())
        {
          rs.serializedObject.ApplyModifiedProperties();
        }
      }
    }

  }

}