using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorSwitcher : MonoBehaviour
{
  private Color StartColor;
  public Color[] Colors;

	// Use this for initialization
	void Start ()
  {
    StartColor = GetComponent<Image>().color;
	}

  public void SwitchColorTo(int colorState)
  {
    if (colorState + 1 > Colors.Length) return;
    Debug.Log("Color swap");
    GetComponent<Image>().CrossFadeColor(Colors[colorState], 0f, true, false);
  }

  public void RevertColor()
  {
    Debug.Log("Color revert");
    GetComponent<Image>().CrossFadeColor(StartColor, 0f, true, false);
  }
}
