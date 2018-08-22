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

  public void Start()
  {
        //WatchSlider.onValueChanged.AddListener(delegate { UpdateColor(); });
        UpdateColor();
  }
  public void UpdateColor()
  {
    var obj = GetComponent<Graphic>();

    if (WatchSlider.value < LowThreshold) obj.color = LowColor;
    else if (WatchSlider.value < MidThreshold) obj.color = MidColor;
    else obj.color = HighColor;
  }
}
