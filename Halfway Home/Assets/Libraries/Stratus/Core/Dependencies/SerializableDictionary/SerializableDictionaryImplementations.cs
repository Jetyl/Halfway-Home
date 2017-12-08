/******************************************************************************/
/*!
@file   SerializableDictionaryImplementation.cs
@author Christian Sagel
@par    email: ckpsm@live.com

All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System;
 
using UnityEngine;

[Serializable]
public class StringIntDictionary : SerializableDictionary<string, int> {}
[Serializable]
public class StringFloatDictionary : SerializableDictionary<string, float> {}
[Serializable]
public class StringBoolDictionary : SerializableDictionary<string, bool> {}

// ---------------
//  GameObject => Float
// ---------------
[Serializable]
public class GameObjectFloatDictionary : SerializableDictionary<GameObject, float> {}
