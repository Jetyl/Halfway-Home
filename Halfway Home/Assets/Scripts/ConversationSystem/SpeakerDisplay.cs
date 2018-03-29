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

public class SpeakerDisplay : MonoBehaviour
{

    public GameObject Box;

    public List<string> Characters;
    public List<Color> Colors;

    Dictionary<string, Color> Speakers;

    TextMeshProUGUI txt;

	// Use this for initialization
	void Start ()
    {

        Speakers = new Dictionary<string, Color>();

        for (var i = 0; i < Characters.Count; ++i)
        {
            Speakers.Add(Characters[i], Colors[i]);
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
            Box.SetActive(false);
            return;
        }

        Box.SetActive(true);
        

        txt.color = GetColor(eventdata.TrueSpeaker);
        

        txt.text = TextParser.DynamicEdit(eventdata.Speaker);


    }

    public Color GetColor(string Speaker)
    {
        if (Characters.Contains(Speaker))
        {
            return Speakers[Speaker];
        }
        else
            return Color.white;
    }

}
