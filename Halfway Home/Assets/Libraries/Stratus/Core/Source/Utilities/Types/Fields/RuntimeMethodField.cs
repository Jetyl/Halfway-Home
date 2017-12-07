﻿/******************************************************************************/
/*!
@file   RuntimeMethodField.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Stratus
{
  [Serializable]
  public class RuntimeMethodField
  {
    /// <summary>
    /// The method which this button will invoke
    /// </summary>
    public System.Action[] methods { get; private set; }

    public RuntimeMethodField(params System.Action[] methods)
    {
      this.methods = methods;
    }

    public RuntimeMethodField(System.Action method)
    {
      this.methods = new System.Action[] { method };
    }
    
  } 
}
