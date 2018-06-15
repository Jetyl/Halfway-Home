using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

public class QuirkDisplay : MonoBehaviour
{

    Dictionary<string, List<TypingQuirk>> CharacterQuirks;
    Dictionary<string, TypingQuirk> BeginingEndingQuirk;


    // Use this for initialization
    void Start ()
    {
        
        CharacterQuirks = new Dictionary<string, List<TypingQuirk>>();
        BeginingEndingQuirk = new Dictionary<string, TypingQuirk>();

        var list = TextParser.ToJson("Characters");

        foreach (JsonData element in list)
        {
            
            string Names = (string)element["Name"];

            TypingQuirk StartEndQuirk;
            StartEndQuirk.NormalText = "";
            StartEndQuirk.QuirkedText = "";

            if (element["FrontQuirk"] != null)
                StartEndQuirk.NormalText = (string)element["FrontQuirk"];
            if (element["EndQuirk"] != null)
                StartEndQuirk.QuirkedText = (string)element["EndQuirk"];

            var ReplacingQuirks = new List<TypingQuirk>();

            if (element["Quirks"] != null)
            {
                for (int i = 0; i < element["Quirks"].Count; ++i)
                {
                    TypingQuirk replace;
                    replace.NormalText = (string)element["Quirks"][i]["NormalText"];
                    replace.QuirkedText = (string)element["Quirks"][i]["QuirkText"];

                    ReplacingQuirks.Add(replace);
                }

            }

            CharacterQuirks.Add(Names, ReplacingQuirks);
            BeginingEndingQuirk.Add(Names, StartEndQuirk);
        }


    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public string UpdateText(string text, string speaker)
    {
        if (!CharacterQuirks.ContainsKey(speaker))
            return text;

        foreach(var quirk in CharacterQuirks[speaker])
        {
            text = text.Replace(quirk.NormalText, quirk.QuirkedText);
        }

        var startEnd = BeginingEndingQuirk[speaker];

        text = startEnd.NormalText + text + startEnd.QuirkedText;

        return text;
    }
}

[Serializable]
public struct TypingQuirk
{
    public string NormalText;
    public string QuirkedText;
}