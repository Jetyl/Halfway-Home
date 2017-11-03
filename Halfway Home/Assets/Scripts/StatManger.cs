using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManger : MonoBehaviour
{

    public int DefaultIncrementValue = 5;

    public List<StatAdder> IncrementValues;

	// Use this for initialization
	void Start ()
    {
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
            Game.current.Self.IncrementWellbeingStat(eventdata.WellnessStat, eventdata.Value);
            
        }
        else
        {
            foreach (var tier in IncrementValues)
            {
                if (tier.key == eventdata.Key)
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

    public ChangeStatEvent(string key, Personality.Social value)
    {
        Key = key;
        SocialStat = value;
        Wellbeing = false;
    }

    public ChangeStatEvent(int key, Personality.Wellbeing value)
    {
        Value = key;
        WellnessStat = value;
        Wellbeing = true;
    }


}