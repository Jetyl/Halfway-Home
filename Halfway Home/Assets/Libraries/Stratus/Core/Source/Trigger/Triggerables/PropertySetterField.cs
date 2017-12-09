using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus.Dependencies.Ludiq.Reflection;
using System;

namespace Stratus
{
  [Serializable]
  public class PropertySetterField 
  {
    [Filter(typeof(float), typeof(int), typeof(bool), typeof(Vector2), typeof(Vector3), typeof(Color), typeof(Vector4), Fields = true, Properties = true, Public = true)]
    public UnityMember property;
    [Tooltip("Over how long to interpolate this property to the specified value")]
    public float duration;
    // Different value types
    //[DrawIf("propertyType", ActionProperty.Types.Integer, ComparisonType.Equals)]
    [SerializeField]
    private int intValue;
    //[DrawIf("propertyType", ActionProperty.Types.Float, ComparisonType.Equals)]
    [SerializeField]
    private float floatValue;
    //[DrawIf("propertyType", ActionProperty.Types.Boolean, ComparisonType.Equals)]
    [SerializeField]
    private bool boolValue;
    //[DrawIf("propertyType", ActionProperty.Types.Vector2, ComparisonType.Equals)]
    [SerializeField]
    private Vector2 vector2Value;
    //[DrawIf("propertyType", ActionProperty.Types.Vector3, ComparisonType.Equals)]
    [SerializeField]
    private Vector3 vector3Value;
    //[DrawIf("propertyType", ActionProperty.Types.Vector4, ComparisonType.Equals)]
    [SerializeField]
    private Vector4 vector4Value;
    //[DrawIf("propertyType", ActionProperty.Types.Color, ComparisonType.Equals)]
    [SerializeField]
    private Color colorValue;

    [Tooltip("Whether a consecutive call will interpolate the property back to its intitial value")]
    public bool toggle;

    // The enumerated type of this property
    [SerializeField]
    [HideInInspector]
    private ActionProperty.Types propertyType = ActionProperty.Types.None;
    // Saved values for toggling
    private object previousValue;
    // Interpolate function used
    private IEnumerator interpolateRoutine;

    //private int propertyHeight => (propertyType == ActionProperty.Types.None) ? 2 : 5;

    //--------------------------------------------------------------------------------------------/
    // Methods
    //--------------------------------------------------------------------------------------------/
    public IEnumerator MakeInterpolateRoutine()
    {
      IEnumerator lerp = null;
      switch (propertyType)
      {
        case ActionProperty.Types.Integer:
          {
            int currentValue = property.Get<int>();
            bool shouldToggle = (toggle && currentValue == intValue);
            int nextValue = shouldToggle ? (int)previousValue : intValue;
            lerp = Routines.Lerp(currentValue, nextValue, duration, (float val) => { property.Set(Mathf.CeilToInt(val)); }, Routines.Lerp);
            previousValue = currentValue;
          }
          break;
        case ActionProperty.Types.Float:
          {
            float currentValue = property.Get<float>();
            bool shouldToggle = (toggle && currentValue == floatValue);
            Trace.Script("Previous float " + previousValue + ", Current Float = " + currentValue + ", Float Value = " + floatValue + ", shouldToggle = " + shouldToggle);
            float nextValue = shouldToggle ? (float)previousValue : floatValue;
            lerp = Routines.Lerp(currentValue, nextValue, duration, (float val) => { property.Set(val); }, Routines.Lerp);
            previousValue = currentValue;
          }
          break;
        case ActionProperty.Types.Boolean:
          {
            bool currentValue = property.Get<bool>();
            bool shouldToggle = (toggle && currentValue == boolValue);
            bool nextValue = shouldToggle ? (bool)previousValue : boolValue;
            lerp = Routines.Call(() => { property.Set(nextValue); }, duration);
            previousValue = currentValue;
          }
          break;
        case ActionProperty.Types.Vector2:
          {
            Vector2 currentValue = property.Get<Vector2>();
            bool shouldToggle = (toggle && currentValue == vector2Value);
            Vector2 nextValue = shouldToggle ? (Vector2)previousValue : vector2Value;
            lerp = Routines.Lerp(currentValue, nextValue, duration, (Vector2 val) => { property.Set(val); }, Vector2.Lerp);
            previousValue = currentValue;
          }
          break;
        case ActionProperty.Types.Vector3:
          {
            Vector3 currentValue = property.Get<Vector3>();
            bool shouldToggle = (toggle && currentValue == vector3Value);
            Vector3 nextValue = shouldToggle ? (Vector3)previousValue : vector3Value;
            lerp = Routines.Lerp(currentValue, nextValue, duration, (Vector3 val) => { property.Set(val); }, Vector3.Lerp);
            previousValue = currentValue;
          }
          break;
        case ActionProperty.Types.Vector4:
          {
            Vector4 currentValue = property.Get<Vector4>();
            bool shouldToggle = (toggle && currentValue == vector4Value);
            Vector4 nextValue = shouldToggle ? (Vector4)previousValue : vector4Value;
            lerp = Routines.Lerp(currentValue, nextValue, duration, (Vector4 val) => { property.Set(val); }, Vector4.Lerp);
            previousValue = currentValue;
          }
          break;
        case ActionProperty.Types.Color:
          {
            Color currentValue = property.Get<Color>();
            bool shouldToggle = (toggle && currentValue == colorValue);
            Color nextValue = shouldToggle ? (Color)previousValue : colorValue;
            lerp = Routines.Lerp(currentValue, nextValue, duration, (Color val) => { property.Set(val); }, Color.Lerp);
            previousValue = currentValue;
          }
          break;
        default:
          break;
      }
      return lerp;
    }

    /// <summary>
    /// Initializes this field
    /// </summary>
    public void Initialize()
    {
      previousValue = property.Get();
    }

    /// <summary>
    /// Validates the type of this property in editor
    /// </summary>
    public void Validate()
    {
      if (property.isAssigned)
        propertyType = ActionProperty.Deduce(property.type);
      else
        propertyType = ActionProperty.Types.None;
    }

    /// <summary>
    /// Sets this property
    /// </summary>
    public void Set(MonoBehaviour owner)
    {
      interpolateRoutine = MakeInterpolateRoutine(); // Routines.Lerp(interpolateFunc, duration);
      owner.StartCoroutine(interpolateRoutine, property.name);
    }

  }

}