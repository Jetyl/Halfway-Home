/******************************************************************************/
/*!
File:   ChoiceButton.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ChoiceButton : MonoBehaviour
{

    public Choices choiceInfo;

    bool Active = true;

    //Button button;

    TextMeshProUGUI txt;

	// Use this for initialization
	void Start ()
    {
        //button = GetComponent<Button>();
        txt = GetComponentInChildren<TextMeshProUGUI>();

        EventSystem.ConnectEvent<ChoiceEvent>(gameObject, Events.Choice, ImplementChoice);

        Space.Connect<DefaultEvent>(Events.ChoiceMade, CleanUp);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    public void ImplementChoice(ChoiceEvent eventdata)
    {
        choiceInfo = eventdata.choicedata;
        txt.text = choiceInfo.text;
        Active = true;
    }

    public void ChoiceMade ()
    {
        if (!Active)
            return;

        Active = false;
        //add differnt stuff for conversation mode integration??



        Space.DispatchEvent(Events.ChoiceMade);

        if (choiceInfo.ConvMode)
        {
            Space.DispatchEvent(Events.ConversationChoice, new ChoiceEvent(choiceInfo));
        }
        else
        {
            if (choiceInfo.CallTo == EventListener.Space)
                Space.DispatchEvent(choiceInfo.DoOnChose);
            else if (choiceInfo.CallTo == EventListener.Owner)
                choiceInfo.OwnerRef.DispatchEvent(choiceInfo.DoOnChose);
        }
        

        

    }

    public void CleanUp(DefaultEvent eventdata)
    {
        if (Active)
            Active = false;
    }

    void OnDestroy()
    {

        if (Space.Instance != null)
            EventSystem.DisconnectEvent(Space.Instance.gameObject, Events.ChoiceMade, this);
    }


}


public class ChoiceEvent : DefaultEvent
{
    public Choices choicedata;

    public ChoiceEvent(Choices choice)
    {
        choicedata = choice;
    }
}