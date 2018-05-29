using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class SimpleSliderColor : MonoBehaviour
{
  public Slider WatchSlider;
  public int LowThreshold;
  public Color LowColor;
  public int MidThreshold;
  public Color MidColor;
  public Color HighColor;

  public void UpdateColor()
  {
    var color = GetComponent<Graphic>().color;

    if (WatchSlider.value < LowThreshold) color = LowColor;
    else if (WatchSlider.value < MidThreshold) color = MidColor;
    else color = HighColor;
  }
}
