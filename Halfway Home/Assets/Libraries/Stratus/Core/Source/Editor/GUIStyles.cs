/******************************************************************************/
/*!
@file   GUIStyles.cs
@author Christian Sagel
@par    email: ckpsm@live.com
@date   5/25/2016
*/
/******************************************************************************/
using UnityEngine;
using UnityEditor;

namespace Stratus
{
  /// <summary>
  /// Custom styles for the Stratus framework
  /// </summary>
  public static class StratusEditorStyles
  {
    public static GUIStyle editorLine { get; private set; }
    public static GUIStyle tintable { get; private set; }

    /// <summary>
    /// The main background color used by UI elmeents
    /// </summary>
    public static Color backgroundColor => azureColor;
    public static Color azureColor { get; set; }

    static StratusEditorStyles()
    {
      azureColor = new Color(0, 191, 255);
      editorLine = new GUIStyle(GUI.skin.box);
      editorLine.border.top = editorLine.border.bottom =
      editorLine.border.left = editorLine.border.right = 1;
      editorLine.margin.top = editorLine.margin.bottom =
      editorLine.margin.left = editorLine.margin.right = 1;
      editorLine.padding.top = editorLine.padding.bottom =
      editorLine.padding.left = editorLine.padding.right = 1;

      tintable = new GUIStyle();
      tintable.normal.background = EditorGUIUtility.whiteTexture;
      tintable.stretchWidth = tintable.stretchHeight = true;
    }

    /// <summary>
    /// Draws the selected background color inside the given rect
    /// </summary>
    /// <param name="position"></param>
    /// <param name="color"></param>
    public static void DrawBackgroundColor(Rect position, Color color)
    {
      if (UnityEngine.Event.current.type == EventType.Repaint)
      {
        var prevColor = GUI.color;
        GUI.color = color;
        tintable.Draw(position, false, false, false, false);
        GUI.color = prevColor;
      }
    }

    /// <summary>
    /// Generates a 2D texture given another texture to use for dimensions and the color
    /// </summary>
    /// <param name="other"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static Texture2D MakeTexture(Texture2D other, Color color)
    {
      return MakeTexture(other.width, other.height, color);
    }

    /// <summary>
    /// Generates a 2D texture
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public static Texture2D MakeTexture(int width, int height, Color col)
    {
      Color[] pix = new Color[width * height];

      for (int i = 0; i < pix.Length; i++)
        pix[i] = col;

      Texture2D result = new Texture2D(width, height);
      result.SetPixels(pix);
      result.Apply();

      return result;
    }

  }

}