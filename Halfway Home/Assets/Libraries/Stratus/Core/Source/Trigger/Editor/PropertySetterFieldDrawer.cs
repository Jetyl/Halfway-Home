using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Stratus
{
  [CustomPropertyDrawer(typeof(PropertySetterField))]
  public class PropertySetterFieldDrawer : PropertyDrawer
  {
    private float propertyHeight;
    private float padding = 2f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      return propertyHeight;
      //return property.FindPropertyRelative("propertyHeight").floatValue * EditorGUIUtility.singleLineHeight;

    }
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      propertyHeight = 0;
      var memberProp = property.FindPropertyRelative("property");

      //var setter = (PropertySetterField)property.objectReferenceValue;

      var typeProperty = property.FindPropertyRelative("propertyType");
      var type = (ActionProperty.Types)typeProperty.enumValueIndex;

      label = EditorGUI.BeginProperty(position, label, property);
      EditorGUI.PrefixLabel(position, label);
      Rect contentPosition = position;
      //Rect contentPosition = position;
      //EditorGUI.indentLevel = 0;
      EditorGUI.PropertyField(contentPosition, memberProp);
      float height = EditorGUIUtility.singleLineHeight + padding;      
      contentPosition.height = height;
      propertyHeight = 2f * height;

      // If property has been selected yet
      if (type != ActionProperty.Types.None)
      {
        contentPosition.y += height * 2f;
        GUIContent valueLabel = new GUIContent("Value");
        switch (type)
        {
          case ActionProperty.Types.Integer:
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("intValue"), valueLabel);
            break;
          case ActionProperty.Types.Float:
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("floatValue"), valueLabel);
            break;
          case ActionProperty.Types.Boolean:
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("boolValue"), valueLabel);
            break;
          case ActionProperty.Types.Vector2:
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("vector2Value"), valueLabel);
            break;
          case ActionProperty.Types.Vector3:
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("vector3Value"), valueLabel);
            break;
          case ActionProperty.Types.Vector4:
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("vector4Value"), valueLabel);
            break;
          case ActionProperty.Types.Color:
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("colorValue"), valueLabel);
            break;
          default:
            break;
        }

        var durationProp = property.FindPropertyRelative("duration");
        contentPosition.y += height;
        //contentPosition.width /= 2f;
        EditorGUI.PropertyField(contentPosition, durationProp);

        var toggleprop = property.FindPropertyRelative("toggle");
        contentPosition.y += height;
        //contentPosition.width *= 2f;
        //contentPosition.x += contentPosition.width / 2f;
        EditorGUI.PropertyField(contentPosition, toggleprop);


        propertyHeight += 2f * height;
      }

      EditorGUI.EndProperty();

      if (GUI.changed)
        property.serializedObject.ApplyModifiedProperties();

    }

  }

}