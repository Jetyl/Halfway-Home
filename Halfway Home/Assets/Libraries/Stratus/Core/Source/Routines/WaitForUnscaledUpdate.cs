/******************************************************************************/
/*!
@file   WaitForUnscaledUpdate.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using Stratus;

namespace Stratus
{
  public class WaitForUnscaledUpdate : CustomYieldInstruction
  {
    private float WaitTIme;

    public override bool keepWaiting
    {
      get
      {
        return Time.realtimeSinceStartup < WaitTIme;
      }
    }

    public WaitForUnscaledUpdate()
    {
      //WaitTIme += Time.fixedDeltaTime;
      WaitTIme = Time.realtimeSinceStartup + Time.fixedDeltaTime;
    }
  }
}
