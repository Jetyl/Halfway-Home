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
    public string Key;
    public string TooltipType;

    public TooltipLineEvent(string key, string type)
    {
      Key = key;
      TooltipType = type;
    }
  };
  [System.Serializable]
  public class TooltipInfo
  {
    public string Key;
    public List<TooltipString> Strings;

    public TooltipInfo(string key, Color color, List<TooltipString> strings)
    {
      Key = key;
      Strings = strings;
    }
  };
  [System.Serializable]
  public class TooltipString
  {
    public string Key;
    public string Text;
    public Color Color;

    public TooltipString(string key, string text, Color color)
    {
      Key = key;
      Text = text;
      Color = color;
    }
  }

  public List<TooltipInfo> TooltipData;
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
    foreach(TooltipInfo t in TooltipData)
    {
      if (t.Key == e.Key)
      {
        foreach(TooltipString s in t.Strings)
        {
          if(s.Key == e.TooltipType)
          {
            CurrentTooltipText = s.Text;
            GetComponent<TextMeshProUGUI>().color = s.Color;
            TooltipAvailable = true;
          }
        }
      }
    }
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
