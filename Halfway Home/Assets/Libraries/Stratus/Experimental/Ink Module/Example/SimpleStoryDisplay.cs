using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

namespace Stratus
{
  namespace InkModule
  {
    /// <summary>
    /// A simple ink story display that displays one line at a time
    /// </summary>
    public class SimpleStoryDisplay : StoryDisplay
    {
      //------------------------------------------------------------------------------------------/
      // Fields
      //------------------------------------------------------------------------------------------/
      [Header("Dialog")]
      public Text speakerText;
      public Text messageText;

      [Header("Choices")]
      [SerializeField]
      public Button choicePrefab;
      public CanvasRenderer dialogPanel;
      public VerticalLayoutGroup choicesPanel;      

      //------------------------------------------------------------------------------------------/
      // Properties
      //------------------------------------------------------------------------------------------/
      public bool displayChoices
      {
        set
        {
          choicesPanel.gameObject.SetActive(value);
          dialogPanel.gameObject.SetActive(!value);
        }
      }

      public bool display
      {
        set
        {
          dialogPanel.gameObject.SetActive(value);
          choicesPanel.gameObject.SetActive(value);
        }
      }


      //------------------------------------------------------------------------------------------/
      // Methods
      //------------------------------------------------------------------------------------------/
      protected override void OnStart()
      {
        display = false;
      }

      protected override void OnStoryStarted()
      {
        display = true;
        displayChoices = false;
      }

      protected override void OnStoryEnded()
      {
        display = false;
      }

      protected override void OnStoryUpdate(Story.ParsedLine parse)
      {       
        if (!parse.isParsed)
        {
          speakerText.text = "";
          messageText.text = parse.line;
        }
        else
        {
          speakerText.text = parse.Find("Speaker");
          messageText.text = parse.Find("Message");
        }
      }
      
      protected override void OnPresentChoices(List<Choice> choices)
      {
        displayChoices = true;
        AddChoices(choices, choicePrefab, choicesPanel);
      }

      protected override void OnChoiceSelected()
      {
        displayChoices = false;
        RemoveChoices(choicesPanel);
      }
      


    }

  }
}