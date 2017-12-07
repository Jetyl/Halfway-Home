/******************************************************************************/
/*!
@file   SerializedDictionaries.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using Rotorz.Extras.Collections;
using System;

namespace Stratus
{
  namespace Types
  {
    public sealed class StringStringDictionaryEditable : EditableEntry<StringStringDictionary> { }
    [Serializable, EditableEntry(typeof(StringStringDictionaryEditable))]
    public sealed class StringStringDictionary : OrderedDictionary<string, string> { }

  }
}