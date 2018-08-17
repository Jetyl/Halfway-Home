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
using System;

public class ChoiceButton : MonoBehaviour
{
    public float ScaleTime;
    public float DelayTime;
    public ChoiceDisplayData[] Tags;

    Choices choiceInfo;

    ChoiceDisplayData Base;

    bool Selected = false;
    bool SelectionMade = false;
    int slot;

    Button button;

    Color BaseColor;

    TextMeshProUGUI txt;
    
    string[] Comparisons = { ">", "<", ">=", "<=", "==", "!="};

    // Use this for initialization
    void Start ()
    {

        button = GetComponent<Button>();
        txt = GetComponentInChildren<TextMeshProUGUI>();

        var imgur = GetComponent<Image>();
        BaseColor = imgur.color;

        var aWhite = Color.white;
        aWhite.a = 0;

        imgur.color = aWhite;

        choiceInfo = new Choices(txt.text);
        Base.Colors = button.colors;

        EventSystem.ConnectEvent<ChoiceEvent>(gameObject, Events.Choice, UpdateChoice);
        Space.Connect<ChoiceEvent>(Events.ChoiceMade, RemoveChoice);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    //you get the type, and if the condtion checked was "Higher" or "Lower" than the value it was to compare. it was successfull
    void ChoiceUnlocked(string type, string high_low)
    {
        //turn on unlock icon
        //turn on tooltip
    }

    //you get the type, and if the condtion checked was "Higher" or "Lower" than the value it was to compare. it was unsuccessful
    void ChoiceLocked(string type, string high_low)
    {
        //turn on lock icon
        button.interactable = false;
        //turn on tooltip
    }

    public void ChoiceSelected()
    {
        if (SelectionMade)
            return;

        Selected = true;
        SelectionMade = true;
        Space.DispatchEvent(Events.ChoiceMade, new ChoiceEvent(choiceInfo, slot));

        StartCoroutine(RemoveButton(DelayTime));

        
    }

    public void RemoveChoice(ChoiceEvent eventdata)
    {
        if (Selected)
            return;

        SelectionMade = true;
        StartCoroutine(RemoveButton(0));

    }

    public void UpdateChoice(ChoiceEvent eventdata)
    {
        button.colors = ExtractChoiceColor(ref eventdata.choicedata.text, Base.Colors);
        txt.text = TextParser.DynamicEdit( eventdata.choicedata.text.Trim());

        slot = eventdata.ChoiceNumber;
        choiceInfo = eventdata.choicedata;

        StartCoroutine(ShowButton(eventdata.ChoiceNumber * DelayTime));
    }

    IEnumerator ShowButton(float aTime)
    {
        yield return new WaitForSeconds(aTime);

        gameObject.DispatchEvent(Events.Scale, new TransformEvent(Vector3.one, ScaleTime));

        gameObject.DispatchEvent(Events.Fade, new FadeEvent(BaseColor, ScaleTime));

        txt.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Color.white, ScaleTime));

    }

    IEnumerator RemoveButton(float aTime)
    {
        yield return new WaitForSeconds(aTime);

        gameObject.DispatchEvent(Events.Scale, new TransformEvent(Vector3.zero, ScaleTime));

        Color aWhite = Color.white;
        aWhite.a = 0;

        gameObject.DispatchEvent(Events.Fade, new FadeEvent(aWhite, ScaleTime));

        txt.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aWhite, ScaleTime));
        
        yield return new WaitForSeconds(ScaleTime);

        if (Selected)
            Space.DispatchEvent(Events.ChoicesFinished, new ChoiceEvent(choiceInfo, slot));

    }

    public ColorBlock ExtractChoiceColor(ref string text, ColorBlock Base)
    {
        if (!text.Contains("<(") || !text.Contains(")>"))
            return Base;


        int i = text.IndexOf("<(");
        int j = text.IndexOf(")>", i + 1);

        string thisBit = text.Substring(i + 2, j - i - 2);
        
        var colorKey = Decode(thisBit);

        foreach (var tag in Tags)
        {
            if (colorKey.ToLower() == tag.tag.ToLower())
            {
                text = text.Replace("<(" + thisBit + ")>", "");
                return tag.Colors;
            }
        }

        return Base;

    }

    string Decode(string encoded)
    {

        foreach(var comps in Comparisons)
        {
            if(encoded.Contains(comps))
            {
                string[] stringSeparators = new string[] { comps };
                var cutz = encoded.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                encoded = cutz[0].Trim();

                int val = 0;

                if (int.TryParse(cutz[1], out val))
                {
                    if (CheckCondition(encoded, comps, val))
                        ChoiceUnlocked(encoded, TooLowerTooHigh(comps));
                    else
                        ChoiceLocked(encoded, TooLowerTooHigh(comps));

                    return encoded;
                }

            }
        }

        return encoded;
    }

    string TooLowerTooHigh(string comp)
    {
        if (comp == "<" || comp == "<=")
        {
            return "Lower";
        }
        else
            return "Higher";

    }


    bool CheckCondition(string encoded, string condition, int value)
    {
        for (var i = 0; i < Enum.GetValues(typeof(Personality.Wellbeing)).Length; ++i)
        {
            if (Enum.GetName(typeof(Personality.Wellbeing), (Personality.Wellbeing)i).ToLower() == encoded.ToLower())
            {

                switch(condition)
                {
                    case "<":
                        return Game.current.Self.GetWellbingStat((Personality.Wellbeing)i) < value;
                    case ">":
                        return Game.current.Self.GetWellbingStat((Personality.Wellbeing)i) > value;
                    case ">=":
                        return Game.current.Self.GetWellbingStat((Personality.Wellbeing)i) >= value;
                    case "<=":
                        return Game.current.Self.GetWellbingStat((Personality.Wellbeing)i) <= value;
                    case "==":
                        return Game.current.Self.GetWellbingStat((Personality.Wellbeing)i) == value;
                    case "!=":
                        return Game.current.Self.GetWellbingStat((Personality.Wellbeing)i) != value;
                    default:
                        return false;
                }
                


            }

        }

        for (var i = 0; i < Enum.GetValues(typeof(Personality.Social)).Length; ++i)
        {
            if (Enum.GetName(typeof(Personality.Social), (Personality.Social)i).ToLower() == encoded.ToLower())
            {
                switch (condition)
                {
                    case "<":
                        return Game.current.Self.GetModifiedSocialStat((Personality.Social)i) < value;
                    case ">":
                        return Game.current.Self.GetModifiedSocialStat((Personality.Social)i) > value;
                    case ">=":
                        return Game.current.Self.GetModifiedSocialStat((Personality.Social)i) >= value;
                    case "<=":
                        return Game.current.Self.GetModifiedSocialStat((Personality.Social)i) <= value;
                    case "==":
                        return Game.current.Self.GetModifiedSocialStat((Personality.Social)i) == value;
                    case "!=":
                        return Game.current.Self.GetModifiedSocialStat((Personality.Social)i) != value;
                    default:
                        return false;
                }
            }
        }

        return false;
    }

    
    void OnDestroy()
    {
        EventSystem.DisconnectEvent<ChoiceEvent>(gameObject, Events.Choice, UpdateChoice);
        Space.DisConnect<ChoiceEvent>(Events.ChoiceMade, RemoveChoice);
    }

}


public class ChoiceEvent : DefaultEvent
{
    public Choices choicedata;
    public int ChoiceNumber;

    public ChoiceEvent(Choices choice, int num = 0)
    {
        choicedata = choice;
        ChoiceNumber = num;
    }
}

[System.Serializable]
public struct ChoiceDisplayData
{
    public string tag;
    public ColorBlock Colors;
}