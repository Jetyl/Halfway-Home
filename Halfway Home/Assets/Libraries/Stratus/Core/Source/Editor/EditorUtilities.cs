using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stratus
{
  public static class StratusEditorUtlity
  {
    public static void OnMouseClick(System.Action onLeftClick, System.Action onRightClick)
    {
      var button = UnityEngine.Event.current.button;      
      // Left click
      if (button == 0)
        onLeftClick?.Invoke();
      // Right click
      else if (button == 1)
        onRightClick?.Invoke();
    }
    

  }

}