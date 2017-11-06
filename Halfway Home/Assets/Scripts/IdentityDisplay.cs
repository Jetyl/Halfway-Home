using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdentityDisplay : MonoBehaviour
{
    public GameObject ChoiceBox;
    public GameObject ConfirmBox;
    public TMP_InputField Name;
    public TextMeshProUGUI Pronouns;

    string genderPicked = "N";

	// Use this for initialization
	void Start ()
    {

        //ChoiceBox.SetActive(false);
        ConfirmBox.SetActive(false);
        
        Space.Connect<DefaultEvent>(Events.GetPlayerInfo, OnGetPlayerInfo);
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

    public void ConfirmIdentity()
    {
        ConfirmBox.SetActive(true);
    }

    public void SetIdentity()
    {

        Game.current.PlayerName = Name.text;
        Game.current.Progress.SetValue<string>("PlayerName", Game.current.PlayerName);
        Game.current.Progress.SetValue<string>("PlayerGender", genderPicked);

        ChoiceBox.SetActive(false);
        ConfirmBox.SetActive(false);

        Space.DispatchEvent(Events.SetPlayerIdentity);
        Space.DispatchEvent(Events.GetPlayerInfoFinished);
        gameObject.SetActive(false);
    }

  public void OnGetPlayerInfo(DefaultEvent e)
  {
        print("Ooooon");
    gameObject.SetActive(true);
  }
}
