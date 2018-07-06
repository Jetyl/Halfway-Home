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

        choiceInfo = new Choices(txt.text);

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    
    public void ChoiceMade ()
    {
        if (!Active)
            return;

        Active = false;
        
        Space.DispatchEvent(Events.ChoiceMade, new ChoiceEvent(choiceInfo));
        
        

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