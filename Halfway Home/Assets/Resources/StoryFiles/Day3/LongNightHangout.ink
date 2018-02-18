/******************************************************************************/
/*
@file   LongNightHangout.ink
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
VAR delusion = 0
VAR doubt = 0
VAR week = 0
VAR current_room = "unset"

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

-> Start

=== Start ===
I walk down the halls of the home, and here some commotion happening in the commons area. {SetTimeBlock(1)}
Walking in, I spy Euardo and Isaac as the only ones in the room.
[Eduardo] "Hey! {player_name}! how's it hanging?!"
[{player_name}] "What are you guys still doing up?"
[Eduardo] "Oh, You know, chilling, shooting the sh-"
[Isaac] "Eduardo can't sleep."
[Eduardo] "Issaaaaaaac!"
[Isaac] "hrm. I'm not wrong."
"{player_name}. Can't sleep either?"
[{player_name}] "Something like that, yeah."
[Eduardo] "Well, your free to hangout with us!"
[{player_name}] "sure."
I sit down, and hangout with Isaac and Eduardo for a while.
->TimePassing.NextHour

===TimePassing===
another hour passes with the duo. I get a bit more tired. {AlterTime()}
{
	-GetIntValue("fatigue") > 90:
		->LeaveEarly
	-else:
		I Suppose I could go another hour. {SetTimeBlock(1)}
		->NextHour
}
=NextHour
{
	-GetHour() == 0:
		->Midnight
	-GetHour() == 1:
		->OneAM
	-GetHour() == 2:
		->TwoAM
	-GetHour() == 3:
		->ThreeAM
	-GetHour() == 4:
		->FourAM
	-GetHour() == 5:
		->FiveAM
	-else:
		->SixAM
}

//add the time breakdown

===Midnight===
//the midnight hour talk
the two talk about their usual night time rituals.
->TimePassing

===OneAM===
//the 1am hour talk
the two talk about how they met.
->TimePassing

===TwoAM===
//the 2 am hour talk
the two talk about their usual night time rituals.
->TimePassing

===ThreeAM===
//the 3 am hour talk
the two talk about their usual night time rituals.
->TimePassing

===FourAM===
//the 4 am hour talk
the two talk about their usual night time rituals.
->TimePassing

===FiveAM===
//the 5 am hour talk
the two talk about their usual night time rituals.
->TimePassing

===SixAM===
//the 6 am hour talk
Eduardo gets drowsy. Isaac admits something personal.
->MorningMax

===LeaveEarly===
I deceide to leave the two to their chatting, and head to bed.
->END

===MorningMax===
Everyone stays awake so long, that max arrives, and shoo's everyone to their bedroom.
->END