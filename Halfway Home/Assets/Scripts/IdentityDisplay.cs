/******************************************************************************/
/*!
File:   IdentityDisplay.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdentityDisplay : MonoBehaviour
{
    public bool IsNameDisplay;
    public GameObject ChoiceBox;
    public GameObject ConfirmBox;
    public TMP_InputField Name;
    public TextMeshProUGUI Pronouns;

    string genderPicked = "N";
    string namePicked = "Sam";

  public class PlayerGetInfoEvent : DefaultEvent
  {
    public bool IsName;

    public PlayerGetInfoEvent(bool isName)
    {
      IsName = isName;
    }
  }

  public class PlayerSetInfoEvent : DefaultEvent
  {
    public bool IsName;

    public PlayerSetInfoEvent(bool isName)
    {
      IsName = isName;
    }
  }

  // Use this for initialization
  void Start ()
    {

        //ChoiceBox.SetActive(false);
        ConfirmBox.SetActive(false);
        
        Space.Connect<IdentityDisplay.PlayerGetInfoEvent>(Events.GetPlayerInfo, OnGetPlayerInfo);
        gameObject.SetActive(false);
  }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AssignGender(string gender)
    {
        genderPicked = gender;

        if (genderPicked == "N")
            Pronouns.text = "They/Them";
        if (genderPicked == "M")
            Pronouns.text = "He/Him";
        if (genderPicked == "F")
            Pronouns.text = "She/Her";
    }

    public void AssignName()
    {
        if (Name.text == "")
            namePicked = "Sam";
        else
            namePicked = Name.text;
    }

    public void ConfirmIdentity()
    {
        ConfirmBox.SetActive(true);
    }

    public void SetIdentity()
    {
        if (IsNameDisplay)
        {
            Game.current.PlayerName = namePicked;
            Game.current.Progress.SetValue<string>("PlayerName", Game.current.PlayerName);
        }
        else
        { 
            Game.current.Progress.SetValue<string>("PlayerGender", genderPicked);
        }

        ChoiceBox.SetActive(false);
        ConfirmBox.SetActive(false);
        
        var setData = new IdentityDisplay.PlayerSetInfoEvent(IsNameDisplay);
        Space.DispatchEvent(Events.SetPlayerIdentity, setData);
        Space.DispatchEvent(Events.GetPlayerInfoFinished);
        gameObject.SetActive(false);
    }

    public void OnGetPlayerInfo(IdentityDisplay.PlayerGetInfoEvent e)
    {
        if(e.IsName == IsNameDisplay)
        {
          gameObject.SetActive(true);
          ConfirmBox.SetActive(false);
        }
    }
}
