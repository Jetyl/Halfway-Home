/******************************************************************************/
/*!
@file   LinkControllerEditor.cs
@author Christian Sagel
@par    email: c.sagel\@digipen.edu
@par    DigiPen login: c.sagel
@date   5/25/2016
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;
using Stratus;
using Stratus.UI;
using UnityEditor;

namespace Stratus
{
  [CustomEditor(typeof(LinkController))]
  public class LinkControllerEditor : Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();
      var linkController = target as LinkController;
      ApplyStyles(linkController);
    }

    void ApplyStyles(LinkController linkController)
    {
      if (GUILayout.Button("Apply Styles"))
      {
        linkController.ApplyStyles();
      }
    }
      

  }
}

#endif