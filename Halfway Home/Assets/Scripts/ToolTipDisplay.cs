/******************************************************************************/
/*!
File:   TooltipDisplay.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToolTipDisplay : MonoBehaviour
{

    public bool Wellbeing;

    public Personality.Wellbeing WellnessStat;
    public Personality.Social SocialStat;

    public List<ToolTipEvent> Tips;

    public string ModifiedStatText;
    public Color ModifedStatColor = Color.yellow;

    public bool Debug;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void DisplayToolTip()
    {

        var display = new ToolTipEvent();

        
        foreach (var val in Tips)
        {

            if(Wellbeing)
            {
                if (Game.current.Self.GetWellbingStat(WellnessStat) >= val.Value)
                    display = new ToolTipEvent(val);
            }
            else
            {
                if (Game.current.Self.GetModifiedSocialStat(SocialStat) >= val.Value)
                    display = new ToolTipEvent(val);
            }

        }

        

        if(!Wellbeing)
        {
            if (Game.current.Self.GetModifiedSocialStat(SocialStat) != Game.current.Self.GetTrueSocialStat(SocialStat))
            {
                display.info = display.info + Environment.NewLine + ModifiedStatText;
                display.color = ModifedStatColor;
            }

            if (Debug)
            {
                display.info += display.info + Environment.NewLine + " value: " + Game.current.Self.GetModifiedSocialStat(SocialStat);
            }


        }
        else
        {
            if (Debug)
            {
                display.info += display.info + Environment.NewLine + " value: " + Game.current.Self.GetWellbingStat(WellnessStat);
            }
        }

        Space.DispatchEvent(Events.Tooltip, display);

    }
    public void UnDisplayToolTip()
    {
        Space.DispatchEvent(Events.Tooltip, new ToolTipEvent());
    }


}
