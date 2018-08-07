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

    public ChoiceDisplayData[] Tags;

    Choices choiceInfo;

    ChoiceDisplayData Base;

    bool Active = true;

    Button button;

    TextMeshProUGUI txt;

	// Use this for initialization
	void Start ()
    {
        button = GetComponent<Button>();
        txt = GetComponentInChildren<TextMeshProUGUI>();

        choiceInfo = new Choices(txt.text);
        Base.Colors = button.colors;

        EventSystem.ConnectEvent<ChoiceEvent>(gameObject, Events.Choice, UpdateChoice);

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void UpdateChoice(ChoiceEvent eventdata)
    {
        button.colors = ExtractChoiceColor(ref eventdata.choicedata.text, Tags, Base.Colors);

        txt.text = TextParser.DynamicEdit( eventdata.choicedata.text.Trim());
    }
    

    public ColorBlock ExtractChoiceColor(ref string text, ChoiceDisplayData[] compare, ColorBlock Base)
    {
        if (!text.Contains("<") || !text.Contains(">"))
            return Base;


        int i = text.IndexOf('<');
        int j = text.IndexOf('>', i + 1);

        string thisBit = text.Substring(i + 1, j - i - 1);

        foreach (var tag in compare)
        {
            if (thisBit.ToLower() == tag.tag.ToLower())
            {
                text = text.Replace("<" + tag.tag + ">", "");
                return tag.Colors;
            }
        }

        return Base;

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

[System.Serializable]
public struct ChoiceDisplayData
{
    public string tag;
    public ColorBlock Colors;
}