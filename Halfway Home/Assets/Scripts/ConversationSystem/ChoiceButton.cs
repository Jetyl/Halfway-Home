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
    int slot;

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
        Space.Connect<ChoiceEvent>(Events.ChoiceMade, RemoveChoice);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void ChoiceSelected()
    {
        Active = true;
        
        StartCoroutine(RemoveButton(DelayTime));

        Space.DispatchEvent(Events.ChoiceMade, new ChoiceEvent(choiceInfo, slot));
        
    }

    public void RemoveChoice(ChoiceEvent eventdata)
    {
        if (Active)
            return;

        StartCoroutine(RemoveButton(0));

    }

    public void UpdateChoice(ChoiceEvent eventdata)
    {
        button.colors = ExtractChoiceColor(ref eventdata.choicedata.text, Tags, Base.Colors);

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
        button.interactable = false;

        yield return new WaitForSeconds(aTime);

        gameObject.DispatchEvent(Events.Scale, new TransformEvent(Vector3.zero, ScaleTime));

        Color aWhite = Color.white;
        aWhite.a = 0;

        gameObject.DispatchEvent(Events.Fade, new FadeEvent(aWhite, ScaleTime));

        txt.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aWhite, ScaleTime));

        if(Active)
            Space.DispatchEvent(Events.ChoicesFinished, new ChoiceEvent(choiceInfo, slot));

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