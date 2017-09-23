using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus.InkModule;
using Ink.Runtime;
using UnityEngine.UI;
using Stratus;

namespace HalfwayHome
{
  public class HalfwayHomeStoryDisplay : StoryDisplay
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
    
    protected override void OnStoryUpdate(Stratus.InkModule.Story.ParsedLine parse)
    {
      if (logging)
        Trace.Script("Reading " + parse);

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