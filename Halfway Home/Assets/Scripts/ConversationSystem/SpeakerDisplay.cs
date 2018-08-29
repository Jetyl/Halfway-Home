/******************************************************************************/
/*!
File:   SpeakerDisplay.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LitJson;

public class SpeakerDisplay : MonoBehaviour
{

    public GameObject Box;
    public Color PlayerColor;

    [HideInInspector]
    public List<string> Characters;
    [HideInInspector]
    public List<Color> Colors;

    Dictionary<string, Color> Speakers;

    TextMeshProUGUI txt;

	// Use this for initialization
	void Start ()
    {

        Speakers = new Dictionary<string, Color>();
        
        var list = TextParser.ToJson("Characters");

        foreach (JsonData element in list)
        {
            float r = (float)(double)element["r"];
            float g = (float)(double)element["g"];
            float b = (float)(double)element["b"];
            float a = (float)(double)element["a"];

            Color col = new Color(r, g, b, a);

            string Names = (string)element["Name"];
            Speakers.Add(Names, col);
        }

        for (var i = 0; i < Characters.Count; ++i)
        {
            //Speakers.Add(Characters[i], Colors[i]);
        }

        txt = GetComponent<TextMeshProUGUI>();

        Space.Connect<DescriptionEvent>(Events.Description, OnNewLine);

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnNewLine(DescriptionEvent eventdata)
    {
        
        
        if (eventdata.Speaker == "")
        {
            txt.text = "";
            Box.GetComponent<UIFader>().Hide(0.2f);
            Box.tag = "NotHideable";
            return;
        }

        Box.GetComponent<UIFader>().Show(0.2f);
        Box.tag = "Untagged";

        txt.color = GetColor(eventdata.TrueSpeaker);
        

        txt.text = TextParser.DynamicEdit(eventdata.Speaker);


    }

    public Color GetColor(string Speaker)
    {
        if (Speaker == Game.current.PlayerName)
            return PlayerColor;
        
        if (Speakers.ContainsKey(Speaker))
        {
            return Speakers[Speaker];
        }
        else
            return Color.white;
    }

    void OnDestroy()
    {
        Space.DisConnect<DescriptionEvent>(Events.Description, OnNewLine);
    }

}
