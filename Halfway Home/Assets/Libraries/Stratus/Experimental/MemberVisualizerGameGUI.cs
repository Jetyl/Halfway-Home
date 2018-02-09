using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;


namespace Stratus
{
  [Singleton("Member Visualizer Game GUI", true, true)]
  public class MemberVisualizerGameGUI : Singleton<MemberVisualizerGameGUI>
  {
    public Overlay.Anchor anchor = Overlay.Anchor.TopRight;
    public Vector2 size = new Vector2(225f, 200f);

    private bool draw => MemberVisualizer.gameGUIDrawCount > 0;
    private Rect position { get; set; }

    private Vector2 scrollPos { get; set; }

    protected override void OnAwake()
    {
      
    }

    private void OnGUI()
    {
      if (!draw)
        return;

      //GUILayout.Label("Member Visualizer");

      #if UNITY_EDITOR
      string[] res = UnityEditor.UnityStats.screenRes.Split('x');
      Vector2 screenSize = new Vector2(int.Parse(res[0]), int.Parse(res[1]));
      #else
      Vector2 screenSize = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
      #endif

      Rect layoutPosition = Overlay.CalculateAnchoredPositionOnScreen(anchor, size, screenSize);
      GUILayout.BeginArea(layoutPosition);
      GUILayout.Label("Member Visualizer");
      DrawProperties();
      GUILayout.EndArea();
    }    

    private void DrawProperties()
    {
      scrollPos = GUILayout.BeginScrollView(scrollPos, false, false);
      foreach (var drawList in MemberVisualizer.gameGUIDrawLists)
      {
        foreach (var dl in drawList.Value)
        {
          GameObject go = dl.Key;
          GUILayout.Label($"{go.name}");
          foreach (var member in dl.Value)
          {
            //if (useCustomColors)
            //  GUILayout.Label($"<color={member.hexColor}>{member.description}</color>");
            //else
            GUILayout.Label($"{member.description}");
          }
        }
      }
      GUILayout.EndScrollView();
    }

    public void Add(MemberVisualizer mv)
    {      
    }

    public void Remove(MemberVisualizer mv)
    {

    }

  }

}