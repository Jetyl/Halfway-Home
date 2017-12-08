/******************************************************************************/
/*!
@file   ScriptableObjectExtensions.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Stratus.Utilities;

namespace Stratus
{
  public static partial class Extensions
  {
    public static T AddInstanceToAsset<T>(this ScriptableObject assetObject) where T : ScriptableObject
    {
      return Assets.AddInstanceToAsset<T>(assetObject);
    }

    public static ScriptableObject AddInstanceToAsset(this ScriptableObject assetObject, Type type)
    {
      return Assets.AddInstanceToAsset(assetObject, type);
    }

  }

}