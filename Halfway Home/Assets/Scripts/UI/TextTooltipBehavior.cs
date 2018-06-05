using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextTooltipBehavior : MonoBehaviour
{
  public class TooltipLineEvent : Stratus.Event
  {
    public Color LineColor;
    public string TooltipText;

    public TooltipLineEvent(Color lineColor, string tooltipText)
    {
      LineColor = lineColor;
      TooltipText = tooltipText;
    }
  };

  private bool TooltipAvailable;
  private string CurrentTooltipText;
  public UIFollowMouse TooltipContent;

	// Use this for initialization
	void Start ()
  {
    Stratus.Scene.Connect<TooltipLineEvent>(OnTooltipLineEvent);
    Space.Connect<DescriptionEvent>(Events.Description, ResetTooltip);
  }

  void OnTooltipLineEvent(TooltipLineEvent e)
  {
    GetComponent<TextMeshProUGUI>().color = e.LineColor;
    CurrentTooltipText = e.TooltipText;
    TooltipAvailable = true;
  }

  void ResetTooltip(DefaultEvent eventData)
  {
    DeActivate();
    if (TooltipAvailable) TooltipAvailable = false;
    else
    {
      GetComponent<TextMeshProUGUI>().color = Color.white;
      CurrentTooltipText = "";
    }
  }

  public void CheckActivation()
  {
    if(TooltipContent != null && CurrentTooltipText != "")
    {
      TooltipContent.GetComponent<UIFader>().Show(0.1f);
      TooltipContent.GetComponentInChildren<TextMeshProUGUI>().text = CurrentTooltipText;
    }
  }

  public void DeActivate()
  {
    TooltipContent.GetComponent<UIFader>().Hide(0.1f);
  }
}
