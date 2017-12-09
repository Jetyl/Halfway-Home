using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Stratus
{
  [CustomEditor(typeof(CompositeTrigger))]
  public class CompositeTriggerEditor : TriggerEditor
  {
    CompositeTrigger composite => target as CompositeTrigger;

    private SerializedProperty typeProp => declaredProperties[0];
    private SerializedProperty criteriaProp => declaredProperties[1];
    private SerializedProperty neededProp => declaredProperties[2];
    private SerializedProperty triggersProp => declaredProperties[3];
    private SerializedProperty triggerablesProp => declaredProperties[4];

    protected override void Configure()
    {
      //base.Configure();
    }

    protected override void DrawDeclaredProperties()
    {
      DrawSerializedProperty(typeProp, serializedObject);
      DrawSerializedProperty(criteriaProp, serializedObject);

      if (composite.criteria == CompositeTrigger.Criteria.Subset)
        composite.needed = EditorGUILayout.IntSlider(composite.needed, 1, composite.count);
      //  //DrawSerializedProperty(neededProp, serializedObject);
      
      if (composite.type == CompositeTrigger.Type.Trigger)
      {

        DrawSerializedProperty(triggersProp, serializedObject);      
      }
      else if (composite.type == CompositeTrigger.Type.Triggerable)
      {

        DrawSerializedProperty(triggerablesProp, serializedObject);      
      }

    }

  }

}