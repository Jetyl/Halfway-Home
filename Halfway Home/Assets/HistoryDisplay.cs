using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class HistoryDisplay : MonoBehaviour
{

    public GameObject HistoryWindow;

    public TextMeshProUGUI Text;

    public SpeakerDisplay SpeakerColors;

    string History = "";

    string CurrentSpeaker = "";

	// Use this for initialization
	void Start ()
    {

        HistoryWindow.SetActive(false);

        Space.Connect<DescriptionEvent>(Events.Description, UpdateHistory);

        Space.Connect<DefaultEvent>(Events.ReturnToMap, ClearHistory);


        Space.Connect<DefaultEvent>(Events.Save, OnSave);
        Space.Connect<DefaultEvent>(Events.Load, OnLoad);

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.J))
        {
            ShowHistory();
        }
	}


    public void ShowHistory()
    {
        //display history for user to see

        Text.text = History;

        HistoryWindow.SetActive(true);
        Space.DispatchEvent(Events.OpenHistory);

    }

    public void CloseHistory()
    {
        HistoryWindow.SetActive(false);
        Space.DispatchEvent(Events.CloseHistory);
    }



    void UpdateHistory(DescriptionEvent eventdata)
    {
        TextParser.ExtractTextSpeed(ref eventdata.Line);

        if(eventdata.Speaker != CurrentSpeaker)
        {
            CurrentSpeaker = eventdata.Speaker;


            eventdata.Speaker = eventdata.Speaker.Replace("[", "");
            eventdata.Speaker = eventdata.Speaker.Replace("]", "");

            Color col = SpeakerColors.GetColor(eventdata.Speaker);

            if(col != Text.color)
            {
                var add = "<#" + ColorUtility.ToHtmlStringRGBA(col) + ">";

                eventdata.Speaker = add + eventdata.Speaker + "</color>";
            }

            History += Environment.NewLine + TextParser.DynamicEdit(eventdata.Speaker);

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
