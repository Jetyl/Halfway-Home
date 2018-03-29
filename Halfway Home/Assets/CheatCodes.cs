using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheatCodes : MonoBehaviour
{

    public bool Active;
    public GameObject Screen;
    TMP_InputField codeBreaker;

	// Use this for initialization
	void Start ()
    {
        codeBreaker = Screen.GetComponentInChildren<TMP_InputField>();
        Screen.SetActive(false);
        codeBreaker.DeactivateInputField();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!Active)
            return;

        if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if(Input.GetKeyDown(KeyCode.J))
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
