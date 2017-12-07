/******************************************************************************/
/*!
@file   TagField.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System;

namespace Stratus
{
  /// <summary>
  /// This field allows for manually tags layers in your inspector in a safe way, implicitly converting to the tag (string)
  /// </summary>
  [Serializable]
  public class TagField
  {
    [SerializeField]
    private string tag = "Untagged";
    public static implicit operator string(TagField tagField) { return tagField.tag; }
  }

}