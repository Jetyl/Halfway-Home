using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stratus
{
  [ExecuteInEditMode]
  [DisallowMultipleComponent]
  public class TriggerSystem : StratusBehaviour
  {
    //------------------------------------------------------------------------/
    // Fields
    //------------------------------------------------------------------------/
    public List<Trigger> triggers = new List<Trigger>();
    public List<Triggerable> triggerables = new List<Triggerable>();

    //------------------------------------------------------------------------/
    // Messages
    //------------------------------------------------------------------------/
    private void Awake()
    {

    }

    private void OnDestroy()
    {
      ShowComponents(true);
    }

    private void OnEnable()
    {
      AddExisting();
      ShowComponents(false);
      ValidateTriggers();
    }

    private void OnDisable()
    {
      //ShowComponents(true);
    }    

    private void Reset()
    {
      AddExisting();
      ValidateTriggers();
      ShowComponents(false);
    }

    //------------------------------------------------------------------------/
    // Methods
    //------------------------------------------------------------------------/
    public void Add(BaseTrigger baseTrigger)
    {
      if (baseTrigger is Trigger)
        triggers.Add(baseTrigger as Trigger);
      else if (baseTrigger is Triggerable)
        triggerables.Add(baseTrigger as Triggerable);
    }

    private void AddExisting()
    {
      triggers.Clear();
      triggers.AddRange(GetComponents<Trigger>());
      triggerables.Clear();
      triggerables.AddRange(GetComponents<Triggerable>());
    }

    public void ShowComponents(bool show)
    {
      //Trace.Script($"show = {show}", this);
      HideFlags flag = show ? HideFlags.None : HideFlags.HideInInspector;
      foreach (var trigger in triggers)
        trigger.hideFlags = flag;
      foreach (var triggerable in triggerables)
        triggerable.hideFlags = flag;
    }
    
    private void ValidateTriggers()
    {
      foreach (var trigger in triggers)
      {
        trigger.scope = Trigger.Scope.Component;
      }
        
    }
    

  }

}