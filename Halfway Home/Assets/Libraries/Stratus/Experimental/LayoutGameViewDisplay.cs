using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stratus
{
  public abstract class LayoutGameViewDisplay<T> : Singleton<LayoutGameViewDisplay<T>> where T : MonoBehaviour
  {
    /// <summary>
    /// The current screen size of the game windoww
    /// </summary>
    private static Vector2 screenSize
    {
      get
      {
        #if UNITY_EDITOR
        string[] res = UnityEditor.UnityStats.screenRes.Split('x');
        Vector2 screenSize = new Vector2(int.Parse(res[0]), int.Parse(res[1]));
        #else
        Vector2 screenSize = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        #endif
        return screenSize;
      }
    }

    protected override void OnAwake()
    {

    }

  } 
}
