/******************************************************************************/
/*!
@file   PlannerEditor.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using UnityEditor;
using Rotorz.ReorderableList;

namespace Stratus
{
  namespace AI
  {
    [CustomEditor(typeof(Planner))]
    public class PlannerEditor : Editor
    {
      public override void OnInspectorGUI()
      {
        base.OnInspectorGUI();
      }
    } 

  }
}