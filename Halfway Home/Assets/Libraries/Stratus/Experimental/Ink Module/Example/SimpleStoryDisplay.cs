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

        // For each given choice,
        for (int i = 0; i < choices.Count; ++i)
        {
          Choice choice = choices[i];
          Button button = CreateChoiceView(choices[i].text.Trim());
          button.onClick.AddListener(delegate
          {
            SelectChoice(choice);
            //SelectChoice(i);
          });
        }
      }

      protected override void OnChoiceSelected()
      {
        displayChoices = false;
        RemoveChoices();
      }

      Button CreateChoiceView(string text)
      {
        Button choice = Instantiate(choicePrefab) as Button;
        choice.transform.SetParent(choicesPanel.transform, false);

        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        return choice;
      }

      void RemoveChoices()
      {
        var choiceButtons = choicesPanel.GetComponentsInChildren<Button>();
        foreach (var choiceButton in choiceButtons)
        {
          Destroy(choiceButton.gameObject);
        }
      }


    }

  }
}