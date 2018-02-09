/******************************************************************************/
/*
@file   Railroad Snooze.ink
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
VAR week = 0
VAR current_room = "unset"


EXTERNAL PlayMusic(trackName)
EXTERNAL GetValue(value)
EXTERNAL SetTimeBlock(int)
EXTERNAL SetValue(name, values)
EXTERNAL SetIntValue(name, string)
EXTERNAL CallSleep()


-> Start

=== Start ===
I Scamper into bedroom, yawning as I do.
{
	-fatigue < 50:
		I'm not particularly tired, but when I hit the bed, my body becomes enraptured in comfort
	-else:
		I barely make it onto the bed before I close my eyes to rest.
}
I ride the railroad to dreamland in a flash, and I am out. #Background / Dream, eyeclose
... {SetValue("Depression Time Dilation", false)}
......{SetTimeBlock(0)} 
......... {CallSleep()}
[unknown] "...{player_name}."
huh?
"{player_name}. get up ya silly bum."
In what felt like a blink of an eye, my room is lite with morning sunlight. #Background / YourRoom, eyeopen
[Max] "You don't want to be sleeping in on your last day, now do you?" #Max=Calm
[{player_name}] "oh, uh... morning I guess."
I give a large yawn as I strech out of bed.
[Max] "ya sleep well? ya look tired."
[{player_name}] "actually... yeah. I think I slept really well." #fatigue => 0
not that I wouldn't like to go back to sleep, mind you. 
still, I feel very refreshed, oddly enough.
[Max] "That's great!" #Max=Happy
"Timothy, how bout you? you sleep well?"
Timothy looks dead tired. #Timothy=Sad
[Timothy] "...uuuughhh"
[Max] "Oh, sorry little dude."
"Don't worry, I'm sure today will be a nice and simple day."
And with that, MAx directed us to begin our morning routine.
They made special note of me needing to start packing for tomorrow...
tomorrow...
Either way, We wrapped up our morning chores, and went off into the day. #All = Exit
->END
