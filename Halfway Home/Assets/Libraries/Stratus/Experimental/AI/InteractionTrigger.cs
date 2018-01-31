/******************************************************************************/
/*!
@file   InteractionTrigger.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using System;

namespace Stratus.AI
{
  /// <summary>
  /// Receives interaction events with the player
  /// </summary>
  public class InteractionTrigger : Trigger
  {
    public enum TriggerType { Interaction, Scan }

    [Header("Interaction")]
    public TriggerType type = TriggerType.Interaction;    

    /// <summary>
    /// Initializes the InteractionTrigger, subscribing to events with the player
    /// </summary>
    protected override void OnAwake()
    {
      switch (type)
      {
        case TriggerType.Interaction:
          this.gameObject.Connect<Sensor.InteractEvent>(this.OnInteractEvent);
          break;
        case TriggerType.Scan:
          this.gameObject.Connect<Sensor.ScanEvent>(this.OnScanEvent);
          break;
      }
    }

    /// <summary>
    /// Received when this object is within vicinity of the agent.
    /// </summary>
    /// <param name="e"></param>
    void OnScanEvent(Sensor.ScanEvent e)
    {
      //if (!enabled)
      //  return;

      //if (type == TriggerType.Scan)
      this.Activate();
      //else if (type == TriggerType.Interaction)
      //{
      //  var response = new Agent.InteractionAvailableEvent();
      //  response.interactive = this;
      //  response.context = this.context;
      //  e.agent.gameObject.Dispatch<Agent.InteractionAvailableEvent>(response);
      //}
    }

    /// <summary>
    /// Received when there's a request to interact with this object
    /// </summary>
    /// <param name="e"></param>
    void OnInteractEvent(Sensor.InteractEvent e)
    {
      this.Activate();
    }


  }
}