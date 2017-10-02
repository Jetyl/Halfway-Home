using Stratus.AI;
using System;
using UnityEngine;

namespace Stratus
{
  /// <summary>
  /// Base class for the player' avatar logic
  /// </summary>
  public abstract class Player : Agent
  {
    //--------------------------------------------------------------------------------------------/
    // Event Declarations
    //--------------------------------------------------------------------------------------------/
    /// <summary>
    /// Signals that the player has been revived
    /// </summary>
    public class ReviveEvent : Stratus.Event
    {
    }

    public class EnterCombatEvent : Stratus.Event
    {
    }

    public class ExitCombatEvent : Stratus.Event
    {
    }

    //--------------------------------------------------------------------------------------------/
    // Fields
    //--------------------------------------------------------------------------------------------/


    //--------------------------------------------------------------------------------------------/
    // Properties
    //--------------------------------------------------------------------------------------------/
    public override Blackboard blackboard { get { throw new NotImplementedException("The player does not use a blackboard!"); } }

    //--------------------------------------------------------------------------------------------/
    // Events
    //--------------------------------------------------------------------------------------------/
    protected abstract void OnRevive();
    protected abstract void OnPlayerSubscribe();

    //--------------------------------------------------------------------------------------------/
    // Messages
    //--------------------------------------------------------------------------------------------/
    protected override void OnCombatEnter()
    {
      //Trace.Script("Entered combat!", this);
      Scene.Dispatch<EnterCombatEvent>(new EnterCombatEvent());
    }
    protected override void OnCombatExit()
    {
      //Trace.Script("Exited combat!", this);
      Scene.Dispatch<ExitCombatEvent>(new ExitCombatEvent());
    }

    protected override void OnSubscribe()
    {
      this.gameObject.Connect<ReviveEvent>(this.OnReviveEvent);
      this.OnPlayerSubscribe();
    }

    void OnReviveEvent(ReviveEvent e)
    {
      this.OnRevive();
    }


  }

}