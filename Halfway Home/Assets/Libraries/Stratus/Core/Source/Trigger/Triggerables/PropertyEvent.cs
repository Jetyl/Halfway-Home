using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus.Dependencies.Ludiq.Reflection;
using Stratus.Types;

namespace Stratus
{
  /// <summary>
  /// Provides the ability to provide changes to a specified MonoBehaviour's properties at runtime
  /// </summary>
  public class PropertyEvent : Triggerable
  {
    //--------------------------------------------------------------------------------------------/
    // Fields
    //--------------------------------------------------------------------------------------------/    
    public List<PropertySetterField> setters = new List<PropertySetterField>();
    public PropertySetterField wat;

    //--------------------------------------------------------------------------------------------/
    // Messages
    //--------------------------------------------------------------------------------------------/
    protected override void OnAwake()
    {
      foreach (var property in setters)
        property.Initialize();
    }

    protected override void OnTrigger()
    {
      foreach (var property in setters)
        property.Set(this);
    }

    private void OnValidate()
    {
      wat.Validate();
      foreach (var setter in setters)
        setter.Validate();
    }

    //--------------------------------------------------------------------------------------------/
    // Methods
    //--------------------------------------------------------------------------------------------/ 
    //private void ChangeProperty()
    //{
    //  var lerp = PrepareInterpolator(); // Routines.Lerp(interpolateFunc, duration);
    //  this.StartTaggedCoroutine(lerp, "ChangeProperty");
    //}


  }
}