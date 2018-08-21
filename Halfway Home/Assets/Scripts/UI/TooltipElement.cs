using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipElement : MonoBehaviour
{
  public TooltipSource Type;
  [HideInInspector]
  public string TooltipKey;
  [HideInInspector]
  public string TooltipType;
  private bool TooltipActive;
	// Use this for initialization
	void Start ()
  {
    if (Type == TooltipSource.Text)
    {
      Stratus.Scene.Connect<TextTooltipBehavior.TooltipLineEvent>(OnTooltipLineEvent);
      Space.Connect<DescriptionEvent>(Events.Description, ResetTooltip);
    }
  }

  void OnTooltipLineEvent(TextTooltipBehavior.TooltipLineEvent e)
  {
    TooltipKey = e.Key;
    TooltipType = e.TooltipType;
    TooltipActive = true;
  }

  void ResetTooltip(DefaultEvent e)
  {
    if (TooltipActive) TooltipActive = false;
    else
    {
      TooltipKey = null;
      TooltipType = null;
    }
  }

  public enum TooltipSource
  {
    Text,
    Choice
  };
}
