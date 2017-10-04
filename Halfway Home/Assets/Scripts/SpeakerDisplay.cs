using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeakerDisplay : MonoBehaviour
{

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
            txt.text = "";

        if (Characters.Contains(eventdata.Speaker))
        {
            txt.color = Speakers[eventdata.Speaker];
        }
        else
            txt.color = Color.white;

        txt.text = TextParser.DynamicEdit(eventdata.Speaker);


    }

}
