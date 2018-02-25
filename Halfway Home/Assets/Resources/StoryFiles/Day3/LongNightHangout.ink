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
[{player_name}] "So, are you two always up this late?"
[Eduardo] "Eh, I'm not really sure always is the best term for it, but-"
[Isaac] "Yes. Always."
[Eduardo] "Issaaaaaaac!"
[Isaac] "Always when he's manic like this. to be exact."
[Eduardo] "<size=50%>that's not that much better ya know...<size=100%>"
"I feel fine. Great even!"
//talk about eduardo's bipolar mood swings some more
[Isaac] "Not always out here. of course."
"Usually spend the nights he can't sleep in our bedroom. or painting"
[{player_name}] "You actually get anything done this late?"
[Isaac] "hrm. better than doing nothing."
the two talk about their usual night time rituals.
->TimePassing

===OneAM===
//the 1am hour talk
the two talk about how they met.
[{player_name}] "so, how did you two get together?"
->TimePassing

===TwoAM===
//the 2 am hour talk
the two talk about an anime they both like. also, complain about copyright
[Eduardo] "you know what trope really bugs the crap outta me in anime?"
[Isaac] "hrm?"
[Eduardo] "You got all these modern day stories, with all these fakey off brand refrences to real world things, instead of just straight up saying, hey yo! this is a thing."
+[what about that?]
	[{player_name}] "I don't quite think i follow."
+[That's the only problem you have?]
	[{player_name}] "Really? that's your only problem with anime?"
	[Eduardo] "What? no, but this is the one I feel like ranting about at 2AM"
-[Eduardo] "Like, it just really destorys the emerssion, for one."
"Second, it makes real world conversation like, near impossible."
->TimePassing

===ThreeAM===
//the 3 am hour talk
the two talk about the nature of attraction.
[{player_name}] "So, random question, but what are you two?"

->TimePassing

===FourAM===
//the 4 am hour talk
the two talk about why their in the halfway home.
[Eduardo] "Man, I know I don't say this often enough, but I'm gunna miss you guys when I leave."
->TimePassing

===FiveAM===
//the 5 am hour talk
the two talk about memes.
->TimePassing

===SixAM===
//the 6 am hour talk
Eduardo gets drowsy. Isaac admits something personal.
[Eduardo] ">Yawns aggresively"
"Isaac... Tell me a story."
[Isaac] "no."
[Eduardo] "Issaaaaaaac!"
[Isaac] ">Sighs"
->MorningMax

===LeaveEarly===
I yawn, as my eyes make another attempt at forcing themselves closed. My attention is shot, which means its probably a good time to call it a night.
I deceide to leave the two to their chatting, and head to bed.
->END

===MorningMax===
The sun actually begins to rise again, which is the first sign we stayed up way too late.
The second sign, is Max, who comes in for they morning mopping, and is rather surprised to see us up so early.
Everyone stays awake so long, that max arrives, and shoo's everyone to their bedroom.
->END