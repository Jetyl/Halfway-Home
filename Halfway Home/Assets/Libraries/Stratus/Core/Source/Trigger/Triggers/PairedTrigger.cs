/******************************************************************************/
/*!
@file   PariedTrigger.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using System;

namespace Stratus
{
  /// <summary>
  /// A trigger that pairs itself to another trigger. When that one is activated,
  /// so will this one be.
  /// </summary>
  public class PairedTrigger : Trigger
  {
    [Header("Pair Settings")]
    [Tooltip("The other trigger which we activate on")]
    public Trigger Other;
    
    protected override void OnInitialize()
    {
      Other.onActivate += this.Activate;
    }

  }
}