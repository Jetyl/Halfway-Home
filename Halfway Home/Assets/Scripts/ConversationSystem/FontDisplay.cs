using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LitJson;

public class FontDisplay : MonoBehaviour
{
    public class OverrideFontEvent : Stratus.Event { public int FontIndex; public OverrideFontEvent(int fontIndex = 0) { FontIndex = fontIndex; } }
    [HideInInspector]
    public List<string> Characters;
    [HideInInspector]
    public List<TMP_FontAsset> Fonts;
    public List<TMP_FontAsset> OverrideFonts;
    private int Override = 0;

    public TMP_FontAsset NoSpeakerFont;
    public int NoSpeakerSizeMin = 12;
    public int NoSpeakerSizeMax = 72;
    public TMP_FontAsset PlayerFont;
    public int PlayerFontSizeMin = 12;
    public int PlayerFontSizeMax = 72;
    public TMP_FontAsset RandomSpeakerFont;
    public int DefaultFontSizeMin = 12;
    public int DefaultFontSizeMax = 72;

    Dictionary<string, TMP_FontAsset> Speakers;
    Dictionary<string, int> Mins;
    Dictionary<string, int> Maxs;

    TextMeshProUGUI txt;

    // Use this for initialization
    void Start ()
    {
        Speakers = new Dictionary<string, TMP_FontAsset>();
        Mins = new Dictionary<string, int>();
        Maxs = new Dictionary<string, int>();

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

            Mins.Add(Names, (int)element["FontSizeMin"]);
            Maxs.Add(Names, (int)element["FontSizeMax"]);
        }


        for (var i = 0; i < Characters.Count; ++i)
        {
            //Speakers.Add(Characters[i], Fonts[i]);
        }

        txt = GetComponent<TextMeshProUGUI>();

        Space.Connect<DescriptionEvent>(Events.Description, OnNewLine);
        Stratus.Scene.Connect<OverrideFontEvent>(OnOverrideFontEvent);
    }
    
    void OnOverrideFontEvent(OverrideFontEvent e)
    {
        Override = e.FontIndex;
    }

    void OnNewLine(DescriptionEvent eventdata)
    {
        txt.enabled = false;
        
        txt.font = GetFont(eventdata.TrueSpeaker);
        txt.fontSizeMin = GetMin(eventdata.TrueSpeaker);
        txt.fontSizeMax = GetMax(eventdata.TrueSpeaker);
        
        txt.enabled = true;
    }

    public TMP_FontAsset GetFont(string Speaker)
    {
        if(Override>0) return OverrideFonts[Override-1];
        if (Speaker == "")
            return NoSpeakerFont;

        if (Speaker == Game.current.PlayerName)
            return PlayerFont;

        if (Speakers.ContainsKey(Speaker))
        {
            return Speakers[Speaker];
        }
        else
            return RandomSpeakerFont;
    }

    public int GetMin(string Speaker)
    {
        if (Speaker == "")
            return NoSpeakerSizeMin;

        if (Speaker == Game.current.PlayerName)
            return PlayerFontSizeMin;

        if (Mins.ContainsKey(Speaker))
        {
            return Mins[Speaker];
        }
        else
            return DefaultFontSizeMin;
    }

    public int GetMax(string Speaker)
    {
        if (Speaker == "")
            return NoSpeakerSizeMax;

        if (Speaker == Game.current.PlayerName)
            return PlayerFontSizeMax;

        if (Maxs.ContainsKey(Speaker))
        {
            return Maxs[Speaker];
        }
        else
            return DefaultFontSizeMax;
    }

    void OnDestroy()
    {
        Space.DisConnect<DescriptionEvent>(Events.Description, OnNewLine);
    }

}
