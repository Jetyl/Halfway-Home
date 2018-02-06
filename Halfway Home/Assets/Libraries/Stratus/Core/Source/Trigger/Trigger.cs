/******************************************************************************/
/*!
@file   Trigger.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Stratus
{
  /// <summary>
  /// A component that triggers a selected triggerable when the specified condition is met.
  /// </summary>
  public abstract class Trigger : BaseTrigger
  {
    //------------------------------------------------------------------------/
    // Events
    //------------------------------------------------------------------------/
    public enum Instruction
    {
      /// <summary>
      /// Instructs the triggerable to activate (default)
      /// </summary>
      [Tooltip("Instructs the triggerable to activate (default)")]
      On,
      /// <summary>
      /// Instructs the triggerable to deactivate
      /// </summary>
      [Tooltip("Instructs the triggerable to deactivate")]
      Off,
      /// <summary>
      /// Instructs the triggerable something it cared about happened
      /// </summary>
      [Tooltip("Instructs the triggerable something it cared about happened")]
      Signal
    }

    /// <summary>
    /// When received by a triggerable component, it will activate it
    /// </summary>
    public class TriggerEvent : Stratus.Event
    {
      public Instruction instruction;

      public TriggerEvent(Instruction instruction = Instruction.On)
      {
        this.instruction = instruction;
      }
    }

    /// <summary>
    /// When received by a trigger, enables it
    /// </summary>
    public class EnableEvent : Stratus.Event
    {
      public bool enabled;
      public EnableEvent(bool enabled) { this.enabled = enabled; }
    }

    /// <summary>
    /// How the trigger is delivered to the target triggerable
    /// </summary>
    public enum Scope
    {
      [Tooltip("Only the target triggerable will be triggered")]
      Component,
      [Tooltip("All triggerable components in the GameObject will be triggered")]
      GameObject
    }

    //------------------------------------------------------------------------/
    // Fields
    //------------------------------------------------------------------------/
    [Header("Targeting")]
    [Tooltip("What component to send the trigger event to")]
    public List<Triggerable> targets = new List<Triggerable>();
    [Tooltip("What instruction to send to the triggerable")]
    public Instruction instruction;
    [Tooltip("Whether the trigger will be sent to the GameObject as an event or invoked directly on the dispatcher component")]
    public Scope scope = Scope.Component;
    [Tooltip("Whether it should also trigger all of the target's children")]
    public bool recursive = false;

    [Header("Lifetime")]
    [Tooltip("Whether the trigger should persist after being activated")]
    public bool persistent = true;

    /// <summary>
    /// Subscribe to be notified when this trigger has been activated
    /// </summary>
    public UnityAction<Trigger> onActivate { get; set; } = (Trigger trigger) => { };

    //------------------------------------------------------------------------/
    // Properties
    //------------------------------------------------------------------------/
    /// <summary>
    /// Whether we are currently debugging the trigger (development purposes)
    /// </summary>
    private bool isDebug => false;
    /// <summary>
    /// The trigger event being dispatched
    /// </summary>
    private TriggerEvent triggerEvent => new TriggerEvent(instruction);

    /// <summary>
    /// Invoked the first time this trigger is initialized
    /// </summary>
    protected abstract void OnAwake();

    /// <summary>
    /// Invoked when this trigger is enabled
    /// </summary>
    protected virtual void OnEnabled() { }

    //------------------------------------------------------------------------/
    // Messages
    //------------------------------------------------------------------------/
    /// <summary>
    /// On awake, thee trigger first initializes the subclass before connecting to enabled events
    /// </summary>
    void Awake()
    {
      this.OnAwake();
      this.gameObject.Connect<EnableEvent>(this.OnEnableEvent);
    }

    //------------------------------------------------------------------------/
    // Methods
    //------------------------------------------------------------------------/
    /// <summary>
    /// If the trigger was initially disabled,, enables it
    /// </summary>
    /// <param name="e"></param>
    void OnEnableEvent(EnableEvent e)
    {
      enabled = true;
    }

    /// <summary>
    /// Triggers the event
    /// </summary>
    protected void Activate()
    {
      if (!enabled)
        return;

      // Dispatch the trigger event onto a given target if one is provided
      foreach(var target in targets)
      {
        if (target == null)
          continue;

        if (scope == Scope.GameObject)
        {
          target.gameObject.Dispatch<TriggerEvent>(triggerEvent);
          if (this.recursive)
          {
            foreach (var child in target.gameObject.Children())
            {
              child.Dispatch<TriggerEvent>(triggerEvent);
            }
          }
        }

        else if (scope == Scope.Component)
        {
          target.Trigger();
        }
      }

      //if (target)
      //{
      //  if (delivery == DeliveryMethod.Event)
      //  {
      //    this.target.gameObject.Dispatch<TriggerEvent>(triggerEvent);
      //    if (this.recursive)
      //    {
      //      foreach (var child in this.target.gameObject.Children())
      //      {
      //        child.Dispatch<TriggerEvent>(triggerEvent);
      //      }
      //    }
      //  }
      //
      //  else if (delivery == DeliveryMethod.Invoke)
      //  {
      //    this.target.Trigger();
      //  }
      //}

      // If not persistent, disable
      if (!persistent)
      {
        this.enabled = false;
      }

      // Announce this trigger was activated
      this.onActivate(this);

    }








  }

}