using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Stratus
{
  [LayoutViewDisplayAttribute("Member Visualizer", 225f, 200f, Overlay.Anchor.BottomRight, Overlay.Dimensions.Absolute)]
  public class MemberVisualizerSceneDisplay : LayoutSceneViewDisplay
  {
    //private bool useCustomColors = false;
    protected override bool isValid => MemberVisualizer.sceneGUIDrawCount > 0;
    private Vector2 scrollPos = Vector2.zero;
    private GUIStyle textStyle;

    protected override void OnInitialize()
    {
      base.OnInitialize();
    }

    protected override void OnGUI(Rect position)
    {
      
    }

    protected override void OnGUILayout(Rect position)
    {
      scrollPos = GUILayout.BeginScrollView(scrollPos, false, false);
      foreach(var drawList in MemberVisualizer.sceneGUIDrawLists)
      {
        foreach (var dl in drawList.Value)
        {
          GameObject go = dl.Key;
          GUILayout.Label($"{go.name}", EditorStyles.centeredGreyMiniLabel);
          foreach (var member in dl.Value)
          {            
            //if (useCustomColors)
            //  GUILayout.Label($"<color={member.hexColor}>{member.description}</color>");
            //else
              GUILayout.Label($"{member.description}", StratusGUIStyles.miniText);
          }
        }
      }      
      GUILayout.EndScrollView();
      //useCustomColors = GUILayout.Toggle(useCustomColors, "Custom Colors");
    }

    protected override void OnHierarchyWindowChanged()
    {
      
    }
  }

}