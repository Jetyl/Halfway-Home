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
    public float ScaleTime;
    public float DelayTime;
    public ChoiceDisplayData[] Tags;

    Choices choiceInfo;

    ChoiceDisplayData Base;

    bool Active = true;

    Button button;

    Color BaseColor;

    TextMeshProUGUI txt;

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

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void UpdateChoice(ChoiceEvent eventdata)
    {
        button.colors = ExtractChoiceColor(ref eventdata.choicedata.text, Tags, Base.Colors);

        txt.text = TextParser.DynamicEdit( eventdata.choicedata.text.Trim());

        StartCoroutine(ShowButton(eventdata.ChoiceNumber * DelayTime));
    }

    IEnumerator ShowButton(float aTime)
    {
        yield return new WaitForSeconds(aTime);

        gameObject.DispatchEvent(Events.Scale, new TransformEvent(Vector3.one, ScaleTime));

        gameObject.DispatchEvent(Events.Fade, new FadeEvent(BaseColor, ScaleTime));

        txt.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Color.white, ScaleTime));

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
    public int ChoiceNumber;

    public ChoiceEvent(Choices choice, int num)
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