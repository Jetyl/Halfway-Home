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

    public class TooltipActivateEvent : Stratus.Event { };
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
  private bool IsChoiceTooltip;

	// Use this for initialization
	void Start ()
  {
    Stratus.Scene.Connect<TooltipLineEvent>(OnTooltipLineEvent);
    Space.Connect<DescriptionEvent>(Events.Description, ResetTooltip);
    Stratus.Scene.Connect<TooltipActivateEvent>(OnTooltipActivateEvent);
    Stratus.Scene.Connect<TooltipDeactivateEvent>(OnTooltipDeactivateEvent);
    Space.Connect<DefaultEvent>(Events.ConversationChoice, OnChoiceBegin);
    Space.Connect<DefaultEvent>(Events.ChoicesFinished, OnChoiceEnd);
    IsChoiceTooltip = false;
  }

  void OnTooltipLineEvent(TooltipLineEvent e)
  {
    // For each tooltip key in TooltipData (publicly editable list)...
    foreach(TooltipInfo t in TooltipData)
    {
      // Compare that key to the event key. When you find a match...
      if (t.Key == e.Key)
      {
        // Create a substring of the content data
        string[] typeStrings = e.TooltipType.Split(',');
        // For each string in the publicly editable TooltipString list
        foreach (TooltipString ts in t.Strings)
        {
          // Run through each string in the content substring
          foreach(string s1 in typeStrings)
          {
            // If one of those is a key that matches a string in the TooltipString list
            if (ts.Key == s1.Trim())
            {
              // Assign the corresponding text
              CurrentTooltipText = ts.Text;
              var useColor = true;
              // Run through the substring list again to see if a no color flag exists
              foreach (string s2 in typeStrings) if (s2.Trim() == "nocolor") useColor = false;
              // Override the line color unless such a flag exists
              if(useColor) GetComponent<TextMeshProUGUI>().color = ts.Color;
              TooltipAvailable = true;
            }
          }
        }
      }
    }
  }

  void OnChoiceBegin(DefaultEvent e)
  {
    IsChoiceTooltip = true;
  }

  void OnChoiceEnd(DefaultEvent e)
  {
    IsChoiceTooltip = false;
  }

    void OnTooltipActivateEvent(TooltipActivateEvent e)
    {
        CheckActivation();
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

  public void TextHoverEnter()
  {
    if (!IsChoiceTooltip) CheckActivation();
  }

  public void TextHoverExit()
  {
    if (!IsChoiceTooltip) DeActivate();
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
