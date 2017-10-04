using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus.InkModule;
using Ink.Runtime;
using UnityEngine.UI;
using Stratus;
using TMPro;

namespace HalfwayHome
{
  public class HalfwayHomeStoryDisplay : StoryDisplay
  {
    //------------------------------------------------------------------------------------------/
    // Fields
    //------------------------------------------------------------------------------------------/
    [Header("Dialog")]
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI messageText;

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

    protected override void OnStoryUpdate(Stratus.InkModule.Story.ParsedLine parse)
    {
      //if (logging)
      //  Trace.Script("Reading " + parse.line);

      if (!parse.isParsed)
      {
        //speakerText.text = "";
        //messageText.text = parse.line;

        Space.DispatchEvent(Events.Description, new DescriptionEvent(parse.line, ""));

      }
      else
      {
        //speakerText.text = parse.Find("Speaker");
        //messageText.text = parse.Find("Message");

        Space.DispatchEvent(Events.Description, new DescriptionEvent(parse.Find("Message"), parse.Find("Speaker")));
      }
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