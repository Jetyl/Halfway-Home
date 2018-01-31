using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using Rotorz.ReorderableList;
using UnityEditorInternal;

namespace Stratus
{
  /// <summary>
  /// Base editor for all Stratus components
  /// </summary>
  public abstract class BaseEditor : Editor
  {
    public enum DrawOrder { Base, Declared }
    public class PropertyConstraintMap : Dictionary<SerializedProperty, Func<bool>> { }

    /// <summary>
    /// Specific constraints that dictate whether a specified property should be displayed
    /// </summary>
    public PropertyConstraintMap propertyConstraints { get; set; } = new PropertyConstraintMap();
    /// <summary>
    /// Overrides for drawing specific properties from specific types
    /// </summary>
    public Dictionary<Type, System.Action> drawTypeOverrides { get; set; } = new Dictionary<Type, System.Action>();
    /// <summary>
    /// A map of all available properties by name
    /// </summary>
    public Dictionary<string, SerializedProperty> propertyMap { get; set; } = new Dictionary<string, SerializedProperty>();
    /// <summary>
    /// The set of properties of the most-derived class
    /// </summary>
    public Tuple<Type, SerializedProperty[]> declaredProperties => propertyGroups.Last();
    /// <summary>
    /// A map of all property groups by the type
    /// </summary>
    public Dictionary<Type, SerializedProperty[]> propertiesByType { get; set; } = new Dictionary<Type, SerializedProperty[]>();
    /// <summary>
    /// A list of all different property groups, starting from the base class to the most-derived class
    /// </summary>
    public List<Tuple<Type, SerializedProperty[]>> propertyGroups { get; set; } = new List<Tuple<Type, SerializedProperty[]>>();
    /// <summary>
    /// Whether to draw labels for types above property groups
    /// </summary>
    public virtual bool drawTypeLabels => false;
    /// <summary>
    /// A collection of all registered lists to be drawn with reoderable within this editor
    /// </summary>
    protected Dictionary<SerializedProperty, ReorderableList> reorderableLists { get; set; } = new Dictionary<SerializedProperty, ReorderableList>();
    /// <summary>
    /// Whether any custom GUI styles have been configured
    /// </summary>
    private bool initializedStyles { get; set; }

    protected virtual void InitializeBaseEditor() {}
    protected virtual void PostEnable() {}
    protected virtual void OnConfigureGUIStyles() {}

    //------------------------------------------------------------------------/
    // Messages
    //------------------------------------------------------------------------/
    private void OnEnable()
    {
      AddProperties();
      InitializeBaseEditor();
      PostEnable();
    }

    public override void OnInspectorGUI()
    {
      // Invoke the very first time
      if (!initializedStyles)
      {
        OnConfigureGUIStyles();
        initializedStyles = true;
      }

      // Update the serialized object, saving data
      serializedObject.Update();

      // Now draw the base editor
      OnBaseEditorGUI();
    }

    public virtual void OnBaseEditorGUI()
    {
      // Reverse order: Draw all the types up until the most-derived
      for (int i = 0; i < propertyGroups.Count - 1; i++)
      {
        var properties = propertyGroups[i];

        // If there's no properties for this type
        if (properties.Item2.Length < 1)
          continue;

        // If all properties fail the constraints check
        //if (!ValidateConstraints(properties.Item2))
        //  continue;


        if (drawTypeLabels)
          EditorGUILayout.LabelField(properties.Item1.Name, EditorStyles.centeredGreyMiniLabel);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawProperties(properties.Item2);
        EditorGUILayout.EndVertical();
      }

      // Now draw the declared properties
      if (declaredProperties.Item2.Length > 0)
      {
        if (drawTypeLabels)
          EditorGUILayout.LabelField(declaredProperties.Item1.Name, EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawProperties(declaredProperties.Item2);
        EditorGUILayout.EndVertical();
      }
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
          //Trace.Script("Could not find property: " + propInfo[a].Name);
        }
      }

      return properties;
    }

    /// <summary>
    /// Draws a property, saving it if it changes
    /// </summary>
    /// <param name="prop"></param>

    public bool DrawSerializedProperty(SerializedProperty prop, SerializedObject serializedObject)
    {
      EditorGUI.BeginChangeCheck();

      // Arrays
      if (prop.isArray && prop.propertyType != SerializedPropertyType.String)
      {
        //reorderableLists[prop].DoLayoutList();
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

    public static ReorderableList GetListWithFoldout(SerializedObject serializedObject, SerializedProperty property, bool draggable, bool displayHeader, bool displayAddButton, bool displayRemoveButton)
    {
      var list = new ReorderableList(serializedObject, property, draggable, displayHeader, displayAddButton, displayRemoveButton);

      list.drawHeaderCallback = (Rect rect) =>
      {
        var newRect = new Rect(rect.x + 10, rect.y, rect.width - 10, rect.height);
        property.isExpanded = EditorGUI.Foldout(newRect, property.isExpanded, property.displayName);
      };
      list.drawElementCallback =
        (Rect rect, int index, bool isActive, bool isFocused) =>
        {
          if (!property.isExpanded)
          {
            GUI.enabled = index == list.count;
            return;
          }

          var element = list.serializedProperty.GetArrayElementAtIndex(index);
          rect.y += 2;
          EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
        };
      list.elementHeightCallback = (int indexer) =>
      {
        if (!property.isExpanded)
          return 0;
        else
          return list.elementHeight;
      };

      return list;
    }



    //------------------------------------------------------------------------/
    // Procedures
    //------------------------------------------------------------------------/
    protected virtual void DrawDeclaredProperties()
    {
      DrawProperties(declaredProperties.Item2);
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

    private void AddProperties()
    {
      // For every type, starting from the most derived up to the base, get its serialized properties      
      Type declaredType = target.GetType();
      Type currentType = declaredType;
      while (currentType != typeof(MonoBehaviour))
      {
        var properties = GetSerializedProperties(serializedObject, currentType);
        //Trace.Script($"Adding properties for {currentType.Name}");

        // Add the properties onto the map
        foreach (var prop in properties)
        {
          propertyMap.Add(prop.name, prop);
          if (prop.isArray && prop.propertyType != SerializedPropertyType.String)
          {
            ReorderableList list = GetListWithFoldout(serializedObject, prop, true, true, true, true);
            reorderableLists.Add(prop, list);
          }
        }

        // Add all the properties for this type into the property map by type        
        propertiesByType.Add(currentType, properties);
        propertyGroups.Add(new Tuple<Type, SerializedProperty[]>(currentType, properties));

        // Move onto the next type
        //Trace.Script($"BaseType = {currentType.BaseType.Name}");
        currentType = currentType.BaseType;
      }

      propertyGroups.Reverse();
    }

    /// <summary>
    /// Checks whether all the properties are under constraints. Returns false if none
    /// of the properties can be drawn.
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    private bool ValidateConstraints(SerializedProperty[] properties)
    {
      foreach(var prop in properties)
      {
        if (propertyConstraints.ContainsKey(prop) && propertyConstraints[prop]())
          return true;
      }

      // None of the properties could be drawn
      return false;
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