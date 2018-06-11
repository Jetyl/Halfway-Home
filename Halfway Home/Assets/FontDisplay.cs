using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LitJson;

public class FontDisplay : MonoBehaviour
{

    [HideInInspector]
    public List<string> Characters;
    [HideInInspector]
    public List<TMP_FontAsset> Fonts;

    public TMP_FontAsset NoSpeakerFont;
    public int NoSpeakerSize = 48;
    public TMP_FontAsset PlayerFont;
    public int PlayerFontSize = 48;
    public TMP_FontAsset RandomSpeakerFont;
    public int DefaultFontSize = 48;

    Dictionary<string, TMP_FontAsset> Speakers;
    Dictionary<string, int> Sizes;

    TextMeshProUGUI txt;

    // Use this for initialization
    void Start ()
    {
        Speakers = new Dictionary<string, TMP_FontAsset>();
        Sizes = new Dictionary<string, int>();


        var list = TextParser.ToJson("Characters");

        foreach (JsonData element in list)
        {
            string Names = (string)element["Name"];

            var Font = RandomSpeakerFont;

            if (element["font"] != null)
            {
                string slug = (string)element["font"];
                Font = Resources.Load<TMP_FontAsset>(slug);
            }
            
            Speakers.Add(Names, Font);

            Sizes.Add(Names, (int)element["FontSize"]);
        }


        for (var i = 0; i < Characters.Count; ++i)
        {
            //Speakers.Add(Characters[i], Fonts[i]);
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
            txt.fontSize = NoSpeakerSize;
            return;
        }
        
        txt.font = GetFont(eventdata.Speaker);
        txt.fontSize = GetSize(eventdata.Speaker);

    }

    public TMP_FontAsset GetFont(string Speaker)
    {
        if (Speaker == Game.current.PlayerName)
            return PlayerFont;

        if (Speakers.ContainsKey(Speaker))
        {
            return Speakers[Speaker];
        }
        else
            return RandomSpeakerFont;
    }

    public int GetSize(string Speaker)
    {
        if (Speaker == Game.current.PlayerName)
            return PlayerFontSize;

        if (Sizes.ContainsKey(Speaker))
        {
            return Sizes[Speaker];
        }
        else
            return DefaultFontSize;
    }

    void OnDestroy()
    {
        Space.DisConnect<DescriptionEvent>(Events.Description, OnNewLine);
    }

}
