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
    public GameObject ChoiceBox;
    public GameObject ConfirmBox;
    public TextMeshProUGUI ConfirmText;
    public TMP_InputField Name;

    string namePicked = "Sam";

  public class PlayerGetInfoEvent : DefaultEvent { };

  public class PlayerSetInfoEvent : DefaultEvent { };

  // Use this for initialization
  void Start ()
    {

        //ChoiceBox.SetActive(false);
        ConfirmBox.SetActive(false);
        
        Space.Connect<IdentityDisplay.PlayerGetInfoEvent>(Events.GetPlayerInfo, OnGetPlayerInfo);
        gameObject.SetActive(false);
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
        ConfirmText.text = "Is your name " + namePicked + "?";
        ConfirmBox.SetActive(true);
        ChoiceBox.SetActive(false);
    }

    public void SetIdentity()
    {
        Game.current.PlayerName = namePicked;
        Game.current.Progress.SetValue<string>("PlayerName", Game.current.PlayerName);

        ChoiceBox.SetActive(false);
        ConfirmBox.SetActive(false);
        
        var setData = new IdentityDisplay.PlayerSetInfoEvent();
        Space.DispatchEvent(Events.SetPlayerIdentity, setData);
        Space.DispatchEvent(Events.GetPlayerInfoFinished);
        gameObject.SetActive(false);
    }

    public void OnGetPlayerInfo(IdentityDisplay.PlayerGetInfoEvent e)
    {
        gameObject.SetActive(true);
        ConfirmBox.SetActive(false);
    }
}
