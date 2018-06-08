using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontDisplay : MonoBehaviour
{


    public List<string> Characters;
    public List<TMP_FontAsset> Fonts;

    public TMP_FontAsset NoSpeakerFont;
    public TMP_FontAsset RandomSpeakerFont;

    Dictionary<string, TMP_FontAsset> Speakers;

    TextMeshProUGUI txt;

    // Use this for initialization
    void Start ()
    {
        Speakers = new Dictionary<string, TMP_FontAsset>();

        for (var i = 0; i < Characters.Count; ++i)
        {
            Speakers.Add(Characters[i], Fonts[i]);
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
            txt.font = NoSpeakerFont;
            return;
        }
        
        txt.font = GetFont(eventdata.Speaker);


    }

    public TMP_FontAsset GetFont(string Speaker)
    {
        if (Characters.Contains(Speaker))
        {
            return Speakers[Speaker];
        }
        else
            return RandomSpeakerFont;
    }

    void OnDestroy()
    {
        Space.DisConnect<DescriptionEvent>(Events.Description, OnNewLine);
    }

}
