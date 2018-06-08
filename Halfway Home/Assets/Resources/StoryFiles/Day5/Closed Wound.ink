/******************************************************************************/
/*
@file   Closed Wound.ink
@author Jesse Lozano
@par    email: jesse.lozano@digipen.edu
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR depression = 0
VAR doubt = 0
VAR week = 0
VAR current_room = "unset"
VAR HoursSpent = 0

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetPlayerData()
EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)
EXTERNAL GetIntValue(value)
EXTERNAL AlterTime()
EXTERNAL SetTimeBlock(time)
EXTERNAL GetHour()
EXTERNAL GetSelfStat(stat_name)
EXTERNAL CallSleep()
EXTERNAL SetValue(ValueName, newValue)

-> Start

=== Start ===
{
	-GetValue("GettingMail") == true:
		->ClosedWound
	-else:
		You remeber last time, that around now Timothy ran off into your shared room to have a breakdown.
		->ClosedWound
}


===ClosedWound===
~SetValue("FoundWound", true)
You follow Timothy, who heads back to your bedroom #Backgroun = YourRoom
He's curled up on his bed, the letter he got, opened up on his room
He looks extremely distrught.
{
	-awareness > 3:
		should you try to comfort him, or go get someone more capable?
		+[Comfort Timothy on Your Own]
			->ComfortTimothy
		+[Go Get Max]
			->PoorStitches
}
->PoorStitches

===PoorStitches===
You do not feel comfortable handling this on your own. best to get Max. Their at least paid for this.
->END

===ComfortTimothy===
Comfort Misha! :P
->END