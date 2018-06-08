using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HistoryDisplay : MonoBehaviour
{

    public GameObject HistoryWindow;

    public ScrollRect ScrollArea;

    public TextMeshProUGUI Text;

    public SpeakerDisplay SpeakerColors;

    public TMP_Animator TagRemover;
    
    public Button[] ButtonsDisabledOnDisplay;
    public Image[] ImagesHiddenOnDisplay;
    public TextMeshProUGUI[] TextHiddenOnDisplay;

    string History = "";

    string CurrentSpeaker = "";

    private bool Displayed;

	// Use this for initialization
	void Start ()
    {

        HistoryWindow.SetActive(false);
        Displayed = false;

        Space.Connect<DescriptionEvent>(Events.Description, UpdateHistory);

        Space.Connect<DefaultEvent>(Events.ReturnToMap, ClearHistory);


        Space.Connect<DefaultEvent>(Events.Save, OnSave);
        Space.Connect<DefaultEvent>(Events.Load, OnLoad);

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonDown("History"))
        {
            ToggleHistory();
        }
	}

  public void ToggleHistory()
  {
    if (!Displayed) ShowHistory();
    else CloseHistory();
  }


    public void ShowHistory()
    {
        //display history for user to see

        Text.text = History;

        HistoryWindow.SetActive(true);
        ScrollArea.verticalNormalizedPosition = 0f;
        Space.DispatchEvent(Events.OpenHistory);
        Displayed = true;
        EnableButtons(false);
        EnableImages(false);
        EnableText(false);
    }

    public void CloseHistory()
    {
        HistoryWindow.SetActive(false);
        Space.DispatchEvent(Events.CloseHistory);
        Displayed = false;
        EnableButtons(true);
        EnableImages(true);
        EnableText(true);
    }

    void EnableButtons(bool e)
    { 
        foreach (Button o in ButtonsDisabledOnDisplay)
        {
          o.interactable = e;
          o.image.raycastTarget = e;
          foreach (Image i in o.GetComponentsInChildren<Image>())
          {
            i.raycastTarget = e;
          }
        }
    }
    
    void EnableImages(bool e)
    {
        foreach (Image i in ImagesHiddenOnDisplay)
        {
          i.enabled = e;
        }
    }

    void EnableText(bool e)
    {
        foreach (TextMeshProUGUI t in TextHiddenOnDisplay)
        {
          t.enabled = e;
        }
    }

    void UpdateHistory(DescriptionEvent eventdata)
    {
        TextParser.ExtractTextSpeed(ref eventdata.Line);

        TagRemover.KillAllCustomTags(ref eventdata.Line);

        if(eventdata.Speaker != CurrentSpeaker)
        {
            CurrentSpeaker = eventdata.Speaker;

            Color col = SpeakerColors.GetColor(eventdata.TrueSpeaker);

            string Speaker = eventdata.Speaker;

            if(col != Text.color)
            {
                var add = "<#" + ColorUtility.ToHtmlStringRGBA(col) + ">";

                Speaker = add + eventdata.Speaker + "</color>";
            }

            History += Environment.NewLine + TextParser.DynamicEdit(Speaker);

        }

        History += Environment.NewLine + TextParser.DynamicEdit(eventdata.Line);
  }

    void ClearHistory(DefaultEvent eventdata)
    {
        CurrentSpeaker = "";
        History = "";
    }

    public void OnSave(DefaultEvent eventdata)
    {
        Game.current.CurrentHistory = History;
        Game.current.CurrentSpeaker = CurrentSpeaker;

    }

    public void OnLoad(DefaultEvent eventdata)
    {
        History = Game.current.CurrentHistory;
        CurrentSpeaker = Game.current.CurrentSpeaker;
    }


}
