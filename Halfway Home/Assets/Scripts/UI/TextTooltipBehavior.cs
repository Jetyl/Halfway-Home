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

    public class TooltipActivateEvent : Stratus.Event
    {
      public TooltipElement ActivatingElement;

      public TooltipActivateEvent(TooltipElement tooltipElement)
      {
        ActivatingElement = tooltipElement;
      }
    };
    public class TooltipDeactivateEvent : Stratus.Event { };
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
    Stratus.Scene.Connect<TooltipActivateEvent>(OnTooltipActivateEvent);
    Stratus.Scene.Connect<TooltipDeactivateEvent>(OnTooltipDeactivateEvent);
  }

  void OnTooltipLineEvent(TooltipLineEvent e)
  {
    // JUST FOR COLOR NOW
    // For each tooltip key in TooltipData (publicly editable list)...
    foreach (TooltipInfo t in TooltipData)
    {
      // Compare that key to the element's key. When you find a match...
      if (t.Key == e.Key)
      {
        // Create a substring of the content data
        string[] typeStrings = e.TooltipType.Split(',');
        // For each string in the publicly editable TooltipString list
        foreach (TooltipString ts in t.Strings)
        {
          // Run through each string in the content substring
          foreach (string s1 in typeStrings)
          {
            // If one of those is a key that matches a string in the TooltipString list
            if (ts.Key == s1.Trim())
            {
              var useColor = true;
              TooltipAvailable = true;
              // Run through the substring list again to see if a no color flag exists
              foreach (string s2 in typeStrings) if (s2.Trim() == "nocolor") useColor = false;
              // Override the line color unless such a flag exists
              if (useColor) GetComponent<TextMeshProUGUI>().color = ts.Color;
            }
          }
        }
      }
    }
  }

    void OnTooltipActivateEvent(TooltipActivateEvent e)
    {
        CheckActivation(e.ActivatingElement);
    }

    void OnTooltipDeactivateEvent(TooltipDeactivateEvent e)
    {
        DeActivate();
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

  public void CheckActivation(TooltipElement element)
  {
    if(element.TooltipKey!=null && element.TooltipType != null)
    {
      // For each tooltip key in TooltipData (publicly editable list)...
      foreach (TooltipInfo t in TooltipData)
      {
        // Compare that key to the element's key. When you find a match...
        if (t.Key == element.TooltipKey)
        {
          // Create a substring of the content data
          string[] typeStrings = element.TooltipType.Split(',');
          // For each string in the publicly editable TooltipString list
          foreach (TooltipString ts in t.Strings)
          {
            // Run through each string in the content substring
            foreach (string s1 in typeStrings)
            {
              // If one of those is a key that matches a string in the TooltipString list
              if (ts.Key == s1.Trim())
              {
                // Assign the corresponding text
                TooltipContent.GetComponent<UIFader>().Show(0.1f);
                TooltipContent.GetComponentInChildren<TextMeshProUGUI>().text = ts.Text;
              }
            }
          }
        }
      }
    }
  }

  public void DeActivate()
  {
    TooltipContent.GetComponent<UIFader>().Hide(0.1f);
  }
}
