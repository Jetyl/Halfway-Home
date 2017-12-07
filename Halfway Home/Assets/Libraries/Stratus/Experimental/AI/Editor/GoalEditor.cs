/******************************************************************************/
/*!
@file   GoalEditor.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using UnityEditor;
using Stratus.Types;
using Rotorz.ReorderableList;

namespace Stratus
{
  namespace AI
  {
    [CustomEditor(typeof(Goal))]
    public class GoalEditor : Editor
    {
    }
  }
}