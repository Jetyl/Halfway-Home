/******************************************************************************/
/*!
@file   InputTrigger.cs
@author Christian Sagel
@par    email: c.sagel\@digipen.edu
@par    DigiPen login: c.sagel
All content � 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using System;

namespace Prototype 
{
  public class InputTrigger : Trigger
  {
    public InputField input = new InputField();
    public InputField.Action action = InputField.Action.Down;
    
    protected override void OnInitialize()
    {      
    }

    void Update()
    {
      bool triggered = false;
      switch (action)
      {
        case InputField.Action.Down:
          triggered = input.isDown;
          break;
        case InputField.Action.Up:
          triggered = input.isUp;
          break;
        case InputField.Action.Held:
          triggered = input.isHeld;
          break;
      }

      if (triggered)
        Activate();
    }
  }
}
