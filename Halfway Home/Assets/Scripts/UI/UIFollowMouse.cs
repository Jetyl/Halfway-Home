using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIFollowMouse : MonoBehaviour
{
  public Canvas parentCanvas;

  public void Start()
  {
    if(parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
    {
      Vector3 startPos = Input.mousePosition;
    }
    else if (parentCanvas.renderMode == RenderMode.ScreenSpaceCamera)
    {
      Vector2 startPos;
      RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition,
                                                              parentCanvas.worldCamera, out startPos);
    }
  }

  public void Update()
  {
    if (parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
    {
      Vector3 newPos = Input.mousePosition;

      transform.position = newPos;
    }
    else if (parentCanvas.renderMode == RenderMode.ScreenSpaceCamera)
    {
      Vector2 newPos;

      RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out newPos);

      transform.position = parentCanvas.transform.TransformPoint(newPos);
    }
  }
}
