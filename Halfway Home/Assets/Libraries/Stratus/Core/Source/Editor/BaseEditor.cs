using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using Rotorz.ReorderableList;

namespace Stratus
{
  /// <summary>
  /// Base editor for all Stratus components
  /// </summary>
  public abstract class BaseEditor : Editor
  {
    protected class PropertyConstraintMap : Dictionary<SerializedProperty, Func<bool>> {}

    protected PropertyConstraintMap propertyConstraints { get; set; } = new PropertyConstraintMap();
    protected Dictionary<string, SerializedProperty> propertyMap { get; set; } = new Dictionary<string, SerializedProperty>();

    protected SerializedProperty[] baseProperties, declaredProperties;
    protected virtual void InitializeBaseEditor() { }
    protected virtual void Configure() { }

    //------------------------------------------------------------------------/
    // Messages
    //------------------------------------------------------------------------/
    private void OnEnable()
    {
      baseProperties = GetSerializedProperties(serializedObject, target.GetType().BaseType);
      foreach (var prop in baseProperties)
        propertyMap.Add(prop.name, prop);

      declaredProperties = GetSerializedProperties(serializedObject, target.GetType());
      foreach (var prop in declaredProperties)
        propertyMap.Add(prop.name, prop);

      InitializeBaseEditor();
      Configure();
    }

    public override void OnInspectorGUI()
    {
      serializedObject.Update();

      // Draw base
      EditorGUILayout.BeginVertical(EditorStyles.helpBox);
      DrawBaseProperties();
      EditorGUILayout.EndVertical();

      // Draw declared
      if (declaredProperties.Length > 0)
      {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawDeclaredProperties();
        EditorGUILayout.EndVertical();
      }
      
      //if (GUI.changed)
      //  Undo.RecordObject(target, );
      //  //EditorUtility.SetDirty(target);

    }

    //------------------------------------------------------------------------/
    // Helpers
    //------------------------------------------------------------------------/
    /// <summary>
    /// Gets all the serialized property for the given Unity Object of a specified type
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static SerializedProperty[] GetSerializedProperties(SerializedObject serializedObject, Type type)
    {
      FieldInfo[] propInfo = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
      SerializedProperty[] properties = new SerializedProperty[propInfo.Length];

      for (int a = 0; a < properties.Length; a++)
      {
        properties[a] = serializedObject.FindProperty(propInfo[a].Name);

        if (properties[a] == null)
        {
          Trace.Script("Could not find property: " + propInfo[a].Name);
        }
      }

      return properties;
    }

    /// <summary>
    /// Draws a property, saving it if it changes
    /// </summary>
    /// <param name="prop"></param>

    public static bool DrawSerializedProperty(SerializedProperty prop, SerializedObject serializedObject)
    {
      EditorGUI.BeginChangeCheck();

      // Arrays
      if (prop.isArray && prop.propertyType != SerializedPropertyType.String)
      {
        ReorderableListGUI.Title(prop.displayName);
        ReorderableListGUI.ListField(prop);
      }
      else
      {
        EditorGUILayout.PropertyField(prop, true);
      }

      // If property was changed, save
      if (EditorGUI.EndChangeCheck())
      {
        serializedObject.ApplyModifiedProperties();
        return true;
      }

      return false;
    }

    public static void DrawReorderableList(string title, SerializedProperty listProperty)
    {
      ReorderableListGUI.Title(title);
      ReorderableListGUI.ListField(listProperty);
    }


    //------------------------------------------------------------------------/
    // Procedures
    //------------------------------------------------------------------------/
    protected virtual void DrawBaseProperties()
    {
      DrawProperties(baseProperties);
    }

    protected virtual void DrawDeclaredProperties()
    {
      DrawProperties(declaredProperties);
    }

    private void DrawProperties(SerializedProperty[] properties)
    {      
      foreach (var prop in properties)
      {
        bool hasConstraint = propertyConstraints.ContainsKey(prop);
        if (hasConstraint)
        {
          bool canBeDrawn = propertyConstraints[prop].Invoke();
          if (!canBeDrawn)
            continue;
        }

        bool changed = DrawSerializedProperty(prop, serializedObject);
        if (changed)
        {
          //Trace.Script("Recording undo");
          Undo.RecordObject(target, prop.name);
        }
      }      
    }
       

  }

  /// <summary>
  /// Base editor for all Stratus components
  /// </summary>
  public abstract class BaseEditor<T> : BaseEditor where T : MonoBehaviour
  {
    protected T declaredTarget { get; private set; }

    protected sealed override void InitializeBaseEditor()
    {
      declaredTarget = target as T;
    }
        

  }

}