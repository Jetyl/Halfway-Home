/******************************************************************************/
/*!
@file   BehaviorTreeEditor.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using UnityEditor;

namespace Stratus
{
  namespace AI
  {
    [CustomEditor(typeof(BehaviorTree))]
    public class BehaviorTreeEditor : Editor
    {
      BehaviorTree BehaviorTree;

      private void OnEnable()
      {
        BehaviorTree = target as BehaviorTree;
      }

      public override void OnInspectorGUI()
      {
        if (GUILayout.Button("Edit"))
        {
          BehaviorTreeEditorWindow.Open(this.BehaviorTree);
        }
      }


    } 
  }
}