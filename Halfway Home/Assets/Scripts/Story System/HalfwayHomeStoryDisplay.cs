/******************************************************************************/
/*!
File:   HalfwayHomeStoryDisplay.cs
Author: Christian Sagel
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus.Modules.InkModule;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

namespace HalfwayHome
{
  public class HalfwayHomeStoryDisplay : StoryDisplay
  {
    //------------------------------------------------------------------------------------------/
    // Fields
    //------------------------------------------------------------------------------------------/
    //--- Public
    [Header("Dialog")]
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI messageText;

    [Header("Choices")]
    [SerializeField]
    public Button choicePrefab;
    public CanvasRenderer dialogPanel;
    public VerticalLayoutGroup choicesPanel;

    //--- Private
    private string currentSpeaker;

    //------------------------------------------------------------------------------------------/
    // Properties
    //------------------------------------------------------------------------------------------/
    public bool displayChoices
    {
      set
      {
        choicesPanel.gameObject.SetActive(value);
      }
    }

    //------------------------------------------------------------------------------------------/
    // Methods
    //------------------------------------------------------------------------------------------/
    protected override void OnStart()
    {
      Space.Connect<DefaultEvent>(Events.FinishedDescription, OnFinishedDescription);
    }
    void OnFinishedDescription(DefaultEvent eventdata)
    {
      ContinueStory();
    }

    protected override void OnStoryStarted()
    {
      
    }

    protected override void OnStoryEnded()
    {
      
    }

    protected override void OnStoryUpdate(ParsedLine parse, bool visited)
    {
      Parse dialog = parse.Find(HalfwayHomeStoryReader.dialogLabel);
      // 1. Monologue
      bool isMonologue = (dialog == null);
      if (isMonologue)
      {
        Space.DispatchEvent(Events.Description, new DescriptionEvent(parse.line, ""));
        return;
      }

      // 2. Dialog: Update the current speaker
      Parse speaker = parse.Find(HalfwayHomeStoryReader.speakerLabel);
      if (speaker != null)
        currentSpeaker = speaker.firstValue;
      
      Space.DispatchEvent(Events.Description, new DescriptionEvent(dialog.firstValue, currentSpeaker));
    }
    
    protected override void OnPresentChoices(List<Choice> choices)
    {
      displayChoices = true;

      // For each given choice,
      for (int i = 0; i < choices.Count; ++i)
      {
        Choice choice = choices[i];
        Button button = CreateChoiceView(choicePrefab, choicesPanel, choices[i].text.Trim());
        button.onClick.AddListener(delegate
        {
          SelectChoice(choice);
        });
      }
    }

    protected override void OnChoiceSelected()
    {
      displayChoices = false;
      RemoveChoices();
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