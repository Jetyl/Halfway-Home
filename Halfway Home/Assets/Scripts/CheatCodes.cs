using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheatCodes : MonoBehaviour
{

    public bool Active;
    public GameObject Screen;
    TMP_InputField codeBreaker;
    public TextMeshProUGUI CheatsStateText;
    public string QuickJumpScene1;
    public string QuickJumpScene2;
    public string QuickJumpScene3;

  // Use this for initialization
  void Start ()
    {
        Active = false;
        codeBreaker = Screen.GetComponentInChildren<TMP_InputField>();
        Screen.SetActive(false);
        codeBreaker.DeactivateInputField();
        CheatsStateText.gameObject.SetActive(false);
	  }
	
	// Update is called once per frame
	void Update ()
    {
        // Toggle cheat mode
        if (Input.GetButtonDown("Cheats"))
        {
          Active = !Active;
          if(CheatsStateText != null) CheatsStateText.gameObject.SetActive(Active);
        }
        
        if (Active)
        {
            // Check for manual entry
            if(Input.GetButtonDown("StringEntry"))
            {
                Screen.SetActive(!Screen.activeSelf);

                if(Screen.activeSelf)
                {
                    codeBreaker.ActivateInputField();
                }
                else
                {
                    codeBreaker.DeactivateInputField();
                }

            }
            // Check for stat change hotkeys
            if(Input.GetButton("AwarenessChange"))
            {
                if(Input.GetButtonDown("Increase")) Space.DispatchEvent(Events.AddStat, new ChangeStatEvent("minor", Personality.Social.Awareness));
            }
            if (Input.GetButton("GraceChange"))
            {
              if (Input.GetButtonDown("Increase")) Space.DispatchEvent(Events.AddStat, new ChangeStatEvent("minor", Personality.Social.Grace));
            }
            if (Input.GetButton("ExpressionChange"))
            {
              if (Input.GetButtonDown("Increase")) Space.DispatchEvent(Events.AddStat, new ChangeStatEvent("minor", Personality.Social.Expression));
            }
            if (Input.GetButton("FatigueChange"))
            {
              if (Input.GetButtonDown("Increase")) Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(10, Personality.Wellbeing.Fatigue));
              else if (Input.GetButtonDown("Decrease")) Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(-10, Personality.Wellbeing.Fatigue));
            }
            if (Input.GetButton("StressChange"))
            {
              if (Input.GetButtonDown("Increase")) Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(10, Personality.Wellbeing.Stress));
              else if (Input.GetButtonDown("Decrease")) Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(-10, Personality.Wellbeing.Stress));
            }
            if (Input.GetButton("DepressionChange"))
            {
              if (Input.GetButtonDown("Increase")) Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(10, Personality.Wellbeing.Depression));
              else if (Input.GetButtonDown("Decrease")) Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(-10, Personality.Wellbeing.Depression));
            }
            // Check for quick state hotkeys
            if(Input.GetButtonDown("JumpToState1")) ValidateCode(QuickJumpScene1);
            else if (Input.GetButtonDown("JumpToState2")) ValidateCode(QuickJumpScene2);
            else if (Input.GetButtonDown("JumpToState2")) ValidateCode(QuickJumpScene3);
    }
	}


    public void ValidateCode(string code)
    {
        code = code.ToLower();

        bool close = true;

        switch(code)
        {
            case "max_fatigue":
                Space.DispatchEvent(Events.AddStat, 
                    new ChangeStatEvent(100, Personality.Wellbeing.Fatigue, true));
                break;
            case "min_fatigue":
                Space.DispatchEvent(Events.AddStat,
                    new ChangeStatEvent(0, Personality.Wellbeing.Fatigue, true));
                break;
            case "max_stress":
                Space.DispatchEvent(Events.AddStat,
                    new ChangeStatEvent(100, Personality.Wellbeing.Stress, true));
                break;
            case "min_stress":
                Space.DispatchEvent(Events.AddStat,
                    new ChangeStatEvent(0, Personality.Wellbeing.Stress, true));
                break;
            case "max_depression":
                Space.DispatchEvent(Events.AddStat,
                    new ChangeStatEvent(100, Personality.Wellbeing.Depression, true));
                break;
            case "min_depression":
                Space.DispatchEvent(Events.AddStat,
                    new ChangeStatEvent(0, Personality.Wellbeing.Depression, true));
                break;
            case "awareness+":
                Space.DispatchEvent(Events.AddStat,
                    new ChangeStatEvent("minor", Personality.Social.Awareness));
                break;
            case "awareness++":
                Space.DispatchEvent(Events.AddStat,
                    new ChangeStatEvent("major", Personality.Social.Awareness));
                break;
            case "grace+":
                Space.DispatchEvent(Events.AddStat,
                    new ChangeStatEvent("minor", Personality.Social.Grace));
                break;
            case "grace++":
                Space.DispatchEvent(Events.AddStat,
                    new ChangeStatEvent("major", Personality.Social.Grace));
                break;
            case "expression+":
                Space.DispatchEvent(Events.AddStat,
                    new ChangeStatEvent("minor", Personality.Social.Expression));
                break;
            case "expression++":
                Space.DispatchEvent(Events.AddStat,
                    new ChangeStatEvent("major", Personality.Social.Expression));
                break;
            default:
                close = TimelineSystem.Current.TryCheatCode(code);
                break;
        }

        if (close)
        {
            Screen.SetActive(false);
            codeBreaker.DeactivateInputField();
        }

    }

}
