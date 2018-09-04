/******************************************************************************/
/*
@file   PB Eduardo.ink
@author Jesse Lozano
@par    email: jesse.lozano@digipen.edu
All content © 2018 DigiPen (USA) Corporation, all rights reserved.
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
VAR week = 1
VAR current_room = "unset"
VAR currentHour = 0

EXTERNAL GetStringValue(name)
EXTERNAL SetValue(name, values)
EXTERNAL GetValue(name)
EXTERNAL SetTimeBlock(int)
EXTERNAL GetTrueSocial(name)
EXTERNAL CallSleep()
EXTERNAL GetHour()

-> Start

=== Start ===
I am pulled out of head by the sound of empty tins falling over behind me.
[unknown>Eduardo] "Shit!"
I look over, and see, Eduardo, looking worse for wear. #Eduardo = sad
In fact, it looks like he's been crying recently.
[{player_name}] "hey, Eduardo. You okay."
[Eduardo] "hrm? Oh, {player_name}, I didn't see you there."
"Uh, yeah, I'm fine."
[{player_name}] "You don't look fine."
[Eduardo] "No shit sherlock. You don't say." #Eduardo = angry
"Sorry. I've never been one to hide how I feel." #Eduardo = sad
[{player_name}] "I'm actually surprised how much Timothy's panic attack is affecting you?"
He laughes at that comment. #Eduardo = happy
[Eduardo] "Yeah, sure. That's it. Cuz I'm just <flow>sooo</flow> empathetic." #Eduardo = sad
He laughes some more, but even I can tell its fake.
"Sorry, {player_name}, you shouldn't have to deal with two people's broken minds in one day."
"Beside, I, uh, came in here, becuase I thought it was empty. I just want to be left alone right now."
"So, uh, see ya at dinner, or whatever." #Eduardo = exit
[{player_name}] "Um, okay."
I almost feel a bit better knowing I'm not the only one having a bad day becuase of this. @<color=color_descriptor><i>Commiseration <color=color_wellbeing_relief>relieved a small amount of <b>depression</b>.#depression -=10
Almost. @<color=color_wellbeing_penalty>Nevermind #depression += 10
->END