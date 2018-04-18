/******************************************************************************/
/*!
File:   StatManager.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManger : MonoBehaviour
{

    public int SocialThresholdValue = 50;

    public List<StatAdder> IncrementValues;

	// Use this for initialization
	void Start ()
    {
        Game.current.Self.SocialThreshold = SocialThresholdValue;

        Space.Connect<ChangeStatEvent>(Events.AddStat, OnAddValue);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnAddValue(ChangeStatEvent eventdata)
    {
        
        if(eventdata.Wellbeing)
        {
            if(eventdata.Assign) Game.current.Self.SetWellbeingStat(eventdata.WellnessStat, eventdata.Value);
            else Game.current.Self.IncrementWellbeingStat(eventdata.WellnessStat, eventdata.Value);
        }
        else
        {
            foreach (var tier in IncrementValues)
            {
                if (tier.key.ToLower() == eventdata.Key.ToLower())
                {
                    Game.current.Self.IncrementSocialStat(eventdata.SocialStat, tier.value);
                }
            }
        }

        Space.DispatchEvent(Events.StatChange);

    }

}

[System.Serializable]
public class StatAdder
{
    public string key;
    public int value;
}

public class ChangeStatEvent : DefaultEvent
{
    public string Key;
    public Personality.Social SocialStat;
    public Personality.Wellbeing WellnessStat;
    public bool Wellbeing;
    public int Value;
    public bool Assign;

    public ChangeStatEvent(string key, Personality.Social value)
    {
        Key = key;
        SocialStat = value;
        Wellbeing = false;
    }

    public ChangeStatEvent(int key, Personality.Wellbeing value, bool assign = false)
    {
        Value = key;
        WellnessStat = value;
        Wellbeing = true;
        Assign = assign;
    }


}