/******************************************************************************/
/*!
@file   BehaviorTreeNode.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System;
using UnityEditor;
using Stratus.Editors;

namespace Stratus
{
  namespace AI
  {
    public class BehaviorTreeNode : Node
    {
      /// <summary>
      /// The behavior this node represents
      /// </summary>
      public Behavior Behavior;

      protected override void OnProcessContextMenu(GenericMenu menu)
      {
        menu.AddItem(new GUIContent("Stuff"), false, What);
      }

      void What()
      {

      }

    }

  } 
}