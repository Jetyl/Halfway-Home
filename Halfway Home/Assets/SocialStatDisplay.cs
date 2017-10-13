using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SocialStatDisplay : MonoBehaviour
{
    TextMeshProUGUI text;

    public Personality.Social SocialStat;

    Color NormalColor;
    public Color ModifiedColor = Color.yellow;

    public string[] SocialTierVerbs;

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<TextMeshProUGUI>();
        NormalColor = text.color;
        UpdateStats(new DefaultEvent());

        Space.Connect<DefaultEvent>(Events.StatChange, UpdateStats);

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    

    void UpdateStats(DefaultEvent eventdata)
    {
        int stat = Game.current.Self.GetModifiedSocialStat(SocialStat);
        text.text = SocialTierVerbs[stat];

        if (stat != Game.current.Self.GetTrueSocialStat(SocialStat))
            text.color = ModifiedColor;
        else
            text.color = NormalColor;

    }

}
